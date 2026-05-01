namespace CSharpInteractive.Api.Models;

public sealed class IntermediateBossProgress
{
    public int Id { get; set; }
    public int UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }
    public int IntermediateBossId { get; set; }
    public IntermediateBoss? IntermediateBoss { get; set; }
    public LessonProgressStatus Status { get; set; }
    public string? BestCode { get; set; }
    public string? LastOutput { get; set; }
    public int Attempts { get; set; }
    public int FailedAttempts { get; set; }
    public int HintsRevealed { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int EarnedXp { get; set; }
}
