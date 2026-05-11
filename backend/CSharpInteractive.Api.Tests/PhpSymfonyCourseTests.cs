using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Models;
using CSharpInteractive.Api.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CSharpInteractive.Api.Tests;

public sealed class PhpSymfonyCourseTests
{
    [Fact]
    public async Task SeedData_CreatesRefactoredPhpSymfonyCourse()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var course = await db.Courses
            .Include(item => item.Chapters)
            .ThenInclude(item => item.Lessons)
            .FirstAsync(item => item.Slug == "php-symfony");

        Assert.Equal("PHP / Symfony", course.Title);
        Assert.Contains("Product Catalog", course.Description);
        Assert.Equal("php-symfony", course.Language);
        Assert.Contains(course.Chapters, chapter => chapter.Title == "Module 1 - PHP 1 - Fondations du langage");
        Assert.Contains(course.Chapters, chapter => chapter.Title == "Module 2 - PHP 2 - Donnees, tableaux et transformations");
        Assert.Contains(course.Chapters, chapter => chapter.Title == "Module 5 - PHP 5 - POO PHP professionnelle");
        Assert.Contains(course.Chapters, chapter => chapter.Title == "Module 8 - PHP 8 - Donnees, fichiers, JSON et PDO");
        Assert.Contains(course.Chapters, chapter => chapter.Title == "Module 15 - Mini-projet Symfony vertical");

        var expectedSlugs = new[]
        {
            "php-syntax-script",
            "php-condition-discount",
            "php-filter-products-in-stock",
            "php-function-format-product",
            "php-enum-order-status",
            "php-oop-interface",
            "php-oop-service-composition",
            "php-composer-json-minimal",
            "php-json-response-native",
            "php-pdo-prepared-select",
            "symfony-controller",
            "symfony-route-index",
            "symfony-form-type",
            "symfony-doctrine-entity",
            "symfony-service-class",
            "symfony-project-product-create",
            "php-boss-final-native-product-catalog",
            "php-symfony-boss-final-products"
        };

        var lessonSlugs = course.Chapters.SelectMany(chapter => chapter.Lessons).Select(lesson => lesson.Slug).ToHashSet();
        Assert.All(expectedSlugs, slug => Assert.Contains(slug, lessonSlugs));
    }

    [Fact]
    public async Task PhpValidation_PassesEnumLesson()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "php-enum-order-status");
        var service = new PhpSymfonyValidationService();

        var result = await service.SubmitAsync(lesson, """
        <?php

        enum OrderStatus: string
        {
            case Pending = 'pending';
            case Paid = 'paid';
            case Cancelled = 'cancelled';
        }
        """);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task PhpValidation_FailsInterfaceLessonWhenInterfaceIsMissing()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "php-oop-interface");
        var service = new PhpSymfonyValidationService();

        var result = await service.SubmitAsync(lesson, """
        <?php

        final class ProductFormatter
        {
            public function format(Product $product): string { return ''; }
        }
        """);

        Assert.False(result.Passed);
        Assert.Contains(result.Tests, test => !test.Passed && test.Name.Contains("interface"));
    }

    [Fact]
    public async Task PhpValidation_DetectsTryCatchFinally()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "php-oop-try-catch");
        var service = new PhpSymfonyValidationService();

        var result = await service.SubmitAsync(lesson, """
        <?php

        try {
            throw new Exception('Error');
        } catch (Exception $exception) {
            echo $exception->getMessage();
        } finally {
            echo 'Done';
        }
        """);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task SeedData_LinksEveryPhpSymfonyLessonToSkillsAndThreeHints()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var lessons = await db.Lessons
            .Include(lesson => lesson.Chapter)
            .ThenInclude(chapter => chapter!.Course)
            .Include(lesson => lesson.LessonSkills)
            .Include(lesson => lesson.Hints)
            .Where(lesson => lesson.Chapter!.Course!.Language == "php-symfony")
            .ToListAsync();

        Assert.NotEmpty(lessons);
        Assert.All(lessons, lesson =>
        {
            Assert.NotEmpty(lesson.LessonSkills);
            Assert.Equal(3, lesson.Hints.Count);
        });
    }

    [Fact]
    public async Task PhpSymfonyValidation_PassesCorrectSymfonyController()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "symfony-controller");
        var service = new PhpSymfonyValidationService();

        var result = await service.SubmitAsync(lesson, """
        <?php

        namespace App\Controller;

        use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
        use Symfony\Component\HttpFoundation\Response;
        use Symfony\Component\Routing\Attribute\Route;

        final class ProductController extends AbstractController
        {
        }
        """);

        Assert.True(result.Passed);
        Assert.All(result.Tests, test => Assert.True(test.Passed));
    }

    [Fact]
    public async Task PhpSymfonyValidation_FailsSymfonyControllerWithoutAbstractController()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "symfony-controller");
        var service = new PhpSymfonyValidationService();

        var result = await service.SubmitAsync(lesson, """
        <?php

        namespace App\Controller;

        use Symfony\Component\HttpFoundation\Response;
        use Symfony\Component\Routing\Attribute\Route;

        final class ProductController
        {
        }
        """);

        Assert.False(result.Passed);
        Assert.Contains(result.Tests, test => !test.Passed && test.Name.Contains("AbstractController"));
    }

    [Fact]
    public async Task PhpSymfonyValidation_PassesProjectProductCreate()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "symfony-project-product-create");
        var service = new PhpSymfonyValidationService();

        var result = await service.SubmitAsync(lesson, lesson.FinalCorrection);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task PhpSymfonyFeedback_MarksCreateWithoutHandleRequestAsPartial()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var profile = await db.UserProfiles.FirstAsync();
        var lesson = await db.Lessons
            .Include(item => item.Tests)
            .Include(item => item.LessonSkills)
            .FirstAsync(item => item.Slug == "symfony-project-product-create");
        var validation = new PhpSymfonyValidationService();
        var submission = await validation.SubmitAsync(lesson, lesson.FinalCorrection.Replace("    $form->handleRequest($request);\n", ""));
        var service = new SkillProgressService(db);

        var feedback = await service.UpdateAfterLessonSubmissionAsync(profile, lesson, submission.Tests, submission.Passed, submission.Execution, "partial code");

        Assert.False(feedback.IsSuccess);
        Assert.Equal(SubmissionErrorCategory.PartialSolution, feedback.ErrorCategory);
        Assert.NotEmpty(feedback.WhatIsMissing);
        Assert.NotEmpty(feedback.ProgressiveHints);
        Assert.NotEmpty(feedback.RelatedSkills);
    }

    [Fact]
    public async Task PhpSymfonyFeedback_MapsEmptyCodeAndMissingSnippet()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var profile = await db.UserProfiles.FirstAsync();
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "php-syntax-script");
        var service = new SkillProgressService(db);

        var empty = await service.UpdateAfterLessonSubmissionAsync(
            profile,
            lesson,
            [new TestResultDto("Contient <?php", false, "Le code doit contenir: <?php")],
            false,
            new ExecutionResultDto(false, "", ["Le code PHP/Symfony ne peut pas etre vide."], 0),
            "");

        var missing = await service.UpdateAfterLessonSubmissionAsync(
            profile,
            lesson,
            [new TestResultDto("Contient <?php", false, "Le code doit contenir: <?php")],
            false,
            new ExecutionResultDto(true, "", [], 0),
            "echo \"Product Catalog\";");

        Assert.Equal(SubmissionErrorCategory.EmptyCode, empty.ErrorCategory);
        Assert.Equal(SubmissionErrorCategory.MissingRequiredSnippet, missing.ErrorCategory);
    }

    [Fact]
    public async Task PhpSymfonyBossFinal_ReturnsSkillScoresAndSuggestedReviews()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var profile = await db.UserProfiles.FirstAsync();
        var bossLesson = await db.Lessons
            .Include(item => item.Chapter)
            .ThenInclude(item => item!.Course)
            .Include(item => item.Tests)
            .FirstAsync(item => item.Slug == "php-boss-final-native-product-catalog");
        var validation = new PhpSymfonyValidationService();
        var partial = await validation.SubmitAsync(bossLesson, "class Product {}");
        var service = new SkillProgressService(db);

        var result = await service.BuildLessonBossResultAsync(profile, bossLesson, partial.Tests, partial.Passed);

        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.SkillResults);
        Assert.NotEmpty(result.SuggestedReviews);
        Assert.Contains(partial.Tests, test => !test.Passed && test.Name.Contains("findAvailable"));
    }

    [Fact]
    public async Task SeedData_CreatesExpectedPhpSymfonyIntermediateBosses()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var slugs = await db.IntermediateBosses.Select(item => item.Slug).ToListAsync();

        Assert.Contains("php-module-1-intermediate-boss", slugs);
        Assert.Contains("php-module-2-intermediate-boss", slugs);
        Assert.Contains("php-module-3-intermediate-boss", slugs);
        Assert.Contains("php-module-5-intermediate-boss", slugs);
        Assert.Contains("php-module-7-intermediate-boss", slugs);
        Assert.Contains("php-module-8-intermediate-boss", slugs);
    }

    [Theory]
    [InlineData("php-condition-discount")]
    [InlineData("php-filter-products-in-stock")]
    [InlineData("php-function-format-product")]
    [InlineData("php-enum-order-status")]
    [InlineData("php-oop-interface")]
    [InlineData("php-oop-service-composition")]
    [InlineData("php-json-response-native")]
    [InlineData("php-pdo-prepared-select")]
    public async Task PhpValidation_PassesNativePhpCorrections(string slug)
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == slug);
        var service = new PhpSymfonyValidationService();

        var result = await service.SubmitAsync(lesson, lesson.FinalCorrection);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task PhpDiscountValidation_RejectsHardCodedFinalTotal()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "php-condition-discount");
        var service = new PhpSymfonyValidationService();

        var result = await service.SubmitAsync(lesson, """
        <?php

        $total = 120;
        $discount = 0;
        if ($total >= 100) {
            $discount = 12;
        }
        echo "Total final : 108";
        """);

        Assert.False(result.Passed);
        Assert.Contains(result.Tests, test => !test.Passed && test.Name.Contains("Evite"));
    }

    [Fact]
    public async Task PhpBossFinalNative_ReturnsBroadPhpSkillScores()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var profile = await db.UserProfiles.FirstAsync();
        var bossLesson = await db.Lessons
            .Include(item => item.Chapter)
            .ThenInclude(item => item!.Course)
            .Include(item => item.Tests)
            .FirstAsync(item => item.Slug == "php-boss-final-native-product-catalog");
        var validation = new PhpSymfonyValidationService();
        var submission = await validation.SubmitAsync(bossLesson, bossLesson.FinalCorrection);
        var service = new SkillProgressService(db);

        var result = await service.BuildLessonBossResultAsync(profile, bossLesson, submission.Tests, submission.Passed);

        Assert.True(result.IsSuccess);
        Assert.Contains(result.SkillResults, item => item.SkillSlug == "php-oop");
        Assert.Contains(result.SkillResults, item => item.SkillSlug == "php-http");
        Assert.Contains(result.SkillResults, item => item.SkillSlug == "php-pdo" || item.SkillSlug == "php-files");
        Assert.Contains(result.SkillResults, item => item.SkillSlug == "php-project-structure");
    }

    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
}
