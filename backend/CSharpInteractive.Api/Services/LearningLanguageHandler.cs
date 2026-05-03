using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Models;

namespace CSharpInteractive.Api.Services;

public interface ILearningLanguageHandler
{
    string CourseLanguage { get; }
    string EditorLanguage { get; }
    Task<ExecutionResultDto> RunAsync(string code);
    Task<(ExecutionResultDto Execution, IReadOnlyList<TestResultDto> Tests, bool Passed)> SubmitAsync(Lesson lesson, string code);
    Task<TestResultDto> EvaluateBossRuleAsync(IntermediateBossValidationRule rule, string code, ExecutionResultDto execution);
}

public sealed class LearningLanguageService(IEnumerable<ILearningLanguageHandler> handlers)
{
    private readonly IReadOnlyDictionary<string, ILearningLanguageHandler> handlersByCourseLanguage =
        handlers.ToDictionary(handler => handler.CourseLanguage, StringComparer.OrdinalIgnoreCase);

    public ILearningLanguageHandler GetRequiredHandler(string courseLanguage)
    {
        if (handlersByCourseLanguage.TryGetValue(courseLanguage, out var handler))
        {
            return handler;
        }

        throw new InvalidOperationException($"Aucun moteur pedagogique n'est enregistre pour le langage '{courseLanguage}'.");
    }

    public ILearningLanguageHandler GetRequiredHandler(Lesson lesson) =>
        GetRequiredHandler(lesson.Chapter?.Course?.Language ?? "");

    public ILearningLanguageHandler GetRequiredHandler(IntermediateBoss boss) =>
        GetRequiredHandler(boss.Module?.Course?.Language ?? "");
}
