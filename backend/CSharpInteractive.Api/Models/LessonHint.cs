namespace CSharpInteractive.Api.Models;

public sealed class LessonHint
{
    public int Id { get; set; }
    public int LessonId { get; set; }
    public Lesson? Lesson { get; set; }
    public int HintLevel { get; set; }
    public string Content { get; set; } = "";
}
