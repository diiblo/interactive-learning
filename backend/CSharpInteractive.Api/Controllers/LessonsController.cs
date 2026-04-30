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
    RoslynExecutionService executionService,
    LessonCorrectionService correctionService,
    ProgressService progressService) : ControllerBase
{
    [HttpGet("{lessonId:int}")]
    public async Task<ActionResult<LessonDetailDto>> GetLesson(int lessonId)
    {
        var profile = await progressService.GetProfileAsync();
        var lesson = await db.Lessons
            .Include(lesson => lesson.Chapter)
            .ThenInclude(chapter => chapter!.Course)
            .Include(lesson => lesson.Tests)
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

        return await executionService.ExecuteAsync(request.Code);
    }

    [HttpPost("{lessonId:int}/submit")]
    public async Task<ActionResult<SubmitResultDto>> Submit(int lessonId, CodeRequest request)
    {
        var access = await GetAccessibleLessonAsync(lessonId);
        if (access.Error is not null)
        {
            return access.Error;
        }

        var profile = await progressService.GetProfileAsync();
        var lesson = access.Lesson!;
        var correction = await correctionService.SubmitAsync(lesson, request.Code);
        return await progressService.CompleteLessonAsync(profile, lesson, request.Code, correction.Execution.Output, correction.Tests, correction.Passed);
    }

    private async Task<(Lesson? Lesson, ActionResult? Error)> GetAccessibleLessonAsync(int lessonId)
    {
        var profile = await progressService.GetProfileAsync();
        var lesson = await db.Lessons
            .Include(lesson => lesson.Chapter)
            .ThenInclude(chapter => chapter!.Course)
            .Include(lesson => lesson.Tests)
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

    private static LessonDetailDto ToDetailDto(Lesson lesson, LessonProgressStatus status) =>
        new(
            lesson.Id,
            lesson.Slug,
            lesson.Title,
            lesson.Chapter?.Course?.Language == "sqlserver" ? "sql" : "csharp",
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
