namespace CSharpInteractive.Api.Models;

public enum SkillProgressStatus
{
    New,
    Learning,
    Fragile,
    Solid,
    Mastered,
    ReviewDue
}

public sealed class UserSkillProgress
{
    public int Id { get; set; }
    public int UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }
    public int SkillId { get; set; }
    public Skill? Skill { get; set; }
    public int MasteryPercent { get; set; }
    public int SuccessfulAttempts { get; set; }
    public int FailedAttempts { get; set; }
    public DateTime? NextReviewAt { get; set; }
    public SkillProgressStatus Status { get; set; } = SkillProgressStatus.New;
}
