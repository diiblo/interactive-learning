using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Models;
using CSharpInteractive.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSharpInteractive.Api.Controllers;

[ApiController]
[Route("api/boss-final")]
public sealed class BossFinalController(
    AppDbContext db,
    LearningLanguageService languageService,
    ProgressService progressService) : ControllerBase
{
    [HttpPost("run")]
    public async Task<ActionResult<ExecutionResultDto>> Run(CodeRequest request)
    {
        var access = await GetAccessibleBossAsync();
        if (access.Error is not null)
        {
            return access.Error;
        }

        return await languageService.GetRequiredHandler(access.Lesson!).RunAsync(request.Code);
    }

    [HttpPost("submit")]
    public async Task<ActionResult<SubmitResultDto>> Submit(CodeRequest request)
    {
        var access = await GetAccessibleBossAsync();
        if (access.Error is not null)
        {
            return access.Error;
        }

        var profile = await progressService.GetProfileAsync();
        var lesson = access.Lesson!;
        var correction = await languageService.GetRequiredHandler(lesson).SubmitAsync(lesson, request.Code);
        return await progressService.CompleteLessonAsync(profile, lesson, request.Code, correction.Execution.Output, correction.Tests, correction.Passed, correction.Execution);
    }

    private async Task<(Lesson? Lesson, ActionResult? Error)> GetAccessibleBossAsync()
    {
        var profile = await progressService.GetProfileAsync();
        var lesson = await db.Lessons
            .Include(lesson => lesson.Chapter)
            .ThenInclude(chapter => chapter!.Course)
            .Include(lesson => lesson.Tests)
            .FirstOrDefaultAsync(lesson => lesson.IsBossFinal && lesson.Chapter!.Course!.Language == "csharp");
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
}
