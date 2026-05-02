using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpInteractive.Api.Services;

public sealed class IntermediateBossService(
    AppDbContext db,
    RoslynExecutionService roslynExecutionService,
    SqlExecutionService sqlExecutionService,
    PhpSymfonyValidationService phpSymfonyValidationService,
    ProgressService progressService,
    UnlockService unlockService)
{
    public async Task<ExecutionResultDto> RunAsync(IntermediateBoss boss, string code)
    {
        if (IsSqlBoss(boss))
        {
            var result = await sqlExecutionService.ExecuteQueryAsync(code);
            return new ExecutionResultDto(result.Success, result.Output, result.Diagnostics, result.DurationMs, result.Columns, result.Rows);
        }

        if (IsPhpSymfonyBoss(boss))
        {
            return await phpSymfonyValidationService.ValidateAsync(code);
        }

        return await roslynExecutionService.ExecuteAsync(code);
    }

    public async Task<SubmitResultDto> SubmitAsync(UserProfile profile, IntermediateBoss boss, string code)
    {
        var execution = await RunAsync(boss, code);
        var results = new List<TestResultDto>();

        if (!execution.Success)
        {
            results.Add(new TestResultDto(
                IsSqlBoss(boss)
                    ? "Syntaxe et securite SQL"
                    : IsPhpSymfonyBoss(boss)
                        ? "Validation statique PHP/Symfony"
                        : "Compilation et execution",
                false,
                string.Join("\n", execution.Diagnostics)));
        }
        else
        {
            foreach (var rule in boss.ValidationRules.OrderBy(rule => rule.SortOrder).ThenBy(rule => rule.Id))
            {
                results.Add(await EvaluateRuleAsync(rule, code, execution));
            }
        }

        var passed = results.Count > 0 && results.All(result => result.Passed);
        var progress = await GetOrCreateProgressAsync(profile, boss);
        progress.Attempts += 1;
        progress.LastOutput = execution.Output;

        var xpEarned = 0;
        if (passed)
        {
            progress.Status = LessonProgressStatus.Completed;
            progress.BestCode = code;
            progress.CompletedAt ??= DateTime.UtcNow;

            if (progress.EarnedXp == 0)
            {
                xpEarned = boss.XpReward;
                progress.EarnedXp = boss.XpReward;
                profile.TotalXp += xpEarned;
                profile.Level = progressService.CalculateLevel(profile.TotalXp);
                profile.UpdatedAt = DateTime.UtcNow;
            }
        }
        else
        {
            progress.FailedAttempts += 1;
            if (progress.Status == LessonProgressStatus.Available)
            {
                progress.Status = LessonProgressStatus.Started;
            }
        }

        await db.SaveChangesAsync();
        var unlocked = passed ? await unlockService.RefreshUnlocksAsync(profile) : [];
        var feedback = passed
            ? "Monstre vaincu. Le module suivant peut maintenant etre deverrouille."
            : "Le monstre resiste encore. Corrige les erreurs indiquees, puis retente.";

        return new SubmitResultDto(passed, execution.Output, results, feedback, xpEarned, profile.Level, unlocked, []);
    }

    public async Task<IntermediateBossHintResultDto> RevealNextHintAsync(UserProfile profile, IntermediateBoss boss)
    {
        var progress = await GetOrCreateProgressAsync(profile, boss);
        var hints = boss.Hints.OrderBy(hint => hint.SortOrder).ThenBy(hint => hint.Id).ToList();
        if (progress.HintsRevealed < hints.Count)
        {
            progress.HintsRevealed += 1;
            await db.SaveChangesAsync();
        }

        var revealed = hints
            .Take(progress.HintsRevealed)
            .Select((hint, index) => new IntermediateBossHintDto(index + 1, hint.Content))
            .ToList();

        return new IntermediateBossHintResultDto(revealed, progress.HintsRevealed < hints.Count);
    }

    public async Task<IntermediateBossSolutionDto?> RevealSolutionAsync(UserProfile profile, IntermediateBoss boss)
    {
        var progress = await db.IntermediateBossProgress
            .FirstOrDefaultAsync(item => item.UserProfileId == profile.Id && item.IntermediateBossId == boss.Id);

        return progress?.FailedAttempts > 0 ? new IntermediateBossSolutionDto(boss.Solution) : null;
    }

    private async Task<IntermediateBossProgress> GetOrCreateProgressAsync(UserProfile profile, IntermediateBoss boss)
    {
        var progress = await db.IntermediateBossProgress
            .FirstOrDefaultAsync(item => item.UserProfileId == profile.Id && item.IntermediateBossId == boss.Id);

        if (progress is not null)
        {
            return progress;
        }

        progress = new IntermediateBossProgress
        {
            UserProfileId = profile.Id,
            IntermediateBossId = boss.Id,
            Status = LessonProgressStatus.Available
        };
        db.IntermediateBossProgress.Add(progress);
        return progress;
    }

    private async Task<TestResultDto> EvaluateRuleAsync(IntermediateBossValidationRule rule, string code, ExecutionResultDto execution)
    {
        if (execution.Columns is not null && execution.Rows is not null)
        {
            return EvaluateSqlRule(rule, code, execution);
        }

        if (rule.IntermediateBoss?.Module?.Course?.Language == "php-symfony")
        {
            return phpSymfonyValidationService.Evaluate(rule, code);
        }

        return rule.TestType switch
        {
            LessonTestType.ExpectedOutput => Contains(execution.Output, rule.ExpectedOutput)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"La sortie attendue doit contenir: {rule.ExpectedOutput}"),
            LessonTestType.RequiredSnippet => Contains(code, rule.RequiredSnippet)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"Le code doit contenir: {rule.RequiredSnippet}"),
            LessonTestType.MinSnippetCount => CountOccurrences(code, rule.RequiredSnippet) >= (rule.MinCount ?? 1)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"Le code doit contenir au moins {rule.MinCount ?? 1} occurrences de: {rule.RequiredSnippet}"),
            LessonTestType.HiddenCode => await EvaluateHiddenCodeAsync(rule, code),
            _ => Fail(rule.Name, "Type de validation inconnu pour ce monstre.")
        };
    }

    private async Task<TestResultDto> EvaluateHiddenCodeAsync(IntermediateBossValidationRule rule, string code)
    {
        if (string.IsNullOrWhiteSpace(rule.HiddenCode))
        {
            return Fail(rule.Name, "La validation cachee ne contient aucun code a executer.");
        }

        var hiddenExecution = await roslynExecutionService.ExecuteAsync($"{code}\n{rule.HiddenCode}");
        if (!hiddenExecution.Success)
        {
            return Fail(rule.Name, string.Join("\n", hiddenExecution.Diagnostics));
        }

        return string.IsNullOrWhiteSpace(rule.ExpectedOutput) || Contains(hiddenExecution.Output, rule.ExpectedOutput)
            ? Pass(rule.Name)
            : Fail(rule.Name, $"La sortie du test cache doit contenir: {rule.ExpectedOutput}");
    }

    private static TestResultDto EvaluateSqlRule(IntermediateBossValidationRule rule, string query, ExecutionResultDto execution)
    {
        return rule.TestType switch
        {
            LessonTestType.ExpectedOutput => Contains(execution.Output, rule.ExpectedOutput)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"Le resultat attendu doit contenir: {rule.ExpectedOutput}"),
            LessonTestType.RequiredSnippet => Contains(query, rule.RequiredSnippet)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"La requete doit contenir: {rule.RequiredSnippet}"),
            LessonTestType.MinSnippetCount => CountOccurrences(query, rule.RequiredSnippet) >= (rule.MinCount ?? 1)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"La requete doit contenir au moins {rule.MinCount ?? 1} occurrences de: {rule.RequiredSnippet}"),
            LessonTestType.SqlExpectedColumns => ColumnsMatch(execution.Columns ?? [], rule.ExpectedColumns)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"Les colonnes attendues sont: {rule.ExpectedColumns}"),
            LessonTestType.SqlExpectedRowCount => (execution.Rows?.Count ?? -1) == (rule.ExpectedRowCount ?? -1)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"Le nombre de lignes attendu est: {rule.ExpectedRowCount}"),
            LessonTestType.SqlForbiddenSnippet => !Contains(query, rule.RequiredSnippet)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"La requete ne doit pas contenir: {rule.RequiredSnippet}"),
            _ => Fail(rule.Name, "Type de validation SQL inconnu pour ce monstre.")
        };
    }

    private static bool IsSqlBoss(IntermediateBoss boss) => boss.Module?.Course?.Language == "sqlserver";

    private static bool IsPhpSymfonyBoss(IntermediateBoss boss) => boss.Module?.Course?.Language == "php-symfony";

    private static bool Contains(string source, string? expected) =>
        !string.IsNullOrWhiteSpace(expected) && source.Contains(expected, StringComparison.OrdinalIgnoreCase);

    private static int CountOccurrences(string source, string? expected)
    {
        if (string.IsNullOrWhiteSpace(expected))
        {
            return 0;
        }

        var count = 0;
        var index = 0;
        while ((index = source.IndexOf(expected, index, StringComparison.OrdinalIgnoreCase)) >= 0)
        {
            count++;
            index += expected.Length;
        }

        return count;
    }

    private static bool ColumnsMatch(IReadOnlyList<string> actual, string? expected)
    {
        if (string.IsNullOrWhiteSpace(expected))
        {
            return false;
        }

        var expectedColumns = expected.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return actual.Count == expectedColumns.Length && actual.Zip(expectedColumns).All(pair => string.Equals(pair.First, pair.Second, StringComparison.OrdinalIgnoreCase));
    }

    private static TestResultDto Pass(string name) => new(name, true, "OK");

    private static TestResultDto Fail(string name, string message) => new(name, false, message);
}
