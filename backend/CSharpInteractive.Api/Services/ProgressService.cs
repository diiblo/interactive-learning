using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpInteractive.Api.Services;

public sealed class ProgressService(AppDbContext db, UnlockService unlockService, SkillProgressService skillProgressService)
{
    public async Task<UserProfile> GetProfileAsync()
    {
        var profile = await db.UserProfiles
            .Include(profile => profile.UserBadges)
            .ThenInclude(userBadge => userBadge.Badge)
            .FirstOrDefaultAsync();

        if (profile is not null)
        {
            await unlockService.RefreshUnlocksAsync(profile);
            return profile;
        }

        profile = new UserProfile();
        db.UserProfiles.Add(profile);
        await db.SaveChangesAsync();
        await unlockService.RefreshUnlocksAsync(profile);
        return profile;
    }

    public int CalculateLevel(int totalXp) => Math.Max(1, totalXp / 100 + 1);

    public async Task<SubmitResultDto> CompleteLessonAsync(
        UserProfile profile,
        Lesson lesson,
        string code,
        string output,
        IReadOnlyList<TestResultDto> tests,
        bool passed,
        ExecutionResultDto? execution = null)
    {
        return await CompleteLessonAsync(profile, lesson, code, output, tests, passed, execution, null);
    }

    public async Task<SubmitResultDto> CompleteLessonAsync(
        UserProfile profile,
        Lesson lesson,
        string code,
        string output,
        IReadOnlyList<TestResultDto> tests,
        bool passed,
        ExecutionResultDto? execution,
        SubmissionFeedbackDto? feedbackOverride)
    {
        var progress = await db.LessonProgress.FirstOrDefaultAsync(item => item.UserProfileId == profile.Id && item.LessonId == lesson.Id);
        if (progress is null)
        {
            progress = new LessonProgress { UserProfileId = profile.Id, LessonId = lesson.Id, Status = LessonProgressStatus.Available };
            db.LessonProgress.Add(progress);
        }

        progress.Attempts += 1;
        progress.LastOutput = output;

        var xpEarned = 0;
        if (passed)
        {
            progress.Status = LessonProgressStatus.Completed;
            progress.BestCode = code;
            progress.CompletedAt ??= DateTime.UtcNow;

            if (progress.EarnedXp == 0)
            {
                xpEarned = lesson.XpReward;
                progress.EarnedXp = lesson.XpReward;
                profile.TotalXp += xpEarned;
                profile.Level = CalculateLevel(profile.TotalXp);
                profile.UpdatedAt = DateTime.UtcNow;
            }
        }
        else if (progress.Status == LessonProgressStatus.Available)
        {
            progress.Status = LessonProgressStatus.Started;
        }

        await db.SaveChangesAsync();
        var structuredFeedback = feedbackOverride
            ?? await skillProgressService.UpdateAfterLessonSubmissionAsync(profile, lesson, tests, passed, execution ?? new ExecutionResultDto(passed, output, [], 0), code);
        var unlocked = passed ? await unlockService.RefreshUnlocksAsync(profile) : [];
        var earnedBadges = passed ? await AwardBadgesAsync(profile, lesson) : [];

        var bossResult = lesson.IsBossFinal
            ? await skillProgressService.BuildLessonBossResultAsync(profile, lesson, tests, passed)
            : null;

        return new SubmitResultDto(
            passed,
            output,
            tests,
            passed ? lesson.SuccessFeedback : lesson.FailureFeedback,
            xpEarned,
            profile.Level,
            unlocked,
            earnedBadges,
            structuredFeedback,
            bossResult);
    }

    public async Task<ProgressDto> GetProgressAsync()
    {
        var profile = await GetProfileAsync();
        var totalLessons = await db.Lessons.CountAsync(lesson => !lesson.IsBossFinal);
        var progress = await db.LessonProgress.Where(item => item.UserProfileId == profile.Id).ToListAsync();
        var completed = progress.Where(item => item.Status == LessonProgressStatus.Completed).Select(item => item.LessonId).ToList();
        var available = progress.Where(item => item.Status is LessonProgressStatus.Available or LessonProgressStatus.Started).Select(item => item.LessonId).ToList();

        return new ProgressDto(
            await ToProfileDtoAsync(profile),
            completed.Count,
            totalLessons,
            completed,
            available);
    }

    public async Task<ProfileDto> ToProfileDtoAsync(UserProfile profile)
    {
        var badges = await db.Badges.OrderBy(badge => badge.Id).ToListAsync();
        var earnedBadges = await db.UserBadges
            .Where(userBadge => userBadge.UserProfileId == profile.Id)
            .ToDictionaryAsync(userBadge => userBadge.BadgeId, userBadge => userBadge.EarnedAt);

        var badgeDtos = badges
            .Select(badge => new BadgeDto(
                badge.Id,
                badge.Slug,
                badge.Name,
                badge.Description,
                badge.IconName,
                earnedBadges.TryGetValue(badge.Id, out var earnedAt) ? earnedAt : null))
            .ToList();

        return new ProfileDto(profile.Id, profile.DisplayName, profile.TotalXp, profile.Level, badgeDtos);
    }

    private async Task<IReadOnlyList<BadgeDto>> AwardBadgesAsync(UserProfile profile, Lesson lesson)
    {
        var completedLessons = await db.LessonProgress.CountAsync(item => item.UserProfileId == profile.Id && item.Status == LessonProgressStatus.Completed);
        var lessonLanguage = lesson.Chapter?.Course?.Language;
        var completedLessonsByLanguage = lessonLanguage is null
            ? 0
            : await db.LessonProgress
                .Include(item => item.Lesson)
                .ThenInclude(item => item!.Chapter)
                .ThenInclude(item => item!.Course)
                .CountAsync(item => item.UserProfileId == profile.Id
                                    && item.Status == LessonProgressStatus.Completed
                                    && item.Lesson!.Chapter!.Course!.Language == lessonLanguage);
        var courseXp = lessonLanguage is null
            ? 0
            : await db.LessonProgress
                .Include(item => item.Lesson)
                .ThenInclude(item => item!.Chapter)
                .ThenInclude(item => item!.Course)
                .Where(item => item.UserProfileId == profile.Id
                               && item.Status == LessonProgressStatus.Completed
                               && item.Lesson!.Chapter!.Course!.Language == lessonLanguage)
                .SumAsync(item => item.EarnedXp);
        var existingBadgeIds = await db.UserBadges.Where(item => item.UserProfileId == profile.Id).Select(item => item.BadgeId).ToListAsync();
        var badges = await db.Badges.Where(item => !existingBadgeIds.Contains(item.Id)).ToListAsync();
        var earned = new List<Badge>();

        foreach (var badge in badges)
        {
            if (!string.IsNullOrWhiteSpace(badge.RuleCourseLanguage)
                && !string.Equals(badge.RuleCourseLanguage, lessonLanguage, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var matches = badge.RuleType switch
            {
                BadgeRuleType.CompleteLessons => completedLessons >= badge.RuleValue,
                BadgeRuleType.TotalXp => string.IsNullOrWhiteSpace(badge.RuleCourseLanguage)
                    ? profile.TotalXp >= badge.RuleValue
                    : courseXp >= badge.RuleValue,
                BadgeRuleType.CompleteBossFinal => lesson.IsBossFinal,
                BadgeRuleType.CompleteLessonInCourse => completedLessonsByLanguage >= badge.RuleValue,
                BadgeRuleType.CompleteBossFinalInCourse => lesson.IsBossFinal && !string.IsNullOrWhiteSpace(lessonLanguage),
                _ => false
            };

            if (!matches)
            {
                continue;
            }

            db.UserBadges.Add(new UserBadge { UserProfileId = profile.Id, BadgeId = badge.Id });
            earned.Add(badge);
        }

        await db.SaveChangesAsync();
        return earned.Select(badge => new BadgeDto(badge.Id, badge.Slug, badge.Name, badge.Description, badge.IconName, DateTime.UtcNow)).ToList();
    }
}
