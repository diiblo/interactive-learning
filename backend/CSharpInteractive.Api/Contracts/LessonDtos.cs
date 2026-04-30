using CSharpInteractive.Api.Models;

namespace CSharpInteractive.Api.Contracts;

public sealed record CourseSummaryDto(int Id, string Slug, string Title, string Description, string Language);

public sealed record CourseMapDto(int CourseId, string Title, IReadOnlyList<ChapterMapDto> Chapters, LessonMapItemDto? BossFinal);

public sealed record ChapterMapDto(int Id, string Title, string Description, int RequiredXp, IReadOnlyList<LessonMapItemDto> Lessons);

public sealed record LessonMapItemDto(int Id, string Slug, string Title, int XpReward, LessonProgressStatus Status, bool IsLocked, bool IsBossFinal);

public sealed record LessonDetailDto(
    int Id,
    string Slug,
    string Title,
    string EditorLanguage,
    string Objective,
    string ConceptSummary,
    string CommonMistakes,
    string Explanation,
    string ExampleCode,
    string ExercisePrompt,
    string StarterCode,
    string SuccessFeedback,
    string FailureFeedback,
    string FinalCorrection,
    int XpReward,
    bool IsBossFinal,
    LessonProgressStatus Status);
