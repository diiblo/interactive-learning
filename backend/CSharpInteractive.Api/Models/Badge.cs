namespace CSharpInteractive.Api.Models;

public enum BadgeRuleType
{
    CompleteLessons = 1,
    TotalXp = 2,
    CompleteBossFinal = 3,
    CompleteLessonInCourse = 4,
    CompleteBossFinalInCourse = 5
}

public sealed class Badge
{
    public int Id { get; set; }
    public string Slug { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string IconName { get; set; } = "";
    public BadgeRuleType RuleType { get; set; }
    public int RuleValue { get; set; }
    public string? RuleCourseLanguage { get; set; }
    public List<UserBadge> UserBadges { get; set; } = [];
}
