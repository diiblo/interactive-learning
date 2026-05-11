using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Models;

namespace CSharpInteractive.Api.Services;

public sealed class JavaScriptLearningLanguageHandler(StaticSnippetValidationService validationService) : ILearningLanguageHandler
{
    public string CourseLanguage => "javascript";
    public string EditorLanguage => "javascript";

    public Task<ExecutionResultDto> RunAsync(string code) => validationService.ValidateAsync(code);

    public Task<(ExecutionResultDto Execution, IReadOnlyList<TestResultDto> Tests, bool Passed)> SubmitAsync(Lesson lesson, string code) =>
        validationService.SubmitAsync(lesson, code);

    public Task<TestResultDto> EvaluateBossRuleAsync(IntermediateBossValidationRule rule, string code, ExecutionResultDto execution) =>
        Task.FromResult(validationService.Evaluate(rule, code));
}
