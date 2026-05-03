using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpInteractive.Api.Services;

public sealed class CourseCatalogService(AppDbContext db, ProgressService progressService)
{
    public async Task<IReadOnlyList<CourseSummaryDto>> GetCoursesAsync()
    {
        return await db.Courses
            .AsNoTracking()
            .OrderBy(course => course.SortOrder)
            .Select(course => new CourseSummaryDto(course.Id, course.Slug, course.Title, course.Description, course.Language))
            .ToListAsync();
    }

    public async Task<CourseMapDto?> GetMapAsync(int courseId)
    {
        var profile = await progressService.GetProfileAsync();
        var course = await db.Courses
            .AsNoTracking()
            .Include(course => course.Chapters.OrderBy(chapter => chapter.SortOrder))
            .ThenInclude(chapter => chapter.Lessons.OrderBy(lesson => lesson.SortOrder))
            .Include(course => course.Chapters)
            .ThenInclude(chapter => chapter.IntermediateBoss)
            .FirstOrDefaultAsync(course => course.Id == courseId);

        if (course is null)
        {
            return null;
        }

        var progress = await db.LessonProgress
            .AsNoTracking()
            .Where(item => item.UserProfileId == profile.Id)
            .ToDictionaryAsync(item => item.LessonId);

        var bossProgress = await db.IntermediateBossProgress
            .AsNoTracking()
            .Where(item => item.UserProfileId == profile.Id)
            .ToDictionaryAsync(item => item.IntermediateBossId);

        var chapters = course.Chapters
            .OrderBy(chapter => chapter.SortOrder)
            .Select(chapter => new ChapterMapDto(
                chapter.Id,
                chapter.Title,
                chapter.Description,
                chapter.RequiredXp,
                chapter.Lessons
                    .Where(lesson => !lesson.IsBossFinal)
                    .OrderBy(lesson => lesson.SortOrder)
                    .Select(lesson => ToMapItem(lesson, progress))
                    .ToList(),
                chapter.IntermediateBoss is null ? null : ToIntermediateBossMapItem(chapter.IntermediateBoss, bossProgress)))
            .Where(chapter => chapter.Lessons.Any())
            .ToList();

        var boss = course.Chapters
            .SelectMany(chapter => chapter.Lessons)
            .Where(lesson => lesson.IsBossFinal)
            .Select(lesson => ToMapItem(lesson, progress))
            .FirstOrDefault();

        return new CourseMapDto(course.Id, course.Title, chapters, boss);
    }

    private static LessonMapItemDto ToMapItem(Lesson lesson, IReadOnlyDictionary<int, LessonProgress> progress)
    {
        var status = progress.TryGetValue(lesson.Id, out var item) ? item.Status : LessonProgressStatus.Locked;
        return new LessonMapItemDto(lesson.Id, lesson.Slug, lesson.Title, lesson.XpReward, status, status == LessonProgressStatus.Locked, lesson.IsBossFinal);
    }

    private static IntermediateBossMapItemDto ToIntermediateBossMapItem(
        IntermediateBoss boss,
        IReadOnlyDictionary<int, IntermediateBossProgress> progress)
    {
        var status = progress.TryGetValue(boss.Id, out var item) ? item.Status : LessonProgressStatus.Locked;
        return new IntermediateBossMapItemDto(
            boss.Id,
            boss.ModuleId,
            boss.Slug,
            boss.Title,
            boss.XpReward,
            status,
            status == LessonProgressStatus.Locked,
            boss.IsRequiredToUnlockNextModule);
    }
}
