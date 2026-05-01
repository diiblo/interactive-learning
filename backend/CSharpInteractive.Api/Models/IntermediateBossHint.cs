namespace CSharpInteractive.Api.Models;

public sealed class IntermediateBossHint
{
    public int Id { get; set; }
    public int IntermediateBossId { get; set; }
    public IntermediateBoss? IntermediateBoss { get; set; }
    public string Content { get; set; } = "";
    public int SortOrder { get; set; }
}
