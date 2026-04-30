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
    SqlCorrectionService correctionService,
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
    public async Task<ActionResult<SqlExecutionResultDto>> Run(int lessonId, CodeRequest request)
    {
        var access = await GetAccessibleSqlLessonAsync(lessonId);
        if (access.Error is not null)
        {
            return access.Error;
        }

        var result = await executionService.ExecuteQueryAsync(request.Code);
        return new SqlExecutionResultDto(result.Success, result.Output, result.Diagnostics, result.DurationMs, result.Columns, result.Rows);
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
        var correction = await correctionService.SubmitAsync(lesson, request.Code);
        return await progressService.CompleteLessonAsync(profile, lesson, request.Code, correction.Execution.Output, correction.Tests, correction.Passed);
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
            lesson.FinalCorrection,
            lesson.XpReward,
            lesson.IsBossFinal,
            status);
}
