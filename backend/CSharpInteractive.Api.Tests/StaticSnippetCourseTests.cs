using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Models;
using CSharpInteractive.Api.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CSharpInteractive.Api.Tests;

public sealed class StaticSnippetCourseTests
{
    [Fact]
    public async Task SeedData_Audit_HasUniqueCourseAndLessonSlugs()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var courseSlugs = await db.Courses.Select(course => course.Slug).ToListAsync();
        var lessonSlugs = await db.Lessons.Select(lesson => lesson.Slug).ToListAsync();
        var skillSlugs = await db.Skills.Select(skill => skill.Slug).ToListAsync();
        var bossSlugs = await db.IntermediateBosses.Select(boss => boss.Slug).ToListAsync();

        Assert.All(courseSlugs, slug => Assert.False(string.IsNullOrWhiteSpace(slug)));
        Assert.All(lessonSlugs, slug => Assert.False(string.IsNullOrWhiteSpace(slug)));
        Assert.Equal(courseSlugs.Count, courseSlugs.Distinct(StringComparer.OrdinalIgnoreCase).Count());
        Assert.Equal(lessonSlugs.Count, lessonSlugs.Distinct(StringComparer.OrdinalIgnoreCase).Count());
        Assert.Equal(skillSlugs.Count, skillSlugs.Distinct(StringComparer.OrdinalIgnoreCase).Count());
        Assert.Equal(bossSlugs.Count, bossSlugs.Distinct(StringComparer.OrdinalIgnoreCase).Count());
    }

    [Fact]
    public async Task SeedData_Audit_AllLessonsHavePedagogicalContent()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var lessons = await db.Lessons
            .Include(lesson => lesson.Tests)
            .Include(lesson => lesson.Hints)
            .Include(lesson => lesson.LessonSkills)
            .ToListAsync();

        Assert.NotEmpty(lessons);
        Assert.All(lessons, lesson =>
        {
            Assert.False(string.IsNullOrWhiteSpace(lesson.Slug));
            Assert.False(string.IsNullOrWhiteSpace(lesson.Title), lesson.Slug);
            Assert.False(string.IsNullOrWhiteSpace(lesson.Objective), lesson.Slug);
            Assert.False(string.IsNullOrWhiteSpace(lesson.ConceptSummary), lesson.Slug);
            Assert.False(string.IsNullOrWhiteSpace(lesson.Explanation), lesson.Slug);
            Assert.False(string.IsNullOrWhiteSpace(lesson.ExercisePrompt), lesson.Slug);
            Assert.False(string.IsNullOrWhiteSpace(lesson.StarterCode), lesson.Slug);
            Assert.NotEmpty(lesson.Tests);
            Assert.Equal(3, lesson.Hints.Count);
            Assert.False(string.IsNullOrWhiteSpace(lesson.SuccessFeedback), lesson.Slug);
            Assert.False(string.IsNullOrWhiteSpace(lesson.FailureFeedback), lesson.Slug);
            Assert.NotEmpty(lesson.LessonSkills);
            Assert.False(string.IsNullOrWhiteSpace(lesson.FinalCorrection), lesson.Slug);
            Assert.InRange(lesson.XpReward, 1, 500);
        });
    }

    [Fact]
    public async Task SeedData_Audit_LessonSkillsReferenceExistingSkillsAndSkillsAreUsed()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var skills = await db.Skills.Include(skill => skill.LessonSkills).ToListAsync();
        var lessonSkills = await db.LessonSkills
            .Include(item => item.Skill)
            .Include(item => item.Lesson)
            .ToListAsync();

        Assert.NotEmpty(skills);
        Assert.NotEmpty(lessonSkills);
        Assert.All(lessonSkills, item =>
        {
            Assert.NotNull(item.Skill);
            Assert.NotNull(item.Lesson);
            Assert.InRange(item.Weight, 1, 3);
        });

        var futureSkillSlugs = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            // Legacy aggregate skills kept for compatibility with existing progress records.
            "js-foundations",
            "js-arrays-objects",
            "js-modern",
            "js-dom",
            "js-storage",
            "react-router",
            "react-performance",
            "tailwind-flex-grid"
        };
        var unusedSkills = skills
            .Where(skill => skill.LessonSkills.Count == 0 && !futureSkillSlugs.Contains(skill.Slug))
            .Select(skill => skill.Slug)
            .ToList();

        Assert.Empty(unusedSkills);
    }

    [Fact]
    public async Task SeedData_Audit_AllCoursesHaveBossesAndValidBossContent()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var courses = await db.Courses
            .Include(course => course.Chapters)
            .ThenInclude(chapter => chapter.Lessons)
            .Include(course => course.Chapters)
            .ThenInclude(chapter => chapter.IntermediateBoss)
            .ThenInclude(boss => boss!.ValidationRules)
            .Include(course => course.Chapters)
            .ThenInclude(chapter => chapter.IntermediateBoss)
            .ThenInclude(boss => boss!.Hints)
            .ToListAsync();

        Assert.NotEmpty(courses);
        Assert.All(courses, course =>
        {
            var lessons = course.Chapters.SelectMany(chapter => chapter.Lessons).ToList();
            Assert.Contains(lessons, lesson => lesson.IsBossFinal);
            Assert.Contains(course.Chapters, chapter => chapter.IntermediateBoss is not null || chapter.Lessons.Any(lesson => lesson.IsBossFinal));

            foreach (var boss in course.Chapters.Select(chapter => chapter.IntermediateBoss).Where(boss => boss is not null).Cast<IntermediateBoss>())
            {
                Assert.False(string.IsNullOrWhiteSpace(boss.Slug));
                Assert.False(string.IsNullOrWhiteSpace(boss.Title), boss.Slug);
                Assert.False(string.IsNullOrWhiteSpace(boss.Objective), boss.Slug);
                Assert.False(string.IsNullOrWhiteSpace(boss.Instructions), boss.Slug);
                Assert.False(string.IsNullOrWhiteSpace(boss.StarterCode), boss.Slug);
                Assert.False(string.IsNullOrWhiteSpace(boss.Solution), boss.Slug);
                Assert.NotEmpty(boss.ValidationRules);
                Assert.Equal(3, boss.Hints.Count);
                Assert.InRange(boss.XpReward, 1, 500);
            }
        });
    }

    [Fact]
    public async Task SeedData_Audit_PreviewPolicyMatchesCourseLanguage()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var lessons = await db.Lessons
            .Include(lesson => lesson.Chapter)
            .ThenInclude(chapter => chapter!.Course)
            .ToListAsync();

        Assert.All(lessons, lesson =>
        {
            var preview = LessonPreviewService.For(lesson);
            var language = lesson.Chapter!.Course!.Language;
            var expectedMode = language switch
            {
                "css" => "html-css",
                "javascript" => "javascript-dom",
                "tailwindcss" => "tailwind",
                "react" => "react",
                _ => "none"
            };

            Assert.Equal(expectedMode, preview.PreviewMode);
            Assert.Equal(expectedMode != "none", preview.SupportsPreview);
            if (language is "css" or "javascript")
            {
                Assert.False(string.IsNullOrWhiteSpace(preview.PreviewHtml), lesson.Slug);
            }
        });
    }

    [Fact]
    public async Task SeedData_CreatesReactCourse()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var course = await db.Courses
            .Include(item => item.Chapters)
            .ThenInclude(item => item.Lessons)
            .FirstAsync(item => item.Slug == "react");

        Assert.Equal("React", course.Title);
        Assert.Equal("typescript", new ReactLearningLanguageHandler(new StaticSnippetValidationService()).EditorLanguage);
        Assert.Equal(11, course.Chapters.Count);
        Assert.Contains(course.Chapters.SelectMany(chapter => chapter.Lessons), lesson => lesson.Slug == "react-boss-final-product-manager");
    }

    [Fact]
    public async Task SeedData_CreatesReactNativeCourse()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var course = await db.Courses
            .Include(item => item.Chapters)
            .ThenInclude(item => item.Lessons)
            .FirstAsync(item => item.Slug == "react-native");

        Assert.Equal("React Native", course.Title);
        Assert.Equal("typescript", new ReactNativeLearningLanguageHandler(new StaticSnippetValidationService()).EditorLanguage);
        Assert.Equal(9, course.Chapters.Count);
        Assert.Contains(course.Chapters.SelectMany(chapter => chapter.Lessons), lesson => lesson.Slug == "react-native-boss-final-product-app");
        Assert.Contains(course.Chapters.SelectMany(chapter => chapter.Lessons), lesson => lesson.Slug == "rn-render-item");
        Assert.Contains(course.Chapters.SelectMany(chapter => chapter.Lessons), lesson => lesson.Slug == "rn-project-polished-ui");
    }

    [Fact]
    public async Task SeedData_CreatesTailwindCssCourse()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var course = await db.Courses
            .Include(item => item.Chapters)
            .ThenInclude(item => item.Lessons)
            .FirstAsync(item => item.Slug == "tailwindcss");

        Assert.Equal("TailwindCSS", course.Title);
        Assert.Equal("html", new TailwindCssLearningLanguageHandler(new StaticSnippetValidationService()).EditorLanguage);
        Assert.Equal(8, course.Chapters.Count);
        Assert.Contains(course.Chapters.SelectMany(chapter => chapter.Lessons), lesson => lesson.Slug == "tailwindcss-boss-final-dashboard");
        Assert.Contains(course.Chapters.SelectMany(chapter => chapter.Lessons), lesson => lesson.Slug == "tailwind-dark-mode-basic");
        Assert.Contains(course.Chapters.SelectMany(chapter => chapter.Lessons), lesson => lesson.Slug == "tailwind-dashboard-form");
    }

    [Fact]
    public async Task SeedData_CreatesCssCourse()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var course = await db.Courses
            .Include(item => item.Chapters)
            .ThenInclude(item => item.Lessons)
            .FirstAsync(item => item.Slug == "css");

        Assert.Equal("CSS", course.Title);
        Assert.Equal("css", new CssLearningLanguageHandler(new StaticSnippetValidationService()).EditorLanguage);
        Assert.Equal(6, course.Chapters.Count);
        Assert.Contains(course.Chapters.SelectMany(chapter => chapter.Lessons), lesson => lesson.Slug == "css-boss-final-responsive-product-page");
        Assert.Contains(course.Chapters.SelectMany(chapter => chapter.Lessons), lesson => lesson.Slug == "css-clamp-font-size");
        Assert.Contains(course.Chapters.SelectMany(chapter => chapter.Lessons), lesson => lesson.Slug == "css-project-form-section");
    }

    [Fact]
    public async Task SeedData_CreatesJavaScriptCourse()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var course = await db.Courses
            .Include(item => item.Chapters)
            .ThenInclude(item => item.Lessons)
            .FirstAsync(item => item.Slug == "javascript");

        Assert.Equal("JavaScript", course.Title);
        Assert.Equal("javascript", new JavaScriptLearningLanguageHandler(new StaticSnippetValidationService()).EditorLanguage);
        Assert.Equal(6, course.Chapters.Count);
        Assert.Contains(course.Chapters.SelectMany(chapter => chapter.Lessons), lesson => lesson.Slug == "javascript-boss-final-product-list");
        Assert.Contains(course.Chapters.SelectMany(chapter => chapter.Lessons), lesson => lesson.Slug == "js-render-product-list");
        Assert.Contains(course.Chapters.SelectMany(chapter => chapter.Lessons), lesson => lesson.Slug == "js-api-render-products");
    }

    [Fact]
    public async Task SeedData_CreatesExpectedTailwindModules()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var titles = await db.Chapters
            .Include(chapter => chapter.Course)
            .Where(chapter => chapter.Course!.Slug == "tailwindcss")
            .Select(chapter => chapter.Title)
            .ToListAsync();

        Assert.Contains("Module 1 - Fondations utility-first", titles);
        Assert.Contains("Module 2 - Layout Tailwind", titles);
        Assert.Contains("Module 3 - Responsive design Tailwind", titles);
        Assert.Contains("Module 4 - Etats et interactions", titles);
        Assert.Contains("Module 5 - Formulaires et composants UI", titles);
        Assert.Contains("Module 6 - Dark mode et design system leger", titles);
        Assert.Contains("Module 7 - Projet Product Dashboard", titles);
        Assert.Contains("Boss Final TailwindCSS", titles);
    }

    [Fact]
    public async Task SeedData_LinksEveryReactLessonToSkillsAndThreeHints()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var lessons = await LessonsForLanguage(db, "react");

        Assert.NotEmpty(lessons);
        Assert.All(lessons, lesson =>
        {
            Assert.NotEmpty(lesson.LessonSkills);
            Assert.Equal(3, lesson.Hints.Count);
            var preview = LessonPreviewService.For(lesson);
            Assert.True(preview.SupportsPreview);
            Assert.Equal("react", preview.PreviewMode);
        });
    }

    [Fact]
    public async Task SeedData_LinksEveryReactNativeLessonToSkillsAndThreeHints()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var lessons = await LessonsForLanguage(db, "react-native");

        Assert.NotEmpty(lessons);
        Assert.All(lessons, lesson =>
        {
            Assert.NotEmpty(lesson.LessonSkills);
            Assert.Equal(3, lesson.Hints.Count);
            var preview = LessonPreviewService.For(lesson);
            Assert.False(preview.SupportsPreview);
            Assert.Equal("none", preview.PreviewMode);
        });
    }

    [Fact]
    public async Task SeedData_LinksEveryTailwindLessonToSkillsAndThreeHints()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var lessons = await LessonsForLanguage(db, "tailwindcss");

        Assert.NotEmpty(lessons);
        Assert.All(lessons, lesson =>
        {
            Assert.NotEmpty(lesson.LessonSkills);
            Assert.Equal(3, lesson.Hints.Count);
            var preview = LessonPreviewService.For(lesson);
            Assert.True(preview.SupportsPreview);
            Assert.Equal("tailwind", preview.PreviewMode);
        });
    }

    [Fact]
    public async Task SeedData_LinksEveryCssAndJavaScriptLessonToSkillsAndThreeHints()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var lessons = (await LessonsForLanguage(db, "css")).Concat(await LessonsForLanguage(db, "javascript")).ToList();

        Assert.NotEmpty(lessons);
        Assert.All(lessons, lesson =>
        {
            Assert.NotEmpty(lesson.LessonSkills);
            Assert.Equal(3, lesson.Hints.Count);
            var preview = LessonPreviewService.For(lesson);
            Assert.True(preview.SupportsPreview);
            Assert.False(string.IsNullOrWhiteSpace(preview.PreviewHtml));
        });
    }

    [Fact]
    public async Task StaticFrontCourses_HaveStructuredPracticalExercisesAndServerCorrections()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);

        var lessons = await db.Lessons
            .Include(lesson => lesson.Chapter)
            .ThenInclude(chapter => chapter!.Course)
            .Where(lesson =>
                lesson.Chapter!.Course!.Language == "react" ||
                lesson.Chapter!.Course!.Language == "react-native" ||
                lesson.Chapter!.Course!.Language == "tailwindcss" ||
                lesson.Chapter!.Course!.Language == "css" ||
                lesson.Chapter!.Course!.Language == "javascript")
            .ToListAsync();

        Assert.NotEmpty(lessons);
        Assert.All(lessons, lesson =>
        {
            Assert.Contains("Contexte concret:", lesson.ExercisePrompt);
            Assert.Contains("Entrees imposees:", lesson.ExercisePrompt);
            Assert.Contains("Sortie attendue:", lesson.ExercisePrompt);
            Assert.Contains("Contraintes de code:", lesson.ExercisePrompt);
            Assert.Contains("TODO", lesson.StarterCode, StringComparison.OrdinalIgnoreCase);
            Assert.False(string.IsNullOrWhiteSpace(lesson.FinalCorrection));
            Assert.Contains("Erreur frequente", lesson.CommonMistakes);
        });
    }

    [Fact]
    public void LessonDetailDto_ExposesPreviewFields()
    {
        var dto = new LessonDetailDto(
            1,
            "css-selectors",
            "Selecteurs CSS",
            "css",
            "html-css",
            "<main></main>",
            "",
            "",
            "root",
            true,
            "Objectif",
            "Resume",
            "Erreurs",
            "Explication",
            "Exemple",
            "Exercice",
            "Starter",
            "Succes",
            "Echec",
            "",
            10,
            false,
            LessonProgressStatus.Available);

        Assert.True(dto.SupportsPreview);
        Assert.Equal("html-css", dto.PreviewMode);
        Assert.Equal("<main></main>", dto.PreviewHtml);
    }

    [Fact]
    public async Task PreviewMetadata_MarksCssAndJavaScriptLessonsAsPreviewable()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var cssLesson = await LessonWithCourse(db, "css-selectors");
        var jsLesson = await LessonWithCourse(db, "js-query-selector");
        var cssBoss = await LessonWithCourse(db, "css-boss-final-responsive-product-page");
        var jsBoss = await LessonWithCourse(db, "javascript-boss-final-product-list");

        var cssPreview = LessonPreviewService.For(cssLesson);
        var jsPreview = LessonPreviewService.For(jsLesson);

        Assert.True(cssPreview.SupportsPreview);
        Assert.Equal("html-css", cssPreview.PreviewMode);
        Assert.Contains("product-card", cssPreview.PreviewHtml);
        Assert.True(jsPreview.SupportsPreview);
        Assert.Equal("javascript-dom", jsPreview.PreviewMode);
        Assert.Contains("product-form", jsPreview.PreviewHtml);
        Assert.Contains("counter", LessonPreviewService.For(await LessonWithCourse(db, "js-event-listener")).PreviewHtml);
        Assert.Contains("products", LessonPreviewService.For(await LessonWithCourse(db, "css-responsive-grid")).PreviewHtml);
        Assert.True(LessonPreviewService.For(cssBoss).SupportsPreview);
        Assert.True(LessonPreviewService.For(jsBoss).SupportsPreview);
    }

    [Fact]
    public async Task PreviewMetadata_KeepsNonVisualLessonsWithoutPreview()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await LessonWithCourse(db, "hello-world");

        var preview = LessonPreviewService.For(lesson);

        Assert.False(preview.SupportsPreview);
        Assert.Equal("none", preview.PreviewMode);
    }

    [Fact]
    public async Task ReactValidation_PassesUseStateLesson()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "react-usestate");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, """
        function App() {
          const [count, setCount] = useState(0);
          return <button onClick={() => setCount(count + 1)}>{count}</button>;
        }
        """);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task ReactValidation_ListMapRequiresMapAndKey()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "react-list-map");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, """
        export function ProductList({ products }: Props) {
          return <ul>{products.map((product) => <li>{product.name}</li>)}</ul>;
        }
        """);

        Assert.False(result.Passed);
        Assert.Contains(result.Tests, test => !test.Passed && test.Name.Contains("key="));
    }

    [Fact]
    public async Task ReactValidation_FormSubmitRequiresSubmitAndPreventDefault()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "react-form-submit");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, """
        function App() {
          const [name, setName] = useState('');
          function handleSubmit(event) {
            event.preventDefault();
          }
          return <form onSubmit={handleSubmit}><input value={name} onChange={(event) => setName(event.target.value)} /></form>;
        }
        """);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task ReactValidation_ApiProductsRequiresEffectFetchLoadingAndError()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "react-api-products");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, """
        function App() {
          const [products, setProducts] = useState([]);
          const [loading, setLoading] = useState(true);
          const [error, setError] = useState('');
          useEffect(() => {
            async function loadProducts() {
              try {
                const response = await fetch('/api/products');
                setProducts(await response.json());
              } catch (error) {
                setError('Failed');
              } finally {
                setLoading(false);
              }
            }
            loadProducts();
          }, []);
          return <p>{loading ? 'Loading' : error || products.length}</p>;
        }
        """);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task ReactBoss_ReturnsSkillScores()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var profile = await db.UserProfiles.FirstAsync();
        var boss = await db.IntermediateBosses.FirstAsync(item => item.Slug == "react-module-10-intermediate-boss");
        var service = new SkillProgressService(db);

        var result = await service.BuildBossResultAsync(profile, boss, [new TestResultDto("Snippet", true, "OK")], true);

        Assert.True(result.IsSuccess);
        Assert.NotEmpty(result.SkillResults);
        Assert.Contains(result.SkillResults, item => item.SkillSlug == "react-project");
    }

    [Fact]
    public async Task ReactFinalBoss_ReturnsSkillScores()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var profile = await db.UserProfiles.FirstAsync();
        var bossLesson = await db.Lessons
            .Include(item => item.Chapter)
            .ThenInclude(item => item!.Course)
            .Include(item => item.Tests)
            .FirstAsync(item => item.Slug == "react-boss-final-product-manager");
        var validation = new StaticSnippetValidationService();
        var partial = await validation.SubmitAsync(bossLesson, bossLesson.FinalCorrection);
        var service = new SkillProgressService(db);

        var result = await service.BuildLessonBossResultAsync(profile, bossLesson, partial.Tests, partial.Passed);

        Assert.True(result.IsSuccess);
        Assert.NotEmpty(result.SkillResults);
        Assert.Contains(result.SkillResults, item => item.SkillSlug == "react-project");
        Assert.Contains(result.SkillResults, item => item.SkillSlug == "react-context");
    }

    [Fact]
    public async Task ReactNativeValidation_PassesFlatListLesson()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "rn-flatlist");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, """
        export function ProductList({ products }: Props) {
          return <FlatList data={products} keyExtractor={(item) => String(item.id)} renderItem={({ item }) => <Text>{item.name}</Text>} />;
        }
        """);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task ReactNativeValidation_PassesControlledInputLesson()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "rn-controlled-input");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, """
        function ProductNameInput() {
          const [name, setName] = useState('');
          return <TextInput value={name} onChangeText={setName} />;
        }
        """);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task ReactNativeValidation_NavigationParamsRequiresRouteParams()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "rn-navigation-params");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, """
        function ProductDetailScreen({ productId }: Props) {
          return <Text>{productId}</Text>;
        }
        """);

        Assert.False(result.Passed);
        Assert.Contains(result.Tests, test => !test.Passed && test.Name.Contains("route.params"));
    }

    [Fact]
    public async Task ReactNativeValidation_AsyncStorageRequiresJsonSerialization()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "rn-async-storage");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, """
        await AsyncStorage.setItem('products', JSON.stringify(products));
        const savedProducts = JSON.parse(await AsyncStorage.getItem('products') ?? '[]');
        """);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task ReactNativeBoss_ReturnsSkillScores()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var profile = await db.UserProfiles.FirstAsync();
        var boss = await db.IntermediateBosses.FirstAsync(item => item.Slug == "rn-module-8-intermediate-boss");
        var service = new SkillProgressService(db);

        var result = await service.BuildBossResultAsync(profile, boss, [new TestResultDto("Snippet", true, "OK")], true);

        Assert.True(result.IsSuccess);
        Assert.NotEmpty(result.SkillResults);
        Assert.Contains(result.SkillResults, item => item.SkillSlug == "rn-project");
        Assert.Contains(result.SkillResults, item => item.SkillSlug == "rn-navigation");
    }

    [Fact]
    public async Task ReactNativeFinalBoss_ReturnsSkillScores()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var profile = await db.UserProfiles.FirstAsync();
        var bossLesson = await db.Lessons
            .Include(item => item.Chapter)
            .ThenInclude(item => item!.Course)
            .Include(item => item.Tests)
            .FirstAsync(item => item.Slug == "react-native-boss-final-product-app");
        var validation = new StaticSnippetValidationService();
        var partial = await validation.SubmitAsync(bossLesson, bossLesson.FinalCorrection);
        var service = new SkillProgressService(db);

        var result = await service.BuildLessonBossResultAsync(profile, bossLesson, partial.Tests, partial.Passed);

        Assert.True(result.IsSuccess);
        Assert.NotEmpty(result.SkillResults);
        Assert.Contains(result.SkillResults, item => item.SkillSlug == "rn-project");
        Assert.Contains(result.SkillResults, item => item.SkillSlug == "rn-safe-area");
        Assert.Contains(result.SkillResults, item => item.SkillSlug == "rn-loading-error");
    }

    [Fact]
    public async Task TailwindValidation_PassesFlexLesson()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "tailwind-flex");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, """
        <nav class="flex items-center justify-between gap-4">
          <span>Product Dashboard</span>
          <button>Add</button>
        </nav>
        """);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task TailwindValidation_PassesGridLesson()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "tailwind-grid");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, """
        <section class="grid grid-cols-3 gap-4">
          <article>Products</article>
        </section>
        """);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task TailwindValidation_PassesResponsiveGridLesson()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "tailwind-responsive-grid");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, """
        <section class="grid grid-cols-1 gap-4 md:grid-cols-2 lg:grid-cols-3">
          <article>Book</article>
        </section>
        """);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task TailwindValidation_PassesFormLesson()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "tailwind-form");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, """
        <form class="grid gap-3">
          <input type="text" class="rounded border p-2 focus:ring-2" />
          <input type="number" class="rounded border p-2 focus:ring-2" />
          <button class="rounded bg-blue-600 px-4 py-2 text-white">Save</button>
        </form>
        """);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task TailwindValidation_PassesDashboardResponsiveLesson()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "tailwind-dashboard-responsive");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, """
        <main class="grid gap-6 p-4 md:grid-cols-[16rem_1fr] lg:grid-cols-[18rem_1fr]">
          <aside class="rounded shadow">Sidebar</aside>
          <section class="grid gap-4">
            <nav class="flex rounded shadow">Navbar</nav>
            <div class="overflow-x-auto">
              <table class="w-full"><tr><td>Book</td></tr></table>
            </div>
          </section>
        </main>
        """);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task TailwindBoss_ReturnsSkillScores()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var profile = await db.UserProfiles.FirstAsync();
        var bossLesson = await db.Lessons
            .Include(item => item.Chapter)
            .ThenInclude(item => item!.Course)
            .Include(item => item.Tests)
            .FirstAsync(item => item.Slug == "tailwindcss-boss-final-dashboard");
        var validation = new StaticSnippetValidationService();
        var partial = await validation.SubmitAsync(bossLesson, bossLesson.FinalCorrection);
        var service = new SkillProgressService(db);

        var result = await service.BuildLessonBossResultAsync(profile, bossLesson, partial.Tests, partial.Passed);

        Assert.True(result.IsSuccess);
        Assert.NotEmpty(result.SkillResults);
        Assert.Contains(result.SkillResults, item => item.SkillSlug == "tailwind-dashboard");
    }

    [Fact]
    public async Task CssValidation_PassesBossFinalCorrection()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "css-boss-final-responsive-product-page");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, lesson.FinalCorrection);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task CssValidation_PassesResponsiveGridLesson()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "css-responsive-grid");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, """
        .products {
          display: grid;
          grid-template-columns: 1fr;
          gap: 16px;
        }

        @media (min-width: 768px) {
          .products { grid-template-columns: repeat(2, 1fr); }
        }

        @media (min-width: 1024px) {
          .products { grid-template-columns: repeat(3, 1fr); }
        }
        """);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task CssBoss_ReturnsSkillScores()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var profile = await db.UserProfiles.FirstAsync();
        var bossLesson = await db.Lessons
            .Include(item => item.Chapter)
            .ThenInclude(item => item!.Course)
            .Include(item => item.Tests)
            .FirstAsync(item => item.Slug == "css-boss-final-responsive-product-page");
        var validation = new StaticSnippetValidationService();
        var partial = await validation.SubmitAsync(bossLesson, bossLesson.FinalCorrection);
        var service = new SkillProgressService(db);

        var result = await service.BuildLessonBossResultAsync(profile, bossLesson, partial.Tests, partial.Passed);

        Assert.True(result.IsSuccess);
        Assert.NotEmpty(result.SkillResults);
        Assert.Contains(result.SkillResults, item => item.SkillSlug == "css-responsive");
    }

    [Fact]
    public async Task JavaScriptValidation_PassesBossFinalCorrection()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "javascript-boss-final-product-list");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, lesson.FinalCorrection);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task JavaScriptValidation_PassesEventListenerLesson()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "js-event-listener");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, """
        const counter = document.querySelector('#counter');
        let count = 0;

        counter.addEventListener('click', () => {
          count += 1;
          counter.textContent = String(count);
        });
        """);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task JavaScriptValidation_PassesRenderProductListLesson()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var lesson = await db.Lessons.Include(item => item.Tests).FirstAsync(item => item.Slug == "js-render-product-list");
        var service = new StaticSnippetValidationService();

        var result = await service.SubmitAsync(lesson, """
        const products = ['Book', 'Keyboard', 'Mouse'];
        const list = document.querySelector('#products');

        products.forEach((product) => {
          const item = document.createElement('li');
          item.textContent = product;
          list.append(item);
        });
        """);

        Assert.True(result.Passed);
    }

    [Fact]
    public async Task JavaScriptBoss_ReturnsSkillScores()
    {
        await using var db = CreateDbContext();
        await SeedData.EnsureSeededAsync(db);
        var profile = await db.UserProfiles.FirstAsync();
        var bossLesson = await db.Lessons
            .Include(item => item.Chapter)
            .ThenInclude(item => item!.Course)
            .Include(item => item.Tests)
            .FirstAsync(item => item.Slug == "javascript-boss-final-product-list");
        var validation = new StaticSnippetValidationService();
        var partial = await validation.SubmitAsync(bossLesson, bossLesson.FinalCorrection);
        var service = new SkillProgressService(db);

        var result = await service.BuildLessonBossResultAsync(profile, bossLesson, partial.Tests, partial.Passed);

        Assert.True(result.IsSuccess);
        Assert.NotEmpty(result.SkillResults);
        Assert.Contains(result.SkillResults, item => item.SkillSlug == "js-dom-manipulation");
    }

    private static Task<List<Lesson>> LessonsForLanguage(AppDbContext db, string language) =>
        db.Lessons
            .Include(lesson => lesson.Chapter)
            .ThenInclude(chapter => chapter!.Course)
            .Include(lesson => lesson.LessonSkills)
            .Include(lesson => lesson.Hints)
            .Where(lesson => lesson.Chapter!.Course!.Language == language)
            .ToListAsync();

    private static Task<Lesson> LessonWithCourse(AppDbContext db, string slug) =>
        db.Lessons
            .Include(lesson => lesson.Chapter)
            .ThenInclude(chapter => chapter!.Course)
            .FirstAsync(lesson => lesson.Slug == slug);

    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
}
