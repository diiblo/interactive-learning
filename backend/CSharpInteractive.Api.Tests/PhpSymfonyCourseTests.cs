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
        Assert.Contains(course.Chapters, chapter => chapter.Title == "Module 1 - PHP 1 - B-A-BA : premiers scripts PHP");
        Assert.Contains(course.Chapters, chapter => chapter.Title == "Module 2 - PHP 2 - Tableaux, listes et donnees metier");
        Assert.Contains(course.Chapters, chapter => chapter.Title == "Module 5 - PHP 5 - POO PHP professionnelle");
        Assert.Contains(course.Chapters, chapter => chapter.Title == "Module 8 - PHP 8 - Persistance : JSON, fichiers et PDO");
        Assert.Contains(course.Chapters, chapter => chapter.Title == "Module 10 - Symfony 1 - Comprendre Symfony");
        Assert.Contains(course.Chapters, chapter => chapter.Title == "Module 18 - Symfony 9 - Projet Product Catalog Symfony");

        var expectedSlugs = new[]
        {
            "php-what-is-php",
            "php-syntax-script",
            "php-echo-output",
            "php-condition-discount",
            "php-strings-output",
            "php-array-access-index",
            "php-filter-products-in-stock",
            "php-function-why",
            "php-function-format-product",
            "php-scalar-type-hints",
            "php-enum-order-status",
            "php-oop-why-objects",
            "php-oop-interface",
            "php-oop-service-composition",
            "php-project-structure-src-public",
            "php-composer-json-minimal",
            "php-http-request-response",
            "php-json-response-native",
            "php-pdo-prepared-select",
            "php-pdo-repository",
            "symfony-why-framework",
            "symfony-controller-basic",
            "symfony-route-index",
            "symfony-request-query",
            "symfony-twig-layout",
            "symfony-form-type",
            "symfony-doctrine-entity",
            "symfony-doctrine-find",
            "symfony-service-class",
            "symfony-security-why",
            "symfony-api-list-products",
            "symfony-project-product-create",
            "symfony-project-clean-architecture",
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
    public async Task SeedData_PhpSymfonyLessonsReadLikeInteractiveCourse()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var lessons = await db.Lessons
            .Include(lesson => lesson.Chapter)
            .ThenInclude(chapter => chapter!.Course)
            .Include(lesson => lesson.Hints)
            .Include(lesson => lesson.LessonSkills)
            .Where(lesson => lesson.Chapter!.Course!.Language == "php-symfony")
            .ToListAsync();

        Assert.NotEmpty(lessons);
        Assert.All(lessons, lesson =>
        {
            Assert.Contains("Situation", lesson.ConceptSummary);
            Assert.Contains("Cours", lesson.Explanation);
            Assert.True(lesson.Explanation.Length > 450);
            Assert.Contains("Question rapide", lesson.ExercisePrompt);
            Assert.Contains("Manipulation guidee", lesson.ExercisePrompt);
            Assert.Contains("Exercice principal", lesson.ExercisePrompt);
            Assert.Contains("Validation automatique", lesson.ExercisePrompt);
            Assert.Contains("Recapitulatif", lesson.ExercisePrompt);
            Assert.Contains("Lien avec le mini-projet", lesson.ExercisePrompt);
            Assert.Contains("Product Catalog", lesson.ExercisePrompt);
            Assert.NotEmpty(lesson.FinalCorrection);
            Assert.Equal(3, lesson.Hints.Count);
            Assert.NotEmpty(lesson.LessonSkills);
        });
    }

    [Fact]
    public async Task SeedData_HasNoDuplicatePhpSymfonyLessonSlugs()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var duplicates = await db.Lessons
            .Include(lesson => lesson.Chapter)
            .ThenInclude(chapter => chapter!.Course)
            .Where(lesson => lesson.Chapter!.Course!.Language == "php-symfony")
            .GroupBy(lesson => lesson.Slug)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToListAsync();

        Assert.Empty(duplicates);
    }

    [Fact]
    public async Task SeedData_PhpIntermediateBossesAreGuidedEvaluations()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var bosses = await db.IntermediateBosses
            .Include(boss => boss.Module)
            .ThenInclude(module => module!.Course)
            .Where(boss => boss.Module!.Course!.Language == "php-symfony")
            .ToListAsync();

        var expected = new[]
        {
            "php-module-1-intermediate-boss",
            "php-module-2-intermediate-boss",
            "php-module-3-intermediate-boss",
            "php-module-5-intermediate-boss",
            "php-module-7-intermediate-boss",
            "php-module-8-intermediate-boss",
            "symfony-module-2-intermediate-boss",
            "symfony-module-3-intermediate-boss",
            "symfony-module-4-intermediate-boss",
            "symfony-module-5-intermediate-boss",
            "symfony-module-6-intermediate-boss",
            "symfony-module-8-intermediate-boss"
        };

        Assert.All(expected, slug => Assert.Contains(bosses, boss => boss.Slug == slug));
        Assert.All(bosses.Where(boss => expected.Contains(boss.Slug)), boss =>
        {
            Assert.Contains("Mise en situation", boss.Instructions);
            Assert.Contains("Competences testees", boss.Instructions);
            Assert.Contains("Criteres visibles", boss.Instructions);
            Assert.Contains("Rapport final", boss.Instructions);
            Assert.NotEmpty(boss.ValidationRules);
            Assert.Equal(3, boss.Hints.Count);
        });
    }

    [Fact]
    public async Task PhpSymfonyValidation_PassesCorrectSymfonyController()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "symfony-controller-basic");
        var service = new PhpSymfonyValidationService();

        var result = await service.SubmitAsync(lesson, """
        <?php

        namespace App\Controller;

        use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
        use Symfony\Component\HttpFoundation\Response;
        use Symfony\Component\Routing\Attribute\Route;

        final class ProductController extends AbstractController
        {
            public function index(): Response
            {
                return new Response('Products');
            }
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
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "symfony-controller-basic");
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
        Assert.Contains("symfony-module-2-intermediate-boss", slugs);
        Assert.Contains("symfony-module-3-intermediate-boss", slugs);
        Assert.Contains("symfony-module-4-intermediate-boss", slugs);
        Assert.Contains("symfony-module-5-intermediate-boss", slugs);
        Assert.Contains("symfony-module-6-intermediate-boss", slugs);
        Assert.Contains("symfony-module-8-intermediate-boss", slugs);
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

    [Theory]
    [InlineData("symfony-controller-basic")]
    [InlineData("symfony-route-parameter")]
    [InlineData("symfony-doctrine-entity")]
    [InlineData("symfony-form-type")]
    [InlineData("symfony-service-class")]
    [InlineData("symfony-api-json-response")]
    public async Task PhpSymfonyValidation_PassesSymfonyCorrections(string slug)
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
