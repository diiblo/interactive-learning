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

        var progress = await db.LessonProgress
            .Where(item => item.UserProfileId == profile.Id)
            .ToDictionaryAsync(item => item.LessonId);

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
                var previousLessons = lessons.Where(item =>
                    item.Chapter!.CourseId == courseId &&
                    !item.IsBossFinal &&
                    (item.Chapter!.SortOrder < lesson.Chapter!.SortOrder ||
                     item.Chapter.SortOrder == lesson.Chapter.SortOrder && item.SortOrder < lesson.SortOrder));

                shouldBeAvailable = !previousLessons.Any() || previousLessons.All(item =>
                    progress.TryGetValue(item.Id, out var itemProgress) && itemProgress.Status == LessonProgressStatus.Completed);
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

        await db.SaveChangesAsync();
        return unlocked;
    }
}
