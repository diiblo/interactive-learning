namespace CSharpInteractive.Api.Models;

public sealed class IntermediateBoss
{
    public int Id { get; set; }
    public int ModuleId { get; set; }
    public Chapter? Module { get; set; }
    public string Slug { get; set; } = "";
    public string Title { get; set; } = "";
    public string Objective { get; set; } = "";
    public string Instructions { get; set; } = "";
    public string StarterCode { get; set; } = "";
    public string ExpectedResult { get; set; } = "";
    public string Solution { get; set; } = "";
    public int XpReward { get; set; }
    public bool IsRequiredToUnlockNextModule { get; set; } = true;
    public List<IntermediateBossValidationRule> ValidationRules { get; set; } = [];
    public List<IntermediateBossHint> Hints { get; set; } = [];
}
