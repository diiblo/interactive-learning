namespace CSharpInteractive.Api.Contracts;

public sealed record ProfileDto(int Id, string DisplayName, int TotalXp, int Level, IReadOnlyList<BadgeDto> Badges);

public sealed record UpdateProfileRequest(string DisplayName);

public sealed record BadgeDto(int Id, string Slug, string Name, string Description, string IconName, DateTime? EarnedAt);

public sealed record ProgressDto(
    ProfileDto Profile,
    int CompletedLessons,
    int TotalLessons,
    IReadOnlyList<int> CompletedLessonIds,
    IReadOnlyList<int> AvailableLessonIds);
