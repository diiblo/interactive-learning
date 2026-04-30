namespace CSharpInteractive.Api.Models;

public enum LessonTestType
{
    ExpectedOutput = 1,
    RequiredSnippet = 2,
    HiddenCode = 3,
    MinSnippetCount = 4,
    SqlExpectedColumns = 5,
    SqlExpectedRowCount = 6,
    SqlForbiddenSnippet = 7
}

public sealed class LessonTest
{
    public int Id { get; set; }
    public int LessonId { get; set; }
    public Lesson? Lesson { get; set; }
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
