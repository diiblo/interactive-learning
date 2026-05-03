namespace CSharpInteractive.Api.Models;

public sealed class Skill
{
    public int Id { get; set; }
    public string CourseLanguage { get; set; } = "";
    public string Slug { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public List<LessonSkill> LessonSkills { get; set; } = [];
}
