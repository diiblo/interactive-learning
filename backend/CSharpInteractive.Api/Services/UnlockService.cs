using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpInteractive.Api.Services;

public sealed class UnlockService(AppDbContext db)
{
    public async Task<IReadOnlyList<int>> RefreshUnlocksAsync(UserProfile profile)
    {
        var unlocked = new List<int>();
        var lessons = await db.Lessons
            .Include(lesson => lesson.Chapter)
            .ThenInclude(chapter => chapter!.Course)
            .OrderBy(lesson => lesson.Chapter!.SortOrder)
            .ThenBy(lesson => lesson.SortOrder)
            .ToListAsync();

        var intermediateBosses = await db.IntermediateBosses
            .Include(boss => boss.Module)
            .ThenInclude(module => module!.Course)
            .OrderBy(boss => boss.Module!.Course!.SortOrder)
            .ThenBy(boss => boss.Module!.SortOrder)
            .ToListAsync();

        var progress = await db.LessonProgress
            .Where(item => item.UserProfileId == profile.Id)
            .ToDictionaryAsync(item => item.LessonId);

        var bossProgress = await db.IntermediateBossProgress
            .Where(item => item.UserProfileId == profile.Id)
            .ToDictionaryAsync(item => item.IntermediateBossId);

        foreach (var lesson in lessons)
        {
            var courseId = lesson.Chapter!.CourseId;
            var shouldBeAvailable = false;
            if (lesson.IsBossFinal)
            {
                shouldBeAvailable = lessons.Where(item => item.Chapter!.CourseId == courseId && item.IsBossPrerequisite).All(item =>
                    progress.TryGetValue(item.Id, out var itemProgress) && itemProgress.Status == LessonProgressStatus.Completed);
            }
            else if (lesson.Chapter is not null && lesson.Chapter.RequiredXp <= profile.TotalXp)
            {
                var previousRequiredBosses = intermediateBosses.Where(item =>
                    item.IsRequiredToUnlockNextModule &&
                    item.Module!.CourseId == courseId &&
                    item.Module.SortOrder < lesson.Chapter!.SortOrder);

                var previousLessons = lessons.Where(item =>
                    item.Chapter!.CourseId == courseId &&
                    !item.IsBossFinal &&
                    (item.Chapter!.SortOrder < lesson.Chapter!.SortOrder ||
                     item.Chapter.SortOrder == lesson.Chapter.SortOrder && item.SortOrder < lesson.SortOrder));

                shouldBeAvailable =
                    previousRequiredBosses.All(item =>
                        bossProgress.TryGetValue(item.Id, out var itemProgress) && itemProgress.Status == LessonProgressStatus.Completed) &&
                    (!previousLessons.Any() || previousLessons.All(item =>
                        progress.TryGetValue(item.Id, out var itemProgress) && itemProgress.Status == LessonProgressStatus.Completed));
            }

            if (!progress.TryGetValue(lesson.Id, out var existing))
            {
                if (shouldBeAvailable)
                {
                    db.LessonProgress.Add(new LessonProgress
                    {
                        UserProfileId = profile.Id,
                        LessonId = lesson.Id,
                        Status = LessonProgressStatus.Available
                    });
                    unlocked.Add(lesson.Id);
                }

                continue;
            }

            if (shouldBeAvailable && existing.Status == LessonProgressStatus.Locked)
            {
                existing.Status = LessonProgressStatus.Available;
                unlocked.Add(lesson.Id);
            }
        }

        foreach (var boss in intermediateBosses)
        {
            var moduleLessons = lessons.Where(lesson =>
                lesson.ChapterId == boss.ModuleId &&
                !lesson.IsBossFinal);

            var previousRequiredBosses = intermediateBosses.Where(item =>
                item.IsRequiredToUnlockNextModule &&
                item.Module!.CourseId == boss.Module!.CourseId &&
                item.Module.SortOrder < boss.Module.SortOrder);

            var shouldBeAvailable =
                previousRequiredBosses.All(item =>
                    bossProgress.TryGetValue(item.Id, out var itemProgress) && itemProgress.Status == LessonProgressStatus.Completed) &&
                moduleLessons.Any() &&
                moduleLessons.All(lesson =>
                    progress.TryGetValue(lesson.Id, out var lessonProgress) && lessonProgress.Status == LessonProgressStatus.Completed);

            if (!bossProgress.TryGetValue(boss.Id, out var existing))
            {
                if (shouldBeAvailable)
                {
                    db.IntermediateBossProgress.Add(new IntermediateBossProgress
                    {
                        UserProfileId = profile.Id,
                        IntermediateBossId = boss.Id,
                        Status = LessonProgressStatus.Available
                    });
                }

                continue;
            }

            if (shouldBeAvailable && existing.Status == LessonProgressStatus.Locked)
            {
                existing.Status = LessonProgressStatus.Available;
            }
        }

        await db.SaveChangesAsync();
        return unlocked;
    }
}
