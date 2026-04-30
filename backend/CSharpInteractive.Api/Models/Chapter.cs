namespace CSharpInteractive.Api.Models;

public sealed class Chapter
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public Course? Course { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public int SortOrder { get; set; }
    public int RequiredXp { get; set; }
    public List<Lesson> Lessons { get; set; } = [];
}
