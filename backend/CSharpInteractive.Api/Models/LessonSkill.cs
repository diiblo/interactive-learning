namespace CSharpInteractive.Api.Models;

public sealed class LessonSkill
{
    public int LessonId { get; set; }
    public Lesson? Lesson { get; set; }
    public int SkillId { get; set; }
    public Skill? Skill { get; set; }
    public int Weight { get; set; } = 1;
}
