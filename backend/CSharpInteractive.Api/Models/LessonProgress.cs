namespace CSharpInteractive.Api.Models;

public enum LessonProgressStatus
{
    Locked = 0,
    Available = 1,
    Started = 2,
    Completed = 3
}

public sealed class LessonProgress
{
    public int Id { get; set; }
    public int UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }
    public int LessonId { get; set; }
    public Lesson? Lesson { get; set; }
    public LessonProgressStatus Status { get; set; }
    public string? BestCode { get; set; }
    public string? LastOutput { get; set; }
    public int Attempts { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int EarnedXp { get; set; }
}
