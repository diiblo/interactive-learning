namespace CSharpInteractive.Api.Models;

public sealed class Course
{
    public int Id { get; set; }
    public string Slug { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Language { get; set; } = "";
    public int SortOrder { get; set; }
    public List<Chapter> Chapters { get; set; } = [];
}
