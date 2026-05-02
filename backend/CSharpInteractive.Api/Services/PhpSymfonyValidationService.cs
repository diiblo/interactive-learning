using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Models;

namespace CSharpInteractive.Api.Services;

public sealed class PhpSymfonyValidationService
{
    public Task<ExecutionResultDto> ValidateAsync(string code)
    {
        var diagnostics = new List<string>();
        if (string.IsNullOrWhiteSpace(code))
        {
            diagnostics.Add("Le code PHP/Symfony ne peut pas etre vide.");
        }

        if (code.Contains("<?php", StringComparison.OrdinalIgnoreCase) &&
            !code.TrimStart().StartsWith("<?php", StringComparison.OrdinalIgnoreCase))
        {
            diagnostics.Add("La balise <?php doit etre placee au debut du script quand elle est utilisee.");
        }

        var output = diagnostics.Count == 0
            ? "Validation statique PHP/Symfony terminee."
            : "";

        return Task.FromResult(new ExecutionResultDto(diagnostics.Count == 0, output, diagnostics, 0));
    }

    public async Task<(ExecutionResultDto Execution, IReadOnlyList<TestResultDto> Tests, bool Passed)> SubmitAsync(Lesson lesson, string code)
    {
        var execution = await ValidateAsync(code);
        var results = new List<TestResultDto>();

        if (!execution.Success)
        {
            results.Add(new TestResultDto("Validation statique PHP/Symfony", false, string.Join("\n", execution.Diagnostics)));
            return (execution, results, false);
        }

        foreach (var test in lesson.Tests.OrderBy(test => test.SortOrder).ThenBy(test => test.Id))
        {
            results.Add(Evaluate(test, code));
        }

        return (execution, results, results.All(result => result.Passed));
    }

    public TestResultDto Evaluate(IntermediateBossValidationRule rule, string code)
    {
        return rule.TestType switch
        {
            LessonTestType.ExpectedOutput => Contains(code, rule.ExpectedOutput)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"Le code doit produire ou contenir: {rule.ExpectedOutput}"),
            LessonTestType.RequiredSnippet => Contains(code, rule.RequiredSnippet)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"Le code doit contenir: {rule.RequiredSnippet}"),
            LessonTestType.MinSnippetCount => CountOccurrences(code, rule.RequiredSnippet) >= (rule.MinCount ?? 1)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"Le code doit contenir au moins {rule.MinCount ?? 1} occurrences de: {rule.RequiredSnippet}"),
            LessonTestType.SqlForbiddenSnippet => !Contains(code, rule.RequiredSnippet)
                ? Pass(rule.Name)
                : Fail(rule.Name, $"Le code ne doit pas contenir: {rule.RequiredSnippet}"),
            _ => Fail(rule.Name, "Type de validation PHP/Symfony inconnu.")
        };
    }

    private static TestResultDto Evaluate(LessonTest test, string code)
    {
        return test.TestType switch
        {
            LessonTestType.ExpectedOutput => Contains(code, test.ExpectedOutput)
                ? Pass(test.Name)
                : Fail(test.Name, $"Le code doit produire ou contenir: {test.ExpectedOutput}"),
            LessonTestType.RequiredSnippet => Contains(code, test.RequiredSnippet)
                ? Pass(test.Name)
                : Fail(test.Name, $"Le code doit contenir: {test.RequiredSnippet}"),
            LessonTestType.MinSnippetCount => CountOccurrences(code, test.RequiredSnippet) >= (test.MinCount ?? 1)
                ? Pass(test.Name)
                : Fail(test.Name, $"Le code doit contenir au moins {test.MinCount ?? 1} occurrences de: {test.RequiredSnippet}"),
            LessonTestType.SqlForbiddenSnippet => !Contains(code, test.RequiredSnippet)
                ? Pass(test.Name)
                : Fail(test.Name, $"Le code ne doit pas contenir: {test.RequiredSnippet}"),
            _ => Fail(test.Name, "Type de validation PHP/Symfony inconnu.")
        };
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
