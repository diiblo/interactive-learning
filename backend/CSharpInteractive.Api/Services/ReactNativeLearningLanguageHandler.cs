using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Models;

namespace CSharpInteractive.Api.Services;

public sealed class ReactNativeLearningLanguageHandler(StaticSnippetValidationService validationService) : ILearningLanguageHandler
{
    public string CourseLanguage => "react-native";
    public string EditorLanguage => "typescript";

    public Task<ExecutionResultDto> RunAsync(string code) => validationService.ValidateAsync(code);

    public Task<(ExecutionResultDto Execution, IReadOnlyList<TestResultDto> Tests, bool Passed)> SubmitAsync(Lesson lesson, string code) =>
        validationService.SubmitAsync(lesson, code);

    public Task<TestResultDto> EvaluateBossRuleAsync(IntermediateBossValidationRule rule, string code, ExecutionResultDto execution) =>
        Task.FromResult(validationService.Evaluate(rule, code));
}
