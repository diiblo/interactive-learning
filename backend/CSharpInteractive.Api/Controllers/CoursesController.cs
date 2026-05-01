using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Models;
using CSharpInteractive.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSharpInteractive.Api.Controllers;

[ApiController]
[Route("api/courses")]
public sealed class CoursesController(AppDbContext db, ProgressService progressService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CourseSummaryDto>>> GetCourses()
    {
        return await db.Courses
            .OrderBy(course => course.SortOrder)
            .Select(course => new CourseSummaryDto(course.Id, course.Slug, course.Title, course.Description, course.Language))
            .ToListAsync();
    }

    [HttpGet("{courseId:int}/map")]
    public async Task<ActionResult<CourseMapDto>> GetMap(int courseId)
    {
        var profile = await progressService.GetProfileAsync();
        var course = await db.Courses
            .Include(course => course.Chapters.OrderBy(chapter => chapter.SortOrder))
            .ThenInclude(chapter => chapter.Lessons.OrderBy(lesson => lesson.SortOrder))
            .Include(course => course.Chapters)
            .ThenInclude(chapter => chapter.IntermediateBoss)
            .FirstOrDefaultAsync(course => course.Id == courseId);

        if (course is null)
        {
            return NotFound();
        }

        var progress = await db.LessonProgress
            .Where(item => item.UserProfileId == profile.Id)
            .ToDictionaryAsync(item => item.LessonId);

        var bossProgress = await db.IntermediateBossProgress
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
