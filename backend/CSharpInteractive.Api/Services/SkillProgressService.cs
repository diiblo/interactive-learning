using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpInteractive.Api.Services;

public sealed class SkillProgressService(AppDbContext db)
{
    public async Task<IReadOnlyList<SkillDto>> GetSkillsAsync()
    {
        return await db.Skills
            .AsNoTracking()
            .OrderBy(skill => skill.CourseLanguage)
            .ThenBy(skill => skill.Name)
            .Select(skill => new SkillDto(skill.Id, skill.CourseLanguage, skill.Slug, skill.Name, skill.Description))
            .ToListAsync();
    }

    public async Task<IReadOnlyList<SkillProgressDto>> GetProgressAsync(UserProfile profile, string? courseLanguage = null)
    {
        await EnsureProgressRowsAsync(profile);
        var now = DateTime.UtcNow;
        var query = db.UserSkillProgress
            .AsNoTracking()
            .Include(progress => progress.Skill)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(courseLanguage))
        {
            query = query.Where(progress => progress.Skill!.CourseLanguage == courseLanguage);
        }

        var reviewLessons = await db.LessonSkills
            .AsNoTracking()
            .Include(item => item.Lesson)
            .Include(item => item.Skill)
            .GroupBy(item => item.SkillId)
            .Select(group => new { SkillId = group.Key, Slug = group.OrderBy(item => item.Lesson!.SortOrder).Select(item => item.Lesson!.Slug).FirstOrDefault() })
            .ToDictionaryAsync(item => item.SkillId, item => item.Slug);

        return await query
            .OrderBy(progress => progress.Skill!.CourseLanguage)
            .ThenBy(progress => progress.Skill!.Name)
            .Select(progress => new SkillProgressDto(
                progress.SkillId,
                progress.Skill!.CourseLanguage,
                progress.Skill.Slug,
                progress.Skill.Name,
                progress.Skill.Description,
                progress.MasteryPercent,
                progress.SuccessfulAttempts,
                progress.FailedAttempts,
                progress.NextReviewAt,
                progress.NextReviewAt != null && progress.NextReviewAt <= now ? SkillProgressStatus.ReviewDue : progress.Status,
                progress.NextReviewAt != null && progress.NextReviewAt <= now,
                reviewLessons.ContainsKey(progress.SkillId) ? reviewLessons[progress.SkillId] : null))
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ReviewItemDto>> GetDueReviewsAsync(UserProfile profile)
    {
        var progress = await GetProgressAsync(profile);
        var due = progress.Where(item => item.IsReviewDue).ToList();
        if (due.Count == 0)
        {
            return [];
        }

        var skillIds = due.Select(item => item.SkillId).ToList();
        var suggestions = await db.LessonSkills
            .AsNoTracking()
            .Include(item => item.Lesson)
            .Include(item => item.Skill)
            .Where(item => skillIds.Contains(item.SkillId))
            .GroupBy(item => item.SkillId)
            .ToDictionaryAsync(
                group => group.Key,
                group => group.OrderBy(item => item.Lesson!.ChapterId)
                    .ThenBy(item => item.Lesson!.SortOrder)
                    .Select(item => item.Lesson!.Slug)
                    .Distinct()
                    .Take(3)
                    .ToList() as IReadOnlyList<string>);

        return due.Select(item => new ReviewItemDto(
            item.SkillSlug,
            item.SkillName,
            item.CourseLanguage,
            item.MasteryPercent,
            item.Status,
            item.NextReviewAt,
            suggestions.TryGetValue(item.SkillId, out var list) ? list : [])).ToList();
    }

    public async Task MarkReviewCompletedAsync(UserProfile profile, int skillId)
    {
        var progress = await GetOrCreateProgressAsync(profile, skillId);
        progress.MasteryPercent = Math.Clamp(progress.MasteryPercent + 4, 0, 100);
        progress.NextReviewAt = NextReviewAt(progress.MasteryPercent);
        progress.Status = ResolveStatus(progress.MasteryPercent, progress.FailedAttempts);
        await db.SaveChangesAsync();
    }

    public async Task<SubmissionFeedbackDto> UpdateAfterLessonSubmissionAsync(
        UserProfile profile,
        Lesson lesson,
        IReadOnlyList<TestResultDto> tests,
        bool passed,
        ExecutionResultDto execution,
        string code)
    {
        var lessonSkills = await db.LessonSkills
            .Include(item => item.Skill)
            .Where(item => item.LessonId == lesson.Id)
            .ToListAsync();

        foreach (var item in lessonSkills)
        {
            var progress = await GetOrCreateProgressAsync(profile, item.SkillId);
            if (passed)
            {
                progress.SuccessfulAttempts += 1;
                progress.MasteryPercent = Math.Clamp(progress.MasteryPercent + 12 * Math.Max(1, item.Weight), 0, 100);
            }
            else
            {
                progress.FailedAttempts += 1;
                progress.MasteryPercent = Math.Clamp(progress.MasteryPercent - 4, 0, 100);
            }

            progress.Status = ResolveStatus(progress.MasteryPercent, progress.FailedAttempts);
            progress.NextReviewAt = NextReviewAt(progress.MasteryPercent);
        }

        await db.SaveChangesAsync();
        return await BuildFeedbackAsync(profile, lesson, tests, passed, execution, code);
    }

    public async Task<BossResultDto> BuildBossResultAsync(UserProfile profile, IntermediateBoss boss, IReadOnlyList<TestResultDto> tests, bool passed)
    {
        var moduleLessonIds = await db.Lessons
            .Where(lesson => lesson.ChapterId == boss.ModuleId && !lesson.IsBossFinal)
            .Select(lesson => lesson.Id)
            .ToListAsync();

        var skills = await db.LessonSkills
            .Include(item => item.Skill)
            .Where(item => moduleLessonIds.Contains(item.LessonId))
            .Select(item => item.Skill!)
            .Distinct()
            .ToListAsync();

        var passedRatio = tests.Count == 0 ? 0 : (int)Math.Round(100.0 * tests.Count(test => test.Passed) / tests.Count);
        var results = new List<SkillResultDto>();
        foreach (var skill in skills)
        {
            var progress = await GetOrCreateProgressAsync(profile, skill.Id);
            var score = Math.Clamp((progress.MasteryPercent + passedRatio) / 2, 0, 100);
            results.Add(new SkillResultDto(skill.Slug, skill.Name, score, ResolveStatus(score, progress.FailedAttempts), score >= 70 ? "Competence solide sur ce boss." : "Competence a retravailler."));
        }

        var strengths = results.Where(item => item.ScorePercent >= 70).Select(item => item.SkillName).ToList();
        var weaknesses = results.Where(item => item.ScorePercent < 70).Select(item => item.SkillName).ToList();

        return new BossResultDto(
            passed,
            passedRatio,
            passed ? "Boss valide. Les competences principales sont consolidees." : "Boss incomplet. Certaines competences demandent une revision ciblee.",
            results,
            strengths,
            weaknesses,
            weaknesses);
    }

    public async Task<BossResultDto> BuildLessonBossResultAsync(UserProfile profile, Lesson bossLesson, IReadOnlyList<TestResultDto> tests, bool passed)
    {
        var courseId = bossLesson.Chapter?.CourseId;
        var skills = await db.LessonSkills
            .Include(item => item.Skill)
            .Include(item => item.Lesson)
            .ThenInclude(lesson => lesson!.Chapter)
            .Where(item => item.Lesson!.Chapter!.CourseId == courseId && item.Skill != null)
            .Select(item => item.Skill!)
            .Distinct()
            .ToListAsync();

        var passedRatio = tests.Count == 0 ? 0 : (int)Math.Round(100.0 * tests.Count(test => test.Passed) / tests.Count);
        var results = new List<SkillResultDto>();
        foreach (var skill in skills)
        {
            var progress = await GetOrCreateProgressAsync(profile, skill.Id);
            var score = Math.Clamp((progress.MasteryPercent + passedRatio) / 2, 0, 100);
            results.Add(new SkillResultDto(skill.Slug, skill.Name, score, ResolveStatus(score, progress.FailedAttempts), score >= 70 ? "Competence mobilisee correctement." : "Revision conseillee avant de retenter un projet complet."));
        }

        var strengths = results.Where(item => item.ScorePercent >= 70).Select(item => item.SkillName).ToList();
        var weaknesses = results.Where(item => item.ScorePercent < 70).Select(item => item.SkillName).ToList();

        return new BossResultDto(
            passed,
            passedRatio,
            passed ? "Boss final valide. Le parcours est consolide." : "Boss final incomplet. Priorise les competences fragiles.",
            results,
            strengths,
            weaknesses,
            weaknesses);
    }

    private async Task EnsureProgressRowsAsync(UserProfile profile)
    {
        var skillIds = await db.Skills.Select(skill => skill.Id).ToListAsync();
        var existing = await db.UserSkillProgress
            .Where(progress => progress.UserProfileId == profile.Id)
            .Select(progress => progress.SkillId)
            .ToListAsync();

        foreach (var skillId in skillIds.Except(existing))
        {
            db.UserSkillProgress.Add(new UserSkillProgress { UserProfileId = profile.Id, SkillId = skillId });
        }

        await db.SaveChangesAsync();
    }

    private async Task<UserSkillProgress> GetOrCreateProgressAsync(UserProfile profile, int skillId)
    {
        var progress = await db.UserSkillProgress.FirstOrDefaultAsync(item => item.UserProfileId == profile.Id && item.SkillId == skillId);
        if (progress is not null)
        {
            return progress;
        }

        progress = new UserSkillProgress { UserProfileId = profile.Id, SkillId = skillId };
        db.UserSkillProgress.Add(progress);
        return progress;
    }

    private async Task<SubmissionFeedbackDto> BuildFeedbackAsync(UserProfile profile, Lesson lesson, IReadOnlyList<TestResultDto> tests, bool passed, ExecutionResultDto execution, string code)
    {
        var hints = await db.LessonHints
            .AsNoTracking()
            .Where(hint => hint.LessonId == lesson.Id)
            .OrderBy(hint => hint.HintLevel)
            .Select(hint => hint.Content)
            .ToListAsync();

        var skills = await db.LessonSkills
            .AsNoTracking()
            .Include(item => item.Skill)
            .Where(item => item.LessonId == lesson.Id)
            .Select(item => item.Skill!)
            .ToListAsync();

        var skillProgress = await GetProgressAsync(profile);
        var related = skills
            .Select(skill =>
            {
                var progress = skillProgress.FirstOrDefault(item => item.SkillId == skill.Id);
                return new RelatedSkillDto(skill.Slug, skill.Name, progress?.MasteryPercent ?? 0, progress?.Status ?? SkillProgressStatus.New);
            })
            .ToList();

        var failedTests = tests.Where(test => !test.Passed).ToList();
        var passedTests = tests.Where(test => test.Passed).ToList();
        var errorCategory = ResolveErrorCategory(execution, failedTests, code);
        var summary = BuildSummary(passed, errorCategory, failedTests, execution);
        var wentWell = BuildWhatWentWell(passed, passedTests, execution);
        var missing = BuildWhatIsMissing(passed, errorCategory, failedTests, execution);
        var suggested = await SuggestedReviewLessonsAsync(skills.Select(skill => skill.Id).ToList(), lesson.Slug);
        var shouldSuggest = !passed || related.Any(item => item.Status == SkillProgressStatus.Fragile || item.Status == SkillProgressStatus.Learning);

        return new SubmissionFeedbackDto(
            passed,
            summary,
            wentWell,
            missing,
            errorCategory,
            (hints.Count > 0 ? hints : DefaultHints(lesson, failedTests)).Take(3).ToList(),
            related,
            shouldSuggest ? suggested : []);
    }

    private async Task<IReadOnlyList<string>> SuggestedReviewLessonsAsync(IReadOnlyList<int> skillIds, string currentSlug)
    {
        return await db.LessonSkills
            .AsNoTracking()
            .Include(item => item.Lesson)
            .Where(item => skillIds.Contains(item.SkillId) && item.Lesson!.Slug != currentSlug)
            .OrderBy(item => item.Lesson!.ChapterId)
            .ThenBy(item => item.Lesson!.SortOrder)
            .Select(item => item.Lesson!.Slug)
            .Distinct()
            .Take(3)
            .ToListAsync();
    }

    private static IReadOnlyList<string> DefaultHints(Lesson lesson, IReadOnlyList<TestResultDto> failedTests) =>
    [
        "Relis l'objectif et identifie exactement ce qui doit etre produit.",
        failedTests.Count > 0 ? failedTests[0].Message : "Compare ton resultat avec les criteres de validation.",
        $"Travaille une petite partie a la fois: {lesson.ExercisePrompt}"
    ];

    private static SubmissionErrorCategory ResolveErrorCategory(ExecutionResultDto execution, IReadOnlyList<TestResultDto> failedTests, string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return SubmissionErrorCategory.EmptyCode;
        }

        if (!execution.Success)
        {
            return execution.Diagnostics.Any(item => item.Contains("exception", StringComparison.OrdinalIgnoreCase))
                ? SubmissionErrorCategory.RuntimeError
                : SubmissionErrorCategory.CompilationError;
        }

        var message = string.Join(' ', failedTests.Select(test => test.Message));
        if (string.IsNullOrWhiteSpace(message))
        {
            return SubmissionErrorCategory.Unknown;
        }

        if (message.Contains("ne doit pas contenir", StringComparison.OrdinalIgnoreCase))
        {
            return SubmissionErrorCategory.ForbiddenSnippetUsed;
        }

        if (message.Contains("doit contenir", StringComparison.OrdinalIgnoreCase))
        {
            return SubmissionErrorCategory.MissingRequiredSnippet;
        }

        if (message.Contains("sortie", StringComparison.OrdinalIgnoreCase) || message.Contains("resultat", StringComparison.OrdinalIgnoreCase))
        {
            return SubmissionErrorCategory.WrongOutput;
        }

        return failedTests.Count > 0 ? SubmissionErrorCategory.PartialSolution : SubmissionErrorCategory.Unknown;
    }

    private static string BuildSummary(bool passed, SubmissionErrorCategory category, IReadOnlyList<TestResultDto> failedTests, ExecutionResultDto execution)
    {
        if (passed)
        {
            return "Mission validee. Les criteres sont remplis.";
        }

        return category switch
        {
            SubmissionErrorCategory.EmptyCode => "Aucun code valide n'a ete detecte. Ecris une solution complete.",
            SubmissionErrorCategory.CompilationError => "Le code ne compile pas encore. Corrige les erreurs de syntaxe ou de type.",
            SubmissionErrorCategory.RuntimeError => "Le programme s'arrete avec une erreur a l'execution.",
            SubmissionErrorCategory.MissingRequiredSnippet => "Un element requis manque encore dans ta solution.",
            SubmissionErrorCategory.ForbiddenSnippetUsed => "Un element interdit est present dans ta solution.",
            SubmissionErrorCategory.WrongOutput => "La sortie produite ne correspond pas a l'attendu.",
            SubmissionErrorCategory.PartialSolution => failedTests.Count > 0
                ? $"{failedTests.Count} point(s) restent a corriger pour valider l'exercice."
                : "La solution est incomplete.",
            _ => execution.Success ? "Le code a ete analyse, mais il manque encore un element." : "Le code doit etre ajuste pour passer la validation."
        };
    }

    private static IReadOnlyList<string> BuildWhatWentWell(bool passed, IReadOnlyList<TestResultDto> passedTests, ExecutionResultDto execution)
    {
        var items = new List<string>();
        if (passed)
        {
            items.Add("Tous les criteres de validation sont remplis.");
        }

        if (passedTests.Count > 0)
        {
            items.AddRange(passedTests.Take(2).Select(test => test.Name));
        }

        if (execution.Success && !string.IsNullOrWhiteSpace(execution.Output))
        {
            items.Add("Le programme s'execute sans erreur.");
        }

        if (items.Count == 0)
        {
            items.Add("Le code a bien ete analyse.");
        }

        return items;
    }

    private static IReadOnlyList<string> BuildWhatIsMissing(bool passed, SubmissionErrorCategory category, IReadOnlyList<TestResultDto> failedTests, ExecutionResultDto execution)
    {
        if (passed)
        {
            return ["Aucun ajustement requis."];
        }

        if (!execution.Success)
        {
            return ["Corrige les erreurs de compilation avant de retenter."];
        }

        var missing = failedTests.Select(test => $"{test.Name}: {test.Message}").ToList();
        if (missing.Count == 0)
        {
            missing.Add(category switch
            {
                SubmissionErrorCategory.WrongOutput => "Ajuste la sortie pour qu'elle corresponde exactement a l'attendu.",
                SubmissionErrorCategory.MissingRequiredSnippet => "Ajoute le morceau de code attendu par la consigne.",
                SubmissionErrorCategory.ForbiddenSnippetUsed => "Retire l'element interdit signale par la consigne.",
                SubmissionErrorCategory.PartialSolution => "Complete les elements manquants pour tout valider.",
                _ => "Verifie que chaque critere de validation est couvert."
            });
        }

        return missing;
    }

    private static DateTime NextReviewAt(int masteryPercent)
    {
        var days = masteryPercent switch
        {
            < 26 => 1,
            < 60 => 3,
            < 85 => 7,
            _ => 14
        };
        return DateTime.UtcNow.AddDays(days);
    }

    private static SkillProgressStatus ResolveStatus(int masteryPercent, int failedAttempts)
    {
        if (failedAttempts >= 2 && masteryPercent < 60)
        {
            return SkillProgressStatus.Fragile;
        }

        return masteryPercent switch
        {
            <= 25 => SkillProgressStatus.New,
            <= 59 => SkillProgressStatus.Learning,
            <= 84 => SkillProgressStatus.Solid,
            _ => SkillProgressStatus.Mastered
        };
    }
}
