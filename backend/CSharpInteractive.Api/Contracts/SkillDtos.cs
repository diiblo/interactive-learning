using CSharpInteractive.Api.Models;

namespace CSharpInteractive.Api.Contracts;

public sealed record SkillDto(int Id, string CourseLanguage, string Slug, string Name, string Description);

public sealed record LessonHintDto(int HintLevel, string Content);

public sealed record SkillProgressDto(
    int SkillId,
    string CourseLanguage,
    string SkillSlug,
    string SkillName,
    string Description,
    int MasteryPercent,
    int SuccessfulAttempts,
    int FailedAttempts,
    DateTime? NextReviewAt,
    SkillProgressStatus Status,
    bool IsReviewDue,
    string? ReviewLessonSlug);

public sealed record ReviewItemDto(
    string SkillSlug,
    string SkillName,
    string CourseLanguage,
    int MasteryPercent,
    SkillProgressStatus Status,
    DateTime? NextReviewAt,
    IReadOnlyList<string> SuggestedLessonSlugs);

public sealed record ReviewCompletedRequest(int SkillId);
