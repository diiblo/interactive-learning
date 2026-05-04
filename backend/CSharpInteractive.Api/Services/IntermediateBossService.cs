using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpInteractive.Api.Services;

public sealed class IntermediateBossService(
    AppDbContext db,
    LearningLanguageService languageService,
    ProgressService progressService,
    UnlockService unlockService,
    SkillProgressService skillProgressService,
    AiValidationService aiValidationService)
{
    public Task<ExecutionResultDto> RunAsync(IntermediateBoss boss, string code) =>
        languageService.GetRequiredHandler(boss).RunAsync(code);

    public async Task<SubmitResultDto> SubmitAsync(UserProfile profile, IntermediateBoss boss, string code)
    {
        var execution = await RunAsync(boss, code);
        var results = new List<TestResultDto>();

        if (!execution.Success)
        {
            results.Add(new TestResultDto(
                $"{languageService.GetRequiredHandler(boss).EditorLanguage} execution",
                false,
                string.Join("\n", execution.Diagnostics)));
        }
        else
        {
            foreach (var rule in boss.ValidationRules.OrderBy(rule => rule.SortOrder).ThenBy(rule => rule.Id))
            {
                results.Add(await languageService.GetRequiredHandler(boss).EvaluateBossRuleAsync(rule, code, execution));
            }
        }

        var passed = results.Count > 0 && results.All(result => result.Passed);
        var progress = await GetOrCreateProgressAsync(profile, boss);
        progress.Attempts += 1;
        progress.LastOutput = execution.Output;

        var xpEarned = 0;
        if (passed)
        {
            progress.Status = LessonProgressStatus.Completed;
            progress.BestCode = code;
            progress.CompletedAt ??= DateTime.UtcNow;

            if (progress.EarnedXp == 0)
            {
                xpEarned = boss.XpReward;
                progress.EarnedXp = boss.XpReward;
                profile.TotalXp += xpEarned;
                profile.Level = progressService.CalculateLevel(profile.TotalXp);
                profile.UpdatedAt = DateTime.UtcNow;
            }
        }
        else
        {
            progress.FailedAttempts += 1;
            if (progress.Status == LessonProgressStatus.Available)
            {
                progress.Status = LessonProgressStatus.Started;
            }
        }

        await db.SaveChangesAsync();
        var unlocked = passed ? await unlockService.RefreshUnlocksAsync(profile) : [];
        var feedback = passed
            ? "Monstre vaincu. Le module suivant peut maintenant etre deverrouille."
            : "Le monstre resiste encore. Corrige les erreurs indiquees, puis retente.";
        var bossResult = await skillProgressService.BuildBossResultAsync(profile, boss, results, passed);

        return new SubmitResultDto(passed, execution.Output, results, feedback, xpEarned, profile.Level, unlocked, [], BossResult: bossResult);
    }

    public async Task<SubmitResultDto> SubmitWithAiAsync(UserProfile profile, IntermediateBoss boss, string code, IReadOnlyList<AiProviderConfigDto>? providers)
    {
        var aiResult = await aiValidationService.ValidateIntermediateBossAsync(boss, code, providers);
        var execution = new ExecutionResultDto(aiResult.Passed, aiResult.Feedback, [], 0);
        var results = new List<TestResultDto>
        {
            new($"Validation IA ({aiResult.ProviderName})", aiResult.Passed, aiResult.Feedback)
        };

        var passed = aiResult.Passed;
        var progress = await GetOrCreateProgressAsync(profile, boss);
        progress.Attempts += 1;
        progress.LastOutput = aiResult.Feedback;

        var xpEarned = 0;
        if (passed)
        {
            progress.Status = LessonProgressStatus.Completed;
            progress.BestCode = code;
            progress.CompletedAt ??= DateTime.UtcNow;

            if (progress.EarnedXp == 0)
            {
                xpEarned = boss.XpReward;
                progress.EarnedXp = boss.XpReward;
                profile.TotalXp += xpEarned;
                profile.Level = progressService.CalculateLevel(profile.TotalXp);
                profile.UpdatedAt = DateTime.UtcNow;
            }
        }
        else
        {
            progress.FailedAttempts += 1;
            if (progress.Status == LessonProgressStatus.Available)
            {
                progress.Status = LessonProgressStatus.Started;
            }
        }

        await db.SaveChangesAsync();
        var unlocked = passed ? await unlockService.RefreshUnlocksAsync(profile) : [];
        var feedback = passed
            ? "Monstre vaincu avec validation IA. Le module suivant peut maintenant etre deverrouille."
            : "La validation IA refuse encore la reponse. Corrige le point indique, puis retente.";
        var bossResult = await skillProgressService.BuildBossResultAsync(profile, boss, results, passed);

        return new SubmitResultDto(
            passed,
            execution.Output,
            results,
            feedback,
            xpEarned,
            profile.Level,
            unlocked,
            [],
            AiSubmissionFeedbackFactory.Create(aiResult),
            bossResult);
    }

    public async Task<IntermediateBossHintResultDto> RevealNextHintAsync(UserProfile profile, IntermediateBoss boss)
    {
        var progress = await GetOrCreateProgressAsync(profile, boss);
        var hints = boss.Hints.OrderBy(hint => hint.SortOrder).ThenBy(hint => hint.Id).ToList();
        if (progress.HintsRevealed < hints.Count)
        {
            progress.HintsRevealed += 1;
            await db.SaveChangesAsync();
        }

        var revealed = hints
            .Take(progress.HintsRevealed)
            .Select((hint, index) => new IntermediateBossHintDto(index + 1, hint.Content))
            .ToList();

        return new IntermediateBossHintResultDto(revealed, progress.HintsRevealed < hints.Count);
    }

    public async Task<IntermediateBossSolutionDto?> RevealSolutionAsync(UserProfile profile, IntermediateBoss boss)
    {
        var progress = await db.IntermediateBossProgress
            .FirstOrDefaultAsync(item => item.UserProfileId == profile.Id && item.IntermediateBossId == boss.Id);

        return progress?.FailedAttempts > 0 ? new IntermediateBossSolutionDto(boss.Solution) : null;
    }

    private async Task<IntermediateBossProgress> GetOrCreateProgressAsync(UserProfile profile, IntermediateBoss boss)
    {
        var progress = await db.IntermediateBossProgress
            .FirstOrDefaultAsync(item => item.UserProfileId == profile.Id && item.IntermediateBossId == boss.Id);

        if (progress is not null)
        {
            return progress;
        }

        progress = new IntermediateBossProgress
        {
            UserProfileId = profile.Id,
            IntermediateBossId = boss.Id,
            Status = LessonProgressStatus.Available
        };
        db.IntermediateBossProgress.Add(progress);
        return progress;
    }

}
