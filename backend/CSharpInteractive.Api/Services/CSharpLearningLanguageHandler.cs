using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Models;

namespace CSharpInteractive.Api.Services;

public sealed class CSharpLearningLanguageHandler(
    RoslynExecutionService executionService,
    LessonCorrectionService correctionService) : ILearningLanguageHandler
{
    public string CourseLanguage => "csharp";
    public string EditorLanguage => "csharp";

    public Task<ExecutionResultDto> RunAsync(string code) => executionService.ExecuteAsync(code);

    public Task<(ExecutionResultDto Execution, IReadOnlyList<TestResultDto> Tests, bool Passed)> SubmitAsync(Lesson lesson, string code) =>
        correctionService.SubmitAsync(lesson, code);

    public async Task<TestResultDto> EvaluateBossRuleAsync(IntermediateBossValidationRule rule, string code, ExecutionResultDto execution)
    {
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
            _ => Fail(rule.Name, "Type de validation C# inconnu pour ce monstre.")
        };
    }

    private async Task<TestResultDto> EvaluateHiddenCodeAsync(IntermediateBossValidationRule rule, string code)
    {
        if (string.IsNullOrWhiteSpace(rule.HiddenCode))
        {
            return Fail(rule.Name, "La validation cachee ne contient aucun code a executer.");
        }

        var hiddenExecution = await executionService.ExecuteAsync($"{code}\n{rule.HiddenCode}");
        if (!hiddenExecution.Success)
        {
            return Fail(rule.Name, string.Join("\n", hiddenExecution.Diagnostics));
        }

        return string.IsNullOrWhiteSpace(rule.ExpectedOutput) || Contains(hiddenExecution.Output, rule.ExpectedOutput)
            ? Pass(rule.Name)
            : Fail(rule.Name, $"La sortie du test cache doit contenir: {rule.ExpectedOutput}");
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
