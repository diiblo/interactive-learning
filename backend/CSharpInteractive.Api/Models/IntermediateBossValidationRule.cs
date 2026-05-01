namespace CSharpInteractive.Api.Models;

public sealed class IntermediateBossValidationRule
{
    public int Id { get; set; }
    public int IntermediateBossId { get; set; }
    public IntermediateBoss? IntermediateBoss { get; set; }
    public string Name { get; set; } = "";
    public LessonTestType TestType { get; set; }
    public string? ExpectedOutput { get; set; }
    public string? RequiredSnippet { get; set; }
    public string? HiddenCode { get; set; }
    public int? MinCount { get; set; }
    public string? ExpectedColumns { get; set; }
    public int? ExpectedRowCount { get; set; }
    public int SortOrder { get; set; }
}
