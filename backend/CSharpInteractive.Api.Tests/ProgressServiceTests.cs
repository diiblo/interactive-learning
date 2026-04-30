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

        var service = new ProgressService(db, new UnlockService(db));
        var tests = new[] { new TestResultDto("Test", true, "OK") };

        await service.CompleteLessonAsync(profile, lesson, "code", "output", tests, passed: true);
        await service.CompleteLessonAsync(profile, lesson, "code", "output", tests, passed: true);

        Assert.Equal(25, profile.TotalXp);
        Assert.Equal(1, await db.LessonProgress.CountAsync());
    }

    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
}
