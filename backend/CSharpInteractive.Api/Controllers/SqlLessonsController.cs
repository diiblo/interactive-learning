using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Models;
using CSharpInteractive.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSharpInteractive.Api.Controllers;

[ApiController]
[Route("api/sql/lessons")]
public sealed class SqlLessonsController(
    AppDbContext db,
    SqlExecutionService executionService,
    LearningLanguageService languageService,
    ProgressService progressService) : ControllerBase
{
    [HttpGet("schema")]
    public ActionResult<SqlSchemaDto> GetSchema() => executionService.GetSchema();

    [HttpGet("{lessonId:int}")]
    public async Task<ActionResult<LessonDetailDto>> GetLesson(int lessonId)
    {
        var access = await GetAccessibleSqlLessonAsync(lessonId);
        if (access.Error is not null)
        {
            return access.Error;
        }

        var lesson = access.Lesson!;
        var profile = await progressService.GetProfileAsync();
        var progress = await db.LessonProgress.FirstOrDefaultAsync(item => item.UserProfileId == profile.Id && item.LessonId == lesson.Id);
        return ToDetailDto(lesson, progress?.Status ?? LessonProgressStatus.Available);
    }

    [HttpPost("{lessonId:int}/run")]
    public async Task<ActionResult<ExecutionResultDto>> Run(int lessonId, CodeRequest request)
    {
        var access = await GetAccessibleSqlLessonAsync(lessonId);
        if (access.Error is not null)
        {
            return access.Error;
        }

        return await languageService.GetRequiredHandler(access.Lesson!).RunAsync(request.Code);
    }

    [HttpPost("{lessonId:int}/submit")]
    public async Task<ActionResult<SubmitResultDto>> Submit(int lessonId, CodeRequest request)
    {
        var access = await GetAccessibleSqlLessonAsync(lessonId);
        if (access.Error is not null)
        {
            return access.Error;
        }

        var profile = await progressService.GetProfileAsync();
        var lesson = access.Lesson!;
        var correction = await languageService.GetRequiredHandler(lesson).SubmitAsync(lesson, request.Code);
        return await progressService.CompleteLessonAsync(profile, lesson, request.Code, correction.Execution.Output, correction.Tests, correction.Passed, correction.Execution);
    }

    [HttpPost("{lessonId:int}/reset")]
    public ActionResult Reset(int lessonId) => Ok(new { lessonId, reset = true });

    private async Task<(Lesson? Lesson, ActionResult? Error)> GetAccessibleSqlLessonAsync(int lessonId)
    {
        var profile = await progressService.GetProfileAsync();
        var lesson = await db.Lessons
            .Include(lesson => lesson.Chapter)
            .ThenInclude(chapter => chapter!.Course)
            .Include(lesson => lesson.Tests)
            .Include(lesson => lesson.Hints)
            .Include(lesson => lesson.LessonSkills)
            .ThenInclude(item => item.Skill)
            .FirstOrDefaultAsync(lesson => lesson.Id == lessonId && lesson.Chapter!.Course!.Language == "sqlserver");

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

    private static LessonDetailDto ToDetailDto(Lesson lesson, LessonProgressStatus status) =>
        new(
            lesson.Id,
            lesson.Slug,
            lesson.Title,
            "sql",
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
