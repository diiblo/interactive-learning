namespace CSharpInteractive.Api.Models;

public sealed class Lesson
{
    public int Id { get; set; }
    public int ChapterId { get; set; }
    public Chapter? Chapter { get; set; }
    public string Slug { get; set; } = "";
    public string Title { get; set; } = "";
    public string Objective { get; set; } = "";
    public string ConceptSummary { get; set; } = "";
    public string CommonMistakes { get; set; } = "";
    public string Explanation { get; set; } = "";
    public string ExampleCode { get; set; } = "";
    public string ExercisePrompt { get; set; } = "";
    public string StarterCode { get; set; } = "";
    public string SuccessFeedback { get; set; } = "";
    public string FailureFeedback { get; set; } = "";
    public string FinalCorrection { get; set; } = "";
    public int XpReward { get; set; }
    public int SortOrder { get; set; }
    public bool IsBossPrerequisite { get; set; } = true;
    public bool IsBossFinal { get; set; }
    public List<LessonTest> Tests { get; set; } = [];
    public List<LessonSkill> LessonSkills { get; set; } = [];
    public List<LessonHint> Hints { get; set; } = [];
}
