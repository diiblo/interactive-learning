using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Models;
using CSharpInteractive.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSharpInteractive.Api.Controllers;

[ApiController]
[Route("api/intermediate-bosses")]
public sealed class IntermediateBossesController(
    AppDbContext db,
    IntermediateBossService intermediateBossService,
    LearningLanguageService languageService,
    ProgressService progressService) : ControllerBase
{
    [HttpGet("modules/{moduleId:int}")]
    public async Task<ActionResult<IntermediateBossDetailDto>> GetByModule(int moduleId)
    {
        var access = await GetAccessibleBossByModuleAsync(moduleId);
        if (access.Error is not null)
        {
            return access.Error;
        }

        return await ToDetailDtoAsync(access.Profile!, access.Boss!);
    }

    [HttpPost("{bossId:int}/run")]
    public async Task<ActionResult<ExecutionResultDto>> Run(int bossId, CodeRequest request)
    {
        var access = await GetAccessibleBossAsync(bossId);
        if (access.Error is not null)
        {
            return access.Error;
        }

        return await intermediateBossService.RunAsync(access.Boss!, request.Code);
    }

    [HttpPost("{bossId:int}/submit")]
    public async Task<ActionResult<SubmitResultDto>> Submit(int bossId, SubmitCodeRequest request)
    {
        var access = await GetAccessibleBossAsync(bossId);
        if (access.Error is not null)
        {
            return access.Error;
        }

        return string.Equals(request.ValidationMode, "ai", StringComparison.OrdinalIgnoreCase)
            ? await intermediateBossService.SubmitWithAiAsync(access.Profile!, access.Boss!, request.Code, request.AiProviders)
            : await intermediateBossService.SubmitAsync(access.Profile!, access.Boss!, request.Code);
    }

    [HttpPost("{bossId:int}/hint")]
    public async Task<ActionResult<IntermediateBossHintResultDto>> RevealHint(int bossId)
    {
        var access = await GetAccessibleBossAsync(bossId);
        if (access.Error is not null)
        {
            return access.Error;
        }

        return await intermediateBossService.RevealNextHintAsync(access.Profile!, access.Boss!);
    }

    [HttpPost("{bossId:int}/solution")]
    public async Task<ActionResult<IntermediateBossSolutionDto>> RevealSolution(int bossId)
    {
        var access = await GetAccessibleBossAsync(bossId);
        if (access.Error is not null)
        {
            return access.Error;
        }

        var solution = await intermediateBossService.RevealSolutionAsync(access.Profile!, access.Boss!);
        return solution is null ? Forbid() : solution;
    }

    private async Task<(IntermediateBoss? Boss, UserProfile? Profile, ActionResult? Error)> GetAccessibleBossByModuleAsync(int moduleId)
    {
        var boss = await QueryBosses().FirstOrDefaultAsync(item => item.ModuleId == moduleId);
        if (boss is null)
        {
            return (null, null, new NotFoundResult());
        }

        return await GetAccessibleBossAsync(boss.Id);
    }

    private async Task<(IntermediateBoss? Boss, UserProfile? Profile, ActionResult? Error)> GetAccessibleBossAsync(int bossId)
    {
        var profile = await progressService.GetProfileAsync();
        var boss = await QueryBosses().FirstOrDefaultAsync(item => item.Id == bossId);
        if (boss is null)
        {
            return (null, null, new NotFoundResult());
        }

        var progress = await db.IntermediateBossProgress
            .FirstOrDefaultAsync(item => item.UserProfileId == profile.Id && item.IntermediateBossId == boss.Id);

        if ((progress?.Status ?? LessonProgressStatus.Locked) == LessonProgressStatus.Locked)
        {
            return (null, null, new ForbidResult());
        }

        return (boss, profile, null);
    }

    private IQueryable<IntermediateBoss> QueryBosses() =>
        db.IntermediateBosses
            .Include(boss => boss.Module)
            .ThenInclude(module => module!.Course)
            .Include(boss => boss.ValidationRules)
            .Include(boss => boss.Hints);

    private async Task<IntermediateBossDetailDto> ToDetailDtoAsync(UserProfile profile, IntermediateBoss boss)
    {
        var progress = await db.IntermediateBossProgress
            .FirstOrDefaultAsync(item => item.UserProfileId == profile.Id && item.IntermediateBossId == boss.Id);

        var validationRules = boss.ValidationRules
            .OrderBy(rule => rule.SortOrder)
            .ThenBy(rule => rule.Id)
            .Select(rule => rule.Name)
            .ToList();

        return new IntermediateBossDetailDto(
            boss.Id,
            boss.ModuleId,
            boss.Slug,
            boss.Title,
            languageService.GetRequiredHandler(boss).EditorLanguage,
            boss.Objective,
            boss.Instructions,
            boss.StarterCode,
            boss.ExpectedResult,
            validationRules,
            boss.XpReward,
            boss.IsRequiredToUnlockNextModule,
            progress?.Status ?? LessonProgressStatus.Available,
            progress?.Attempts ?? 0,
            progress?.FailedAttempts ?? 0,
            progress?.HintsRevealed ?? 0,
            (progress?.FailedAttempts ?? 0) > 0);
    }
}
