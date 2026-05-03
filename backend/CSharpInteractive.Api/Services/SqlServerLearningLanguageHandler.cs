using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Models;

namespace CSharpInteractive.Api.Services;

public sealed class SqlServerLearningLanguageHandler(
    SqlExecutionService executionService,
    SqlCorrectionService correctionService) : ILearningLanguageHandler
{
    public string CourseLanguage => "sqlserver";
    public string EditorLanguage => "sql";

    public async Task<ExecutionResultDto> RunAsync(string code)
    {
        var result = await executionService.ExecuteQueryAsync(code);
        return new ExecutionResultDto(result.Success, result.Output, result.Diagnostics, result.DurationMs, result.Columns, result.Rows);
    }

    public Task<(ExecutionResultDto Execution, IReadOnlyList<TestResultDto> Tests, bool Passed)> SubmitAsync(Lesson lesson, string code) =>
        correctionService.SubmitAsync(lesson, code);

    public Task<TestResultDto> EvaluateBossRuleAsync(IntermediateBossValidationRule rule, string code, ExecutionResultDto execution)
    {
        var result = rule.TestType switch
        {
            LessonTestType.ExpectedOutput => Contains(execution.Output, rule.ExpectedOutput)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"Le resultat attendu doit contenir: {rule.ExpectedOutput}"),
            LessonTestType.RequiredSnippet => Contains(code, rule.RequiredSnippet)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"La requete doit contenir: {rule.RequiredSnippet}"),
            LessonTestType.MinSnippetCount => CountOccurrences(code, rule.RequiredSnippet) >= (rule.MinCount ?? 1)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"La requete doit contenir au moins {rule.MinCount ?? 1} occurrences de: {rule.RequiredSnippet}"),
            LessonTestType.SqlExpectedColumns => ColumnsMatch(execution.Columns ?? [], rule.ExpectedColumns)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"Les colonnes attendues sont: {rule.ExpectedColumns}"),
            LessonTestType.SqlExpectedRowCount => (execution.Rows?.Count ?? -1) == (rule.ExpectedRowCount ?? -1)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"Le nombre de lignes attendu est: {rule.ExpectedRowCount}"),
            LessonTestType.SqlForbiddenSnippet => !Contains(code, rule.RequiredSnippet)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"La requete ne doit pas contenir: {rule.RequiredSnippet}"),
            _ => Fail(rule.Name, "Type de validation SQL inconnu pour ce monstre.")
        };

        return Task.FromResult(result);
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

    private static TestResultDto Pass(string name) => new(name, true, "OK");

    private static TestResultDto Fail(string name, string message) => new(name, false, message);
}
