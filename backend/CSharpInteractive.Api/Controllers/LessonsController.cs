using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Models;
using CSharpInteractive.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSharpInteractive.Api.Controllers;

[ApiController]
[Route("api/lessons")]
public sealed class LessonsController(
    AppDbContext db,
    LearningLanguageService languageService,
    ProgressService progressService,
    AiValidationService aiValidationService) : ControllerBase
{
    [HttpGet("{lessonId:int}")]
    public async Task<ActionResult<LessonDetailDto>> GetLesson(int lessonId)
    {
        var profile = await progressService.GetProfileAsync();
        var lesson = await db.Lessons
            .Include(lesson => lesson.Chapter)
            .ThenInclude(chapter => chapter!.Course)
            .Include(lesson => lesson.Tests)
            .Include(lesson => lesson.Hints)
            .Include(lesson => lesson.LessonSkills)
            .ThenInclude(item => item.Skill)
            .FirstOrDefaultAsync(lesson => lesson.Id == lessonId);
        if (lesson is null)
        {
            return NotFound();
        }

        var progress = await db.LessonProgress.FirstOrDefaultAsync(item => item.UserProfileId == profile.Id && item.LessonId == lesson.Id);
        var status = progress?.Status ?? LessonProgressStatus.Locked;
        if (status == LessonProgressStatus.Locked)
        {
            return Forbid();
        }

        return ToDetailDto(lesson, status);
    }

    [HttpPost("{lessonId:int}/run")]
    public async Task<ActionResult<ExecutionResultDto>> Run(int lessonId, CodeRequest request)
    {
        var access = await GetAccessibleLessonAsync(lessonId);
        if (access.Error is not null)
        {
            return access.Error;
        }

        var lesson = access.Lesson!;
        return await languageService.GetRequiredHandler(lesson).RunAsync(request.Code);
    }

    [HttpPost("{lessonId:int}/submit")]
    public async Task<ActionResult<SubmitResultDto>> Submit(int lessonId, SubmitCodeRequest request)
    {
        var access = await GetAccessibleLessonAsync(lessonId);
        if (access.Error is not null)
        {
            return access.Error;
        }

        var profile = await progressService.GetProfileAsync();
        var lesson = access.Lesson!;
        if (string.Equals(request.ValidationMode, "ai", StringComparison.OrdinalIgnoreCase))
        {
            var aiResult = await aiValidationService.ValidateLessonAsync(lesson, request.Code, request.AiProviders);
            var tests = new[] { new TestResultDto($"Validation IA ({aiResult.ProviderName})", aiResult.Passed, aiResult.Feedback) };
            var execution = new ExecutionResultDto(aiResult.Passed, aiResult.Feedback, [], 0);
            return await progressService.CompleteLessonAsync(profile, lesson, request.Code, aiResult.Feedback, tests, aiResult.Passed, execution, AiSubmissionFeedbackFactory.Create(aiResult));
        }

        var correction = await languageService.GetRequiredHandler(lesson).SubmitAsync(lesson, request.Code);
        return await progressService.CompleteLessonAsync(profile, lesson, request.Code, correction.Execution.Output, correction.Tests, correction.Passed, correction.Execution);
    }

    private async Task<(Lesson? Lesson, ActionResult? Error)> GetAccessibleLessonAsync(int lessonId)
    {
        var profile = await progressService.GetProfileAsync();
        var lesson = await db.Lessons
            .Include(lesson => lesson.Chapter)
            .ThenInclude(chapter => chapter!.Course)
            .Include(lesson => lesson.Tests)
            .Include(lesson => lesson.Hints)
            .Include(lesson => lesson.LessonSkills)
            .ThenInclude(item => item.Skill)
            .FirstOrDefaultAsync(lesson => lesson.Id == lessonId);
        if (lesson is null)
        {
            return (null, new NotFoundResult());
        }

        var progress = await db.LessonProgress.FirstOrDefaultAsync(item => item.UserProfileId == profile.Id && item.LessonId == lesson.Id);
        if ((progress?.Status ?? LessonProgressStatus.Locked) == LessonProgressStatus.Locked)
        {
            return (null, new ForbidResult());
        }

        return (lesson, null);
    }

    private LessonDetailDto ToDetailDto(Lesson lesson, LessonProgressStatus status) =>
        new(
            lesson.Id,
            lesson.Slug,
            lesson.Title,
            languageService.GetRequiredHandler(lesson).EditorLanguage,
            lesson.Objective,
            lesson.ConceptSummary,
            lesson.CommonMistakes,
            lesson.Explanation,
            lesson.ExampleCode,
            lesson.ExercisePrompt,
            lesson.StarterCode,
            lesson.SuccessFeedback,
            lesson.FailureFeedback,
            "",
            lesson.XpReward,
            lesson.IsBossFinal,
            status,
            lesson.Hints.OrderBy(hint => hint.HintLevel).Select(hint => new LessonHintDto(hint.HintLevel, hint.Content)).ToList(),
            lesson.LessonSkills
                .Where(item => item.Skill is not null)
                .Select(item => new SkillDto(item.Skill!.Id, item.Skill.CourseLanguage, item.Skill.Slug, item.Skill.Name, item.Skill.Description))
                .ToList());
}
