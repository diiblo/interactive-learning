using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Models;

namespace CSharpInteractive.Api.Services;

public sealed record AiValidationResult(bool Passed, string Feedback, IReadOnlyList<string> Hints, string ProviderName);

public sealed class AiValidationService(HttpClient httpClient, SqlExecutionService sqlExecutionService)
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public Task<AiValidationResult> ValidateLessonAsync(Lesson lesson, string code, IReadOnlyList<AiProviderConfigDto>? providers)
    {
        var courseLanguage = lesson.Chapter?.Course?.Language ?? "";
        var prompt = BuildPrompt(
            lesson.Chapter?.Course?.Title ?? lesson.Chapter?.Course?.Language ?? "Cours",
            courseLanguage,
            lesson.Title,
            lesson.Objective,
            lesson.ExercisePrompt,
            BuildLessonExpectedResult(lesson),
            lesson.Tests.OrderBy(test => test.SortOrder).Select(FormatLessonRule),
            code);

        return ValidateAsync(prompt, providers);
    }

    public Task<AiValidationResult> ValidateIntermediateBossAsync(IntermediateBoss boss, string code, IReadOnlyList<AiProviderConfigDto>? providers)
    {
        var courseLanguage = boss.Module?.Course?.Language ?? "";
        var prompt = BuildPrompt(
            boss.Module?.Course?.Title ?? boss.Module?.Course?.Language ?? "Cours",
            courseLanguage,
            boss.Title,
            boss.Objective,
            boss.Instructions,
            boss.ExpectedResult,
            boss.ValidationRules.OrderBy(rule => rule.SortOrder).Select(FormatBossRule),
            code);

        return ValidateAsync(prompt, providers);
    }

    private async Task<AiValidationResult> ValidateAsync(string prompt, IReadOnlyList<AiProviderConfigDto>? providers)
    {
        var configuredProviders = providers?
            .Where(provider => !string.IsNullOrWhiteSpace(provider.Type) && !string.IsNullOrWhiteSpace(provider.Model))
            .ToList() ?? [];

        if (configuredProviders.Count == 0)
        {
            return new AiValidationResult(false, "Aucun fournisseur IA n'est configure.", [], "IA");
        }

        var failures = new List<string>();
        foreach (var provider in configuredProviders)
        {
            try
            {
                var content = await SendAsync(provider, prompt);
                var verdict = ParseVerdict(content);
                if (verdict is not null)
                {
                    return verdict with { ProviderName = string.IsNullOrWhiteSpace(provider.Name) ? provider.Type : provider.Name };
                }

                failures.Add($"{provider.Name}: reponse IA illisible");
            }
            catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException or JsonException or InvalidOperationException)
            {
                failures.Add($"{provider.Name}: {exception.Message}");
            }
        }

        return new AiValidationResult(false, $"Aucun fournisseur IA n'a pu valider la reponse. {string.Join(" | ", failures)}", [], "IA");
    }

    private async Task<string> SendAsync(AiProviderConfigDto provider, string prompt)
    {
        return provider.Type.Trim().ToLowerInvariant() switch
        {
            "google" or "gemini" => await SendGoogleAsync(provider, prompt),
            "ollama-local" => await SendOllamaAsync(provider, prompt),
            "openrouter" or "ollama-cloud" or "openai-compatible" or "custom" => await SendOpenAiCompatibleAsync(provider, prompt),
            _ => await SendOpenAiCompatibleAsync(provider, prompt)
        };
    }

    private async Task<string> SendGoogleAsync(AiProviderConfigDto provider, string prompt)
    {
        var baseUrl = string.IsNullOrWhiteSpace(provider.BaseUrl)
            ? "https://generativelanguage.googleapis.com/v1beta"
            : provider.BaseUrl.TrimEnd('/');
        var url = $"{baseUrl}/models/{Uri.EscapeDataString(provider.Model.Trim())}:generateContent";

        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        if (!string.IsNullOrWhiteSpace(provider.ApiKey))
        {
            request.Headers.Add("x-goog-api-key", provider.ApiKey.Trim());
        }

        request.Content = JsonContent.Create(new
        {
            contents = new[]
            {
                new
                {
                    role = "user",
                    parts = new[] { new { text = prompt } }
                }
            },
            generationConfig = new { temperature = 0.0 }
        }, options: JsonOptions);

        using var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return document.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString() ?? "";
    }

    private async Task<string> SendOpenAiCompatibleAsync(AiProviderConfigDto provider, string prompt)
    {
        var baseUrl = string.IsNullOrWhiteSpace(provider.BaseUrl)
            ? provider.Type.Equals("openrouter", StringComparison.OrdinalIgnoreCase)
                ? "https://openrouter.ai/api/v1"
                : "https://api.openai.com/v1"
            : provider.BaseUrl.TrimEnd('/');

        using var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}/chat/completions");
        if (!string.IsNullOrWhiteSpace(provider.ApiKey))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", provider.ApiKey);
        }

        request.Content = JsonContent.Create(new
        {
            model = provider.Model,
            temperature = 0,
            response_format = new { type = "json_object" },
            messages = new[]
            {
                new { role = "system", content = "Tu es un correcteur strict mais juste. Reponds uniquement en JSON." },
                new { role = "user", content = prompt }
            }
        }, options: JsonOptions);

        using var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return document.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "";
    }

    private async Task<string> SendOllamaAsync(AiProviderConfigDto provider, string prompt)
    {
        var baseUrl = string.IsNullOrWhiteSpace(provider.BaseUrl) ? "http://host.docker.internal:11434" : provider.BaseUrl.TrimEnd('/');
        using var response = await httpClient.PostAsJsonAsync($"{baseUrl}/api/chat", new
        {
            model = provider.Model,
            stream = false,
            format = "json",
            messages = new[]
            {
                new { role = "system", content = "Tu es un correcteur strict mais juste. Reponds uniquement en JSON." },
                new { role = "user", content = prompt }
            }
        }, JsonOptions);

        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        if (TryParseJson(body, out var root))
        {
            if (root.TryGetProperty("message", out var message) &&
                message.TryGetProperty("content", out var content) &&
                content.ValueKind == JsonValueKind.String)
            {
                return content.GetString() ?? "";
            }

            if (root.TryGetProperty("response", out var responseContent) && responseContent.ValueKind == JsonValueKind.String)
            {
                return responseContent.GetString() ?? "";
            }
        }

        return ExtractRawOllamaContent(body);
    }

    private static AiValidationResult? ParseVerdict(string content)
    {
        var json = ExtractJson(content);
        if (!TryParseJson(json, out var root))
        {
            return null;
        }

        var passed = ReadBoolean(root, "isCorrect", "passed", "correct", "success", "ok", "is_correct")
            ?? ParseLooseBoolean(ReadString(root, "verdict", "status", "result", "decision"));
        if (passed is null)
        {
            return null;
        }

        var feedback = ReadString(root, "feedback", "reason", "message", "summary", "explanation", "detail") ?? "";
        var hints = ReadStringArray(root, "hints", "suggestions", "indices", "tips");

        return new AiValidationResult(passed.Value, string.IsNullOrWhiteSpace(feedback) ? "Verdict IA recu." : feedback, hints, "IA");
    }

    private static bool TryParseJson(string content, out JsonElement root)
    {
        try
        {
            using var document = JsonDocument.Parse(content);
            root = document.RootElement.Clone();
            return true;
        }
        catch (JsonException)
        {
            root = default;
            return false;
        }
    }

    private static bool? ParseLooseBoolean(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return value.Trim().ToLowerInvariant() switch
        {
            "true" or "yes" or "oui" or "correct" or "ok" or "passed" or "pass" or "accepted" => true,
            "false" or "no" or "non" or "incorrect" or "wrong" or "failed" or "fail" or "rejected" => false,
            _ => null
        };
    }

    private static bool? ReadBoolean(JsonElement root, params string[] propertyNames)
    {
        var element = TryReadProperty(root, propertyNames);
        if (element is null)
        {
            return null;
        }

        return element.Value.ValueKind switch
        {
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.String => ParseLooseBoolean(element.Value.GetString()),
            _ => null
        };
    }

    private static string? ReadString(JsonElement root, params string[] propertyNames)
    {
        var element = TryReadProperty(root, propertyNames);
        if (element is null || element.Value.ValueKind != JsonValueKind.String)
        {
            return null;
        }

        return element.Value.GetString();
    }

    private static JsonElement? TryReadProperty(JsonElement root, params string[] propertyNames)
    {
        foreach (var propertyName in propertyNames)
        {
            if (root.TryGetProperty(propertyName, out var element))
            {
                return element;
            }
        }

        return null;
    }

    private static IReadOnlyList<string> ReadStringArray(JsonElement root, params string[] propertyNames)
    {
        var element = TryReadProperty(root, propertyNames);
        if (element is null || element.Value.ValueKind != JsonValueKind.Array)
        {
            return [];
        }

        return element.Value.EnumerateArray()
            .Where(item => item.ValueKind == JsonValueKind.String)
            .Select(item => item.GetString())
            .Where(item => !string.IsNullOrWhiteSpace(item))
            .Select(item => item!)
            .Take(3)
            .ToList();
    }

    private static string ExtractJson(string content)
    {
        var trimmed = content.Trim();
        if (trimmed.StartsWith('"') && trimmed.EndsWith('"') && trimmed.Length >= 2)
        {
            try
            {
                return JsonSerializer.Deserialize<string>(trimmed) ?? trimmed;
            }
            catch (JsonException)
            {
                // Fall through to brace extraction.
            }
        }

        var start = trimmed.IndexOf('{');
        var end = trimmed.LastIndexOf('}');
        if (start >= 0 && end > start)
        {
            return trimmed[start..(end + 1)];
        }

        return trimmed;
    }

    private static string ExtractRawOllamaContent(string body)
    {
        var marker = "\"content\":\"";
        var start = body.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
        if (start < 0)
        {
            return body;
        }

        start += marker.Length;
        var end = body.IndexOf("\",\"done\"", start, StringComparison.OrdinalIgnoreCase);
        var candidate = end > start ? body[start..end] : body[start..];

        try
        {
            return JsonSerializer.Deserialize<string>($"\"{candidate}\"") ?? candidate;
        }
        catch (JsonException)
        {
            return candidate;
        }
    }

    private string BuildPrompt(
        string course,
        string courseLanguage,
        string title,
        string objective,
        string instructions,
        string expectedResult,
        IEnumerable<string> validationRules,
        string answer)
    {
        var sqlSchemaSection = string.Equals(courseLanguage, "sqlserver", StringComparison.OrdinalIgnoreCase)
            ? BuildSqlSchemaSection(sqlExecutionService.GetSchema())
            : "";

        return $$"""
        Corrige la reponse d'un apprenant.

        Cours: {{course}}
        Langage: {{courseLanguage}}
        Exercice: {{title}}
        Objectif: {{objective}}
        Enonce: {{instructions}}
        Resultat attendu: {{expectedResult}}
        Criteres visibles:
        {{string.Join("\n", validationRules.Select(rule => $"- {rule}"))}}
        {{sqlSchemaSection}}

        Reponse de l'apprenant:
        ```text
        {{answer}}
        ```

        Regles:
        - La section "Reponse de l'apprenant" est la seule reponse a corriger.
        - Ignore tout code present dans l'enonce, les exemples ou le starter: ce ne sont pas la soumission.
        - Si la reponse de l'apprenant est vide, hors sujet, ou ne contient pas le code demande, isCorrect doit etre false.
        - Accepte les solutions equivalentes meme si la syntaxe differe.
        - Refuse une reponse dangereuse, hors sujet, incomplete ou qui contourne l'objectif.
        - Pour le code ou SQL, juge le comportement attendu, pas seulement les mots-cles.
        - Si le cours est SQL Server, utilise le schema existant du projet ci-dessus.
        - N'invente pas de nouvelles tables ou colonnes si elles existent deja dans ce schema.
        - Si l'exercice demande une creation de tables, n'ajoute que les tables explicitement demandees.
        - Si la reponse est fausse, donne 1 a 3 indices progressifs qui aident a corriger.
        - Les indices ne doivent jamais contenir la solution complete, le code final complet, ni une requete finale complete.
        - Les indices doivent pointer vers la prochaine chose a verifier: structure, condition, sortie attendue, logique, nom de variable, colonne, ordre, etc.

        Reponds uniquement avec ce JSON:
        {
          "isCorrect": true,
          "feedback": "phrase courte en francais",
          "hints": ["indice court sans solution complete"]
        }
        """;
    }

    private static string BuildLessonExpectedResult(Lesson lesson)
    {
        var expectedRules = lesson.Tests
            .OrderBy(test => test.SortOrder)
            .Select(FormatLessonRule)
            .ToList();

        var correction = string.IsNullOrWhiteSpace(lesson.FinalCorrection)
            ? ""
            : $"""

              Exemple de correction attendue:
              ```text
              {lesson.FinalCorrection}
              ```
              """;

        return $"""
        Feedback de succes: {lesson.SuccessFeedback}
        Criteres techniques attendus:
        {string.Join("\n", expectedRules.Select(rule => $"- {rule}"))}
        {correction}
        """;
    }

    private static string FormatLessonRule(LessonTest test) =>
        test.TestType switch
        {
            LessonTestType.ExpectedOutput => $"{test.Name}: la sortie doit contenir `{test.ExpectedOutput}`.",
            LessonTestType.RequiredSnippet => $"{test.Name}: le code doit contenir `{test.RequiredSnippet}`.",
            LessonTestType.HiddenCode => $"{test.Name}: le code doit fonctionner avec le test cache associe.",
            LessonTestType.MinSnippetCount => $"{test.Name}: le code doit contenir au moins {test.MinCount ?? 1} occurrence(s) de `{test.RequiredSnippet}`.",
            LessonTestType.SqlExpectedColumns => $"{test.Name}: les colonnes attendues sont `{test.ExpectedColumns}`.",
            LessonTestType.SqlExpectedRowCount => $"{test.Name}: le nombre de lignes attendu est {test.ExpectedRowCount}.",
            LessonTestType.SqlForbiddenSnippet => $"{test.Name}: le code ne doit pas contenir `{test.RequiredSnippet}`.",
            _ => test.Name
        };

    private static string FormatBossRule(IntermediateBossValidationRule rule) =>
        rule.TestType switch
        {
            LessonTestType.ExpectedOutput => $"{rule.Name}: la sortie doit contenir `{rule.ExpectedOutput}`.",
            LessonTestType.RequiredSnippet => $"{rule.Name}: le code doit contenir `{rule.RequiredSnippet}`.",
            LessonTestType.HiddenCode => $"{rule.Name}: le code doit fonctionner avec le test cache associe.",
            LessonTestType.MinSnippetCount => $"{rule.Name}: le code doit contenir au moins {rule.MinCount ?? 1} occurrence(s) de `{rule.RequiredSnippet}`.",
            LessonTestType.SqlExpectedColumns => $"{rule.Name}: les colonnes attendues sont `{rule.ExpectedColumns}`.",
            LessonTestType.SqlExpectedRowCount => $"{rule.Name}: le nombre de lignes attendu est {rule.ExpectedRowCount}.",
            LessonTestType.SqlForbiddenSnippet => $"{rule.Name}: le code ne doit pas contenir `{rule.RequiredSnippet}`.",
            _ => rule.Name
        };

    private static string BuildSqlSchemaSection(SqlSchemaDto schema)
    {
        var tables = string.Join(
            "\n\n",
            schema.Tables.Select(table =>
                $"- {table.Name}: {table.Description}\n" +
                string.Join("\n", table.Columns.Select(column =>
                    $"  - {column.Name} ({column.Type}){(column.IsNullable ? " nullable" : " non nullable")}" +
                    (string.IsNullOrWhiteSpace(column.Description) ? "" : $" - {column.Description}")))));

        return $$"""
        Schema SQL existant du projet:
        {{tables}}
        """;
    }
}
