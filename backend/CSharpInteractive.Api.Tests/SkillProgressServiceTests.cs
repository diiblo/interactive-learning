using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Models;
using CSharpInteractive.Api.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CSharpInteractive.Api.Tests;

public sealed class SkillProgressServiceTests
{
    [Fact]
    public async Task SeedData_CreatesSkillsAndLessonLinks()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        Assert.True(await db.Skills.CountAsync() >= 30);
        Assert.True(await db.LessonSkills.AnyAsync());
        Assert.True(await db.LessonHints.AnyAsync());
    }

    [Fact]
    public async Task SeedData_UsesExplicitLessonSkillWeights()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var hello = await db.Lessons
            .Include(lesson => lesson.LessonSkills)
            .ThenInclude(item => item.Skill)
            .FirstAsync(lesson => lesson.Slug == "hello-world");

        var consoleSkill = hello.LessonSkills.First(item => item.Skill!.Slug == "csharp-console-output");
        Assert.Equal(2, consoleSkill.Weight);
    }

    [Fact]
    public async Task SeedData_KeepsExamplesAndStartersSeparateFromCorrections()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var lessons = await db.Lessons
            .Where(lesson => lesson.FinalCorrection != "")
            .ToListAsync();

        Assert.All(lessons, lesson =>
        {
            Assert.False(IsTooCloseToCorrection(lesson.ExampleCode, lesson.FinalCorrection), $"{lesson.Slug} example is too close to the correction.");
            Assert.False(IsTooCloseToCorrection(lesson.StarterCode, lesson.FinalCorrection), $"{lesson.Slug} starter code exposes the correction.");
        });
    }

    [Fact]
    public async Task SeedData_DoesNotExposeIntermediateCheckpointLessons()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var checkpoints = await db.Lessons
            .Where(lesson => lesson.Slug.EndsWith("-checkpoint") || lesson.Title.StartsWith("Test intermediaire"))
            .Select(lesson => lesson.Slug)
            .ToListAsync();

        Assert.Empty(checkpoints);
    }

    [Fact]
    public void InferSkillSlugs_FallsBackWhenPlanMissing()
    {
        var course = new Course { Language = "sqlserver" };
        var chapter = new Chapter { Course = course };
        var lesson = new Lesson { Slug = "sql-join-demo", Chapter = chapter };

        var method = typeof(SeedData).GetMethod("InferSkillSlugs", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        var result = (IReadOnlyList<string>)method!.Invoke(null, [lesson])!;

        Assert.Contains("sql-joins", result);
    }

    [Fact]
    public async Task UpdateAfterLessonSubmission_IncreasesMasteryAfterSuccess()
    {
        await using var db = CreateDbContext();
        var (service, profile, lesson) = await CreateSeededServiceAsync(db);

        await service.UpdateAfterLessonSubmissionAsync(profile, lesson, [new TestResultDto("Output", true, "OK")], true, new ExecutionResultDto(true, "OK", [], 1), "Console.WriteLine(\"OK\");");
        var progress = await db.UserSkillProgress.Include(item => item.Skill).FirstAsync(item => item.Skill!.Slug == "csharp-console-output");

        Assert.Equal(1, progress.SuccessfulAttempts);
        Assert.True(progress.MasteryPercent > 0);
        Assert.NotNull(progress.NextReviewAt);
    }

    [Fact]
    public async Task UpdateAfterLessonSubmission_MarksSkillFragileAfterRepeatedFailures()
    {
        await using var db = CreateDbContext();
        var (service, profile, lesson) = await CreateSeededServiceAsync(db);
        var failed = new[] { new TestResultDto("Output", false, "La sortie attendue doit contenir: Hello") };

        await service.UpdateAfterLessonSubmissionAsync(profile, lesson, failed, false, new ExecutionResultDto(true, "", [], 1), "Console.WriteLine(\"Hi\");");
        await service.UpdateAfterLessonSubmissionAsync(profile, lesson, failed, false, new ExecutionResultDto(true, "", [], 1), "Console.WriteLine(\"Hi\");");

        var progress = await db.UserSkillProgress.Include(item => item.Skill).FirstAsync(item => item.Skill!.Slug == "csharp-console-output");
        Assert.Equal(SkillProgressStatus.Fragile, progress.Status);
        Assert.Equal(2, progress.FailedAttempts);
    }

    [Fact]
    public async Task GetDueReviews_ReturnsDueSkill()
    {
        await using var db = CreateDbContext();
        var (service, profile, _) = await CreateSeededServiceAsync(db);
        var skill = await db.Skills.FirstAsync(item => item.Slug == "csharp-console-output");
        db.UserSkillProgress.Add(new UserSkillProgress
        {
            UserProfileId = profile.Id,
            SkillId = skill.Id,
            MasteryPercent = 50,
            NextReviewAt = DateTime.UtcNow.AddMinutes(-5),
            Status = SkillProgressStatus.Learning
        });
        await db.SaveChangesAsync();

        var due = await service.GetDueReviewsAsync(profile);

        var item = Assert.Single(due, item => item.SkillSlug == "csharp-console-output");
        Assert.Equal(SkillProgressStatus.ReviewDue, item.Status);
        Assert.NotEmpty(item.SuggestedLessonSlugs);
    }

    [Fact]
    public async Task UpdateAfterLessonSubmission_ReturnsStructuredFeedback()
    {
        await using var db = CreateDbContext();
        var (service, profile, lesson) = await CreateSeededServiceAsync(db);

        var feedback = await service.UpdateAfterLessonSubmissionAsync(
            profile,
            lesson,
            [new TestResultDto("Snippet", false, "Le code doit contenir: Console.WriteLine")],
            false,
            new ExecutionResultDto(true, "", [], 1),
            "using System;\n");

        Assert.False(feedback.IsSuccess);
        Assert.NotEmpty(feedback.ProgressiveHints);
        Assert.NotEmpty(feedback.RelatedSkills);
        Assert.Equal(SubmissionErrorCategory.MissingRequiredSnippet, feedback.ErrorCategory);
    }

    [Fact]
    public async Task UpdateAfterLessonSubmission_ReturnsEmptyCodeCategory()
    {
        await using var db = CreateDbContext();
        var (service, profile, lesson) = await CreateSeededServiceAsync(db);

        var feedback = await service.UpdateAfterLessonSubmissionAsync(
            profile,
            lesson,
            [new TestResultDto("Output", false, "La sortie attendue doit contenir: Hello")],
            false,
            new ExecutionResultDto(true, "", [], 1),
            "");

        Assert.Equal(SubmissionErrorCategory.EmptyCode, feedback.ErrorCategory);
        Assert.NotEmpty(feedback.WhatIsMissing);
    }

    [Fact]
    public async Task UpdateAfterLessonSubmission_ReturnsCompilationErrorCategory()
    {
        await using var db = CreateDbContext();
        var (service, profile, lesson) = await CreateSeededServiceAsync(db);

        var feedback = await service.UpdateAfterLessonSubmissionAsync(
            profile,
            lesson,
            [new TestResultDto("Output", false, "La sortie attendue doit contenir: Hello")],
            false,
            new ExecutionResultDto(false, "", ["error CS1002"], 1),
            "using System; Console.WriteLine(\"Hi\");");

        Assert.Equal(SubmissionErrorCategory.CompilationError, feedback.ErrorCategory);
    }

    [Fact]
    public async Task UpdateAfterLessonSubmission_ReturnsWrongOutputCategory()
    {
        await using var db = CreateDbContext();
        var (service, profile, lesson) = await CreateSeededServiceAsync(db);

        var feedback = await service.UpdateAfterLessonSubmissionAsync(
            profile,
            lesson,
            [new TestResultDto("Output", false, "La sortie attendue doit contenir: Hello")],
            false,
            new ExecutionResultDto(true, "", [], 1),
            "using System; Console.WriteLine(\"Hi\"); Console.WriteLine(\"There\");");

        Assert.Equal(SubmissionErrorCategory.WrongOutput, feedback.ErrorCategory);
    }

    [Fact]
    public async Task UpdateAfterLessonSubmission_ReturnsForbiddenSnippetCategory()
    {
        await using var db = CreateDbContext();
        var (service, profile, lesson) = await CreateSeededServiceAsync(db);

        var feedback = await service.UpdateAfterLessonSubmissionAsync(
            profile,
            lesson,
            [new TestResultDto("Snippet", false, "Le code ne doit pas contenir: Console.ReadLine")],
            false,
            new ExecutionResultDto(true, "", [], 1),
            "Console.ReadLine();");

        Assert.Equal(SubmissionErrorCategory.ForbiddenSnippetUsed, feedback.ErrorCategory);
    }

    [Fact]
    public async Task MarkReviewCompleted_IncreasesMasteryAndSchedulesNextReview()
    {
        await using var db = CreateDbContext();
        var (service, profile, _) = await CreateSeededServiceAsync(db);
        var skill = await db.Skills.FirstAsync(item => item.Slug == "csharp-console-output");
        db.UserSkillProgress.Add(new UserSkillProgress
        {
            UserProfileId = profile.Id,
            SkillId = skill.Id,
            MasteryPercent = 40,
            NextReviewAt = DateTime.UtcNow.AddMinutes(-5),
            Status = SkillProgressStatus.Learning
        });
        await db.SaveChangesAsync();

        await service.MarkReviewCompletedAsync(profile, skill.Id);

        var updated = await db.UserSkillProgress.FirstAsync(item => item.UserProfileId == profile.Id && item.SkillId == skill.Id);
        Assert.True(updated.MasteryPercent > 40);
        Assert.True(updated.NextReviewAt > DateTime.UtcNow);
    }

    [Fact]
    public async Task BuildBossResult_ReturnsSkillScores()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var profile = await db.UserProfiles.FirstAsync();
        var boss = await db.IntermediateBosses
            .Include(item => item.Module)
            .ThenInclude(module => module!.Course)
            .FirstAsync(item => item.Slug == "csharp-module-1-intermediate-boss");
        var service = new SkillProgressService(db);

        var result = await service.BuildBossResultAsync(profile, boss, [new TestResultDto("Output", true, "OK"), new TestResultDto("Snippet", false, "Missing")], false);

        Assert.False(result.IsSuccess);
        Assert.InRange(result.ScorePercent, 0, 100);
        Assert.NotEmpty(result.SkillResults);
    }

    private static async Task<(SkillProgressService Service, UserProfile Profile, Lesson Lesson)> CreateSeededServiceAsync(AppDbContext db)
    {
        await SeedData.EnsureSeededAsync(db);
        var profile = await db.UserProfiles.FirstAsync();
        var lesson = await db.Lessons.FirstAsync(item => item.Slug == "hello-world");
        return (new SkillProgressService(db), profile, lesson);
    }

    private static bool IsTooCloseToCorrection(string candidate, string correction)
    {
        if (string.IsNullOrWhiteSpace(candidate) || string.IsNullOrWhiteSpace(correction))
        {
            return false;
        }

        var normalizedCandidate = NormalizeCode(candidate);
        var normalizedCorrection = NormalizeCode(correction);

        if (normalizedCandidate == normalizedCorrection)
        {
            return true;
        }

        return normalizedCorrection.Contains(normalizedCandidate, StringComparison.Ordinal)
            && normalizedCandidate.Length >= Math.Min(80, normalizedCorrection.Length);
    }

    private static string NormalizeCode(string code)
    {
        var chars = code.Where(character => !char.IsWhiteSpace(character)).ToArray();
        return new string(chars).ToUpperInvariant();
    }

    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
}
