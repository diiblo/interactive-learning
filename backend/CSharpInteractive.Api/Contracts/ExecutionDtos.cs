namespace CSharpInteractive.Api.Contracts;

using CSharpInteractive.Api.Models;

public sealed record CodeRequest(string Code);

public sealed record SubmitCodeRequest(
    string Code,
    string ValidationMode = "local",
    IReadOnlyList<AiProviderConfigDto>? AiProviders = null);

public sealed record AiProviderConfigDto(
    string Id,
    string Type,
    string Name,
    string ApiKey,
    string Model,
    string? BaseUrl = null);

public enum SubmissionErrorCategory
{
    SyntaxError,
    MissingRequiredSnippet,
    ForbiddenSnippetUsed,
    WrongOutput,
    WrongLogic,
    EmptyCode,
    CompilationError,
    RuntimeError,
    PartialSolution,
    HardcodedSolution,
    Unknown
}

public sealed record ExecutionResultDto(
    bool Success,
    string Output,
    IReadOnlyList<string> Diagnostics,
    long DurationMs,
    IReadOnlyList<string>? Columns = null,
    IReadOnlyList<IReadOnlyDictionary<string, object>>? Rows = null);

public sealed record TestResultDto(string Name, bool Passed, string Message);

public sealed record RelatedSkillDto(string Slug, string Name, int MasteryPercent, SkillProgressStatus Status);

public sealed record SubmissionFeedbackDto(
    bool IsSuccess,
    string Summary,
    IReadOnlyList<string> WhatWentWell,
    IReadOnlyList<string> WhatIsMissing,
    SubmissionErrorCategory ErrorCategory,
    IReadOnlyList<string> ProgressiveHints,
    IReadOnlyList<RelatedSkillDto> RelatedSkills,
    IReadOnlyList<string> SuggestedReviewLessonSlugs);

public sealed record SkillResultDto(string SkillSlug, string SkillName, int ScorePercent, SkillProgressStatus Status, string Feedback);

public sealed record BossResultDto(
    bool IsSuccess,
    int ScorePercent,
    string Summary,
    IReadOnlyList<SkillResultDto> SkillResults,
    IReadOnlyList<string> Strengths,
    IReadOnlyList<string> Weaknesses,
    IReadOnlyList<string> SuggestedReviews);

public sealed record SubmitResultDto(
    bool Passed,
    string Output,
    IReadOnlyList<TestResultDto> TestResults,
    string Feedback,
    int XpEarned,
    int NewLevel,
    IReadOnlyList<int> UnlockedLessonIds,
    IReadOnlyList<BadgeDto> EarnedBadges,
    SubmissionFeedbackDto? StructuredFeedback = null,
    BossResultDto? BossResult = null);
