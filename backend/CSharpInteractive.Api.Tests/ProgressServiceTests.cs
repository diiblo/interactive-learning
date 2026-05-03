using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Models;
using CSharpInteractive.Api.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CSharpInteractive.Api.Tests;

public sealed class ProgressServiceTests
{
    [Fact]
    public async Task CompleteLessonAsync_AwardsXpOnlyOnce()
    {
        await using var db = CreateDbContext();
        var profile = new UserProfile();
        var lesson = new Lesson { Id = 1, Title = "Test", XpReward = 25 };
        db.UserProfiles.Add(profile);
        db.Lessons.Add(lesson);
        await db.SaveChangesAsync();

        var service = new ProgressService(db, new UnlockService(db), new SkillProgressService(db));
        var tests = new[] { new TestResultDto("Test", true, "OK") };

        await service.CompleteLessonAsync(profile, lesson, "code", "output", tests, passed: true);
        await service.CompleteLessonAsync(profile, lesson, "code", "output", tests, passed: true);

        Assert.Equal(25, profile.TotalXp);
        Assert.Equal(1, await db.LessonProgress.CountAsync());
    }

    [Fact]
    public async Task CompleteLessonAsync_DoesNotAwardSqlBadgeFromCSharpLesson()
    {
        await using var db = CreateDbContext();
        var csharp = new Course { Id = 1, Language = "csharp", Title = "C#", Slug = "csharp" };
        var sql = new Course { Id = 2, Language = "sqlserver", Title = "SQL", Slug = "sql" };
        var chapter = new Chapter { Id = 1, Course = csharp };
        var lesson = new Lesson { Id = 1, Title = "C#", Chapter = chapter, XpReward = 10 };
        var profile = new UserProfile();
        db.Courses.AddRange(csharp, sql);
        db.Chapters.Add(chapter);
        db.Lessons.Add(lesson);
        db.UserProfiles.Add(profile);
        db.Badges.Add(new Badge
        {
            Slug = "sql-first-select",
            Name = "Premier SELECT",
            RuleType = BadgeRuleType.CompleteLessonInCourse,
            RuleValue = 1,
            RuleCourseLanguage = "sqlserver"
        });
        await db.SaveChangesAsync();

        var service = new ProgressService(db, new UnlockService(db), new SkillProgressService(db));
        await service.CompleteLessonAsync(profile, lesson, "code", "output", [new TestResultDto("Test", true, "OK")], passed: true);

        Assert.Empty(db.UserBadges);
    }

    [Fact]
    public async Task CompleteLessonAsync_AwardsSqlBadgeFromSqlLesson()
    {
        await using var db = CreateDbContext();
        var sql = new Course { Id = 2, Language = "sqlserver", Title = "SQL", Slug = "sql" };
        var chapter = new Chapter { Id = 1, Course = sql };
        var lesson = new Lesson { Id = 1, Title = "SQL", Chapter = chapter, XpReward = 10 };
        var profile = new UserProfile();
        db.Courses.Add(sql);
        db.Chapters.Add(chapter);
        db.Lessons.Add(lesson);
        db.UserProfiles.Add(profile);
        db.Badges.Add(new Badge
        {
            Slug = "sql-first-select",
            Name = "Premier SELECT",
            RuleType = BadgeRuleType.CompleteLessonInCourse,
            RuleValue = 1,
            RuleCourseLanguage = "sqlserver"
        });
        await db.SaveChangesAsync();

        var service = new ProgressService(db, new UnlockService(db), new SkillProgressService(db));
        await service.CompleteLessonAsync(profile, lesson, "code", "output", [new TestResultDto("Test", true, "OK")], passed: true);

        Assert.Single(db.UserBadges);
    }

    [Fact]
    public async Task CompleteLessonAsync_AwardsPhpBadgeFromPhpLesson()
    {
        await using var db = CreateDbContext();
        var php = new Course { Id = 3, Language = "php-symfony", Title = "PHP", Slug = "php" };
        var chapter = new Chapter { Id = 1, Course = php };
        var lesson = new Lesson { Id = 1, Title = "PHP", Chapter = chapter, XpReward = 10 };
        var profile = new UserProfile();
        db.Courses.Add(php);
        db.Chapters.Add(chapter);
        db.Lessons.Add(lesson);
        db.UserProfiles.Add(profile);
        db.Badges.Add(new Badge
        {
            Slug = "php-first-script",
            Name = "Premier script PHP",
            RuleType = BadgeRuleType.CompleteLessonInCourse,
            RuleValue = 1,
            RuleCourseLanguage = "php-symfony"
        });
        await db.SaveChangesAsync();

        var service = new ProgressService(db, new UnlockService(db), new SkillProgressService(db));
        await service.CompleteLessonAsync(profile, lesson, "code", "output", [new TestResultDto("Test", true, "OK")], passed: true);

        Assert.Single(db.UserBadges);
    }

    [Fact]
    public async Task CompleteLessonAsync_AwardsBossFinalBadgeForCourse()
    {
        await using var db = CreateDbContext();
        var sql = new Course { Id = 2, Language = "sqlserver", Title = "SQL", Slug = "sql" };
        var chapter = new Chapter { Id = 1, Course = sql };
        var lesson = new Lesson { Id = 1, Title = "SQL Boss", Chapter = chapter, XpReward = 10, IsBossFinal = true };
        var profile = new UserProfile();
        db.Courses.Add(sql);
        db.Chapters.Add(chapter);
        db.Lessons.Add(lesson);
        db.UserProfiles.Add(profile);
        db.Badges.Add(new Badge
        {
            Slug = "sql-boss-final",
            Name = "Boss Final SQL",
            RuleType = BadgeRuleType.CompleteBossFinalInCourse,
            RuleValue = 1,
            RuleCourseLanguage = "sqlserver"
        });
        await db.SaveChangesAsync();

        var service = new ProgressService(db, new UnlockService(db), new SkillProgressService(db));
        await service.CompleteLessonAsync(profile, lesson, "code", "output", [new TestResultDto("Test", true, "OK")], passed: true);

        Assert.Single(db.UserBadges);
    }

    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
}
