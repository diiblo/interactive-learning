namespace CSharpInteractive.Api.Models;

public sealed class UserProfile
{
    public int Id { get; set; }
    public string DisplayName { get; set; } = "Apprenant";
    public int TotalXp { get; set; }
    public int Level { get; set; } = 1;
    public int? CurrentCourseId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public List<LessonProgress> LessonProgress { get; set; } = [];
    public List<UserBadge> UserBadges { get; set; } = [];
}
