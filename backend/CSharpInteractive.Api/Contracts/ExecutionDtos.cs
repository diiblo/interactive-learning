namespace CSharpInteractive.Api.Contracts;

public sealed record CodeRequest(string Code);

public sealed record ExecutionResultDto(
    bool Success,
    string Output,
    IReadOnlyList<string> Diagnostics,
    long DurationMs,
    IReadOnlyList<string>? Columns = null,
    IReadOnlyList<IReadOnlyDictionary<string, object>>? Rows = null);

public sealed record TestResultDto(string Name, bool Passed, string Message);

public sealed record SubmitResultDto(
    bool Passed,
    string Output,
    IReadOnlyList<TestResultDto> TestResults,
    string Feedback,
    int XpEarned,
    int NewLevel,
    IReadOnlyList<int> UnlockedLessonIds,
    IReadOnlyList<BadgeDto> EarnedBadges);
