using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Models;

namespace CSharpInteractive.Api.Services;

public sealed class LessonCorrectionService(RoslynExecutionService executionService)
{
    public async Task<(ExecutionResultDto Execution, IReadOnlyList<TestResultDto> Tests, bool Passed)> SubmitAsync(Lesson lesson, string code)
    {
        var execution = await executionService.ExecuteAsync(code);
        var results = new List<TestResultDto>();

        if (!execution.Success)
        {
            results.Add(new TestResultDto("Compilation et execution", false, string.Join("\n", execution.Diagnostics)));
            return (execution, results, false);
        }

        foreach (var test in lesson.Tests.OrderBy(test => test.SortOrder).ThenBy(test => test.Id))
        {
            results.Add(await EvaluateAsync(test, code, execution.Output));
        }

        return (execution, results, results.All(result => result.Passed));
    }

    private async Task<TestResultDto> EvaluateAsync(LessonTest test, string code, string output)
    {
        return test.TestType switch
        {
            LessonTestType.ExpectedOutput => Contains(output, test.ExpectedOutput)
                ? Pass(test.Name)
                : Fail(test.Name, $"La sortie attendue doit contenir: {test.ExpectedOutput}"),
            LessonTestType.RequiredSnippet => Contains(code, test.RequiredSnippet)
                ? Pass(test.Name)
                : Fail(test.Name, $"Le code doit contenir: {test.RequiredSnippet}"),
            LessonTestType.HiddenCode => await EvaluateHiddenCodeAsync(test, code),
            LessonTestType.MinSnippetCount => CountOccurrences(code, test.RequiredSnippet) >= (test.MinCount ?? 1)
                ? Pass(test.Name)
                : Fail(test.Name, $"Le code doit contenir au moins {test.MinCount ?? 1} occurrences de: {test.RequiredSnippet}"),
            _ => Fail(test.Name, "Type de test inconnu.")
        };
    }

    private async Task<TestResultDto> EvaluateHiddenCodeAsync(LessonTest test, string code)
    {
        if (string.IsNullOrWhiteSpace(test.HiddenCode))
        {
            return Fail(test.Name, "Le test cache ne contient aucun code a executer.");
        }

        var hiddenExecution = await executionService.ExecuteAsync($"{code}\n{test.HiddenCode}");
        if (!hiddenExecution.Success)
        {
            var diagnostics = hiddenExecution.Diagnostics.Count > 0
                ? string.Join("\n", hiddenExecution.Diagnostics)
                : "Le test cache a echoue.";
            return Fail(test.Name, diagnostics);
        }

        if (!string.IsNullOrWhiteSpace(test.ExpectedOutput) && !Contains(hiddenExecution.Output, test.ExpectedOutput))
        {
            return Fail(test.Name, $"La sortie du test cache doit contenir: {test.ExpectedOutput}");
        }

        return Pass(test.Name);
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
