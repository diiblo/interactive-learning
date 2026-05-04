using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Models;

namespace CSharpInteractive.Api.Services;

public sealed record AiValidationResult(bool Passed, string Feedback, IReadOnlyList<string> Hints, string ProviderName);

public sealed class AiValidationService(HttpClient httpClient)
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public Task<AiValidationResult> ValidateLessonAsync(Lesson lesson, string code, IReadOnlyList<AiProviderConfigDto>? providers)
    {
        var prompt = BuildPrompt(
            lesson.Chapter?.Course?.Title ?? lesson.Chapter?.Course?.Language ?? "Cours",
            lesson.Title,
            lesson.Objective,
            lesson.ExercisePrompt,
            lesson.SuccessFeedback,
            lesson.Tests.OrderBy(test => test.SortOrder).Select(test => test.Name),
            code);

        return ValidateAsync(prompt, providers);
    }

    public Task<AiValidationResult> ValidateIntermediateBossAsync(IntermediateBoss boss, string code, IReadOnlyList<AiProviderConfigDto>? providers)
    {
        var prompt = BuildPrompt(
            boss.Module?.Course?.Title ?? boss.Module?.Course?.Language ?? "Cours",
            boss.Title,
            boss.Objective,
            boss.Instructions,
            boss.ExpectedResult,
            boss.ValidationRules.OrderBy(rule => rule.SortOrder).Select(rule => rule.Name),
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
        var url = $"{baseUrl}/models/{Uri.EscapeDataString(provider.Model)}:generateContent?key={Uri.EscapeDataString(provider.ApiKey)}";
        using var response = await httpClient.PostAsJsonAsync(url, new
        {
            contents = new[]
            {
                new
                {
                    role = "user",
                    parts = new[] { new { text = prompt } }
                }
            },
            generationConfig = new { temperature = 0.0, responseMimeType = "application/json" }
        }, JsonOptions);

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
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return document.RootElement.GetProperty("message").GetProperty("content").GetString() ?? "";
    }

    private static AiValidationResult? ParseVerdict(string content)
    {
        var json = ExtractJson(content);
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        if (!root.TryGetProperty("isCorrect", out var correctElement) && !root.TryGetProperty("passed", out correctElement))
        {
            return null;
        }

        var passed = correctElement.ValueKind switch
        {
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.String => bool.TryParse(correctElement.GetString(), out var value) && value,
            _ => false
        };
        var feedback = root.TryGetProperty("feedback", out var feedbackElement)
            ? feedbackElement.GetString() ?? ""
            : root.TryGetProperty("reason", out var reasonElement)
                ? reasonElement.GetString() ?? ""
                : "";
        var hints = ReadStringArray(root, "hints");

        return new AiValidationResult(passed, string.IsNullOrWhiteSpace(feedback) ? "Verdict IA recu." : feedback, hints, "IA");
    }

    private static IReadOnlyList<string> ReadStringArray(JsonElement root, string propertyName)
    {
        if (!root.TryGetProperty(propertyName, out var element) || element.ValueKind != JsonValueKind.Array)
        {
            return [];
        }

        return element.EnumerateArray()
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
        var start = trimmed.IndexOf('{');
        var end = trimmed.LastIndexOf('}');
        if (start >= 0 && end > start)
        {
            return trimmed[start..(end + 1)];
        }

        return trimmed;
    }

    private static string BuildPrompt(
        string course,
        string title,
        string objective,
        string instructions,
        string expectedResult,
        IEnumerable<string> validationRules,
        string answer) =>
        $$"""
        Corrige la reponse d'un apprenant.

        Cours: {{course}}
        Exercice: {{title}}
        Objectif: {{objective}}
        Enonce: {{instructions}}
        Resultat attendu: {{expectedResult}}
        Criteres visibles:
        {{string.Join("\n", validationRules.Select(rule => $"- {rule}"))}}

        Reponse de l'apprenant:
        ```text
        {{answer}}
        ```

        Regles:
        - Accepte les solutions equivalentes meme si la syntaxe differe.
        - Refuse une reponse dangereuse, hors sujet, incomplete ou qui contourne l'objectif.
        - Pour le code ou SQL, juge le comportement attendu, pas seulement les mots-cles.
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
