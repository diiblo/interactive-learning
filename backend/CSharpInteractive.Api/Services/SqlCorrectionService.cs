using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Models;

namespace CSharpInteractive.Api.Services;

public sealed class SqlCorrectionService(SqlExecutionService executionService)
{
    public async Task<(ExecutionResultDto Execution, IReadOnlyList<TestResultDto> Tests, bool Passed)> SubmitAsync(Lesson lesson, string query)
    {
        var execution = await executionService.ExecuteQueryAsync(query);
        var executionDto = new ExecutionResultDto(execution.Success, execution.Output, execution.Diagnostics, execution.DurationMs, execution.Columns, execution.Rows);
        var results = new List<TestResultDto>();

        if (!execution.Success)
        {
            results.Add(new TestResultDto("Syntaxe et securite SQL", false, string.Join("\n", execution.Diagnostics)));
            return (executionDto, results, false);
        }

        foreach (var test in lesson.Tests.OrderBy(test => test.SortOrder).ThenBy(test => test.Id))
        {
            results.Add(Evaluate(test, query, execution));
        }

        return (executionDto, results, results.All(result => result.Passed));
    }

    private static TestResultDto Evaluate(LessonTest test, string query, SqlQueryResult execution)
    {
        return test.TestType switch
        {
            LessonTestType.ExpectedOutput => Contains(execution.Output, test.ExpectedOutput)
                ? Pass(test.Name)
                : Fail(test.Name, $"Le resultat attendu doit contenir: {test.ExpectedOutput}"),
            LessonTestType.RequiredSnippet => Contains(query, test.RequiredSnippet)
                ? Pass(test.Name)
                : Fail(test.Name, $"La requete doit contenir: {test.RequiredSnippet}"),
            LessonTestType.MinSnippetCount => CountOccurrences(query, test.RequiredSnippet) >= (test.MinCount ?? 1)
                ? Pass(test.Name)
                : Fail(test.Name, $"La requete doit contenir au moins {test.MinCount ?? 1} occurrences de: {test.RequiredSnippet}"),
            LessonTestType.SqlExpectedColumns => ColumnsMatch(execution.Columns, test.ExpectedColumns)
                ? Pass(test.Name)
                : Fail(test.Name, $"Les colonnes attendues sont: {test.ExpectedColumns}"),
            LessonTestType.SqlExpectedRowCount => execution.Rows.Count == (test.ExpectedRowCount ?? -1)
                ? Pass(test.Name)
                : Fail(test.Name, $"Le nombre de lignes attendu est: {test.ExpectedRowCount}"),
            LessonTestType.SqlForbiddenSnippet => !Contains(query, test.RequiredSnippet)
                ? Pass(test.Name)
                : Fail(test.Name, $"La requete ne doit pas contenir: {test.RequiredSnippet}"),
            _ => Fail(test.Name, "Type de test SQL inconnu.")
        };
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
