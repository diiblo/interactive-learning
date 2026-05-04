"use client";

import { useCallback, useEffect, useMemo, useRef, useState } from "react";
import { Play, Send, Loader2, AlertTriangle, PanelRightClose, PanelRightOpen, Settings, Plus, Trash2, Bot } from "lucide-react";
import { apiClient } from "@/lib/api-client";
import { findFirstAvailableLesson, findLessonInMap } from "@/lib/lesson-state";
import type {
  AiProviderConfigDto,
  CourseMapDto,
  CourseSummaryDto,
  ExecutionResultDto,
  IntermediateBossDetailDto,
  IntermediateBossHintDto,
  IntermediateBossMapItemDto,
  LessonDetailDto,
  LessonMapItemDto,
  ProfileDto,
  ProgressDto,
  ReviewItemDto,
  SkillProgressDto,
  SqlSchemaDto,
  SubmitResultDto,
} from "@/types/api";
import { BadgeGrid } from "./badge-grid";
import { BossFinalWorkspace } from "./boss-final-workspace";
import { CodeEditor } from "./code-editor";
import { CourseSidebar } from "./course-sidebar";
import { FeedbackPanel } from "./feedback-panel";
import { IntermediateBossContent } from "./intermediate-boss-content";
import { IntermediateBossHelp } from "./intermediate-boss-help";
import { LessonContent } from "./lesson-content";
import { LessonUnlockCard } from "./lesson-unlock-card";
import { ProgressHeader } from "./progress-header";
import { RunConsole } from "./run-console";
import { ReviewQueue } from "./review-queue";
import { SkillMap } from "./skill-map";
import { SqlResultGrid } from "./sql-result-grid";
import { SqlSafetyNotice } from "./sql-safety-notice";
import { SqlSchemaPanel } from "./sql-schema-panel";
import { XpLevelCard } from "./xp-level-card";

type AppShellProps = {
  initialLessonId?: number;
  initialBossFinal?: boolean;
};

type ValidationMode = "local" | "ai";

const AI_PROVIDERS_STORAGE_KEY = "interactive-learning.aiProviders";
const VALIDATION_MODE_STORAGE_KEY = "interactive-learning.validationMode";

const providerDefaults: Record<string, { name: string; model: string; baseUrl: string }> = {
  google: { name: "Google Gemini", model: "gemini-1.5-flash", baseUrl: "" },
  openrouter: { name: "OpenRouter", model: "openai/gpt-4o-mini", baseUrl: "https://openrouter.ai/api/v1" },
  "ollama-cloud": { name: "Ollama Cloud", model: "llama3.1", baseUrl: "" },
  "ollama-local": { name: "Ollama local", model: "llama3.1", baseUrl: "http://host.docker.internal:11434" },
  custom: { name: "Compatible OpenAI", model: "gpt-4o-mini", baseUrl: "" },
};

function initialEditorCode(language: string) {
  return language === "csharp" ? "using System;\n\n// Ecris ton code ici\n" : "";
}

function createAiProvider(type = "openrouter"): AiProviderConfigDto {
  const defaults = providerDefaults[type] ?? providerDefaults.openrouter;
  const id = typeof crypto !== "undefined" && "randomUUID" in crypto ? crypto.randomUUID() : `${Date.now()}-${Math.random()}`;
  return {
    id,
    type,
    name: defaults.name,
    apiKey: "",
    model: defaults.model,
    baseUrl: defaults.baseUrl,
  };
}

export function AppShell({ initialLessonId, initialBossFinal = false }: AppShellProps) {
  const [courses, setCourses] = useState<CourseSummaryDto[]>([]);
  const [selectedCourseId, setSelectedCourseId] = useState<number | null>(null);
  const [courseMap, setCourseMap] = useState<CourseMapDto | null>(null);
  const [profile, setProfile] = useState<ProfileDto | null>(null);
  const [progress, setProgress] = useState<ProgressDto | null>(null);
  const [skillProgress, setSkillProgress] = useState<SkillProgressDto[]>([]);
  const [reviewQueue, setReviewQueue] = useState<ReviewItemDto[]>([]);
  const reviewSkillIdMap = useRef<Map<string, number>>(new Map());
  const [lesson, setLesson] = useState<LessonDetailDto | null>(null);
  const [intermediateBoss, setIntermediateBoss] = useState<IntermediateBossDetailDto | null>(null);
  const [intermediateBossHints, setIntermediateBossHints] = useState<IntermediateBossHintDto[]>([]);
  const [intermediateBossSolution, setIntermediateBossSolution] = useState<string | null>(null);
  const [code, setCode] = useState("");
  const [output, setOutput] = useState("");
  const [diagnostics, setDiagnostics] = useState<string[]>([]);
  const [sqlSchema, setSqlSchema] = useState<SqlSchemaDto | null>(null);
  const [sqlColumns, setSqlColumns] = useState<string[]>([]);
  const [sqlRows, setSqlRows] = useState<Record<string, string | number | boolean | null>[]>([]);
  const [lastSubmit, setLastSubmit] = useState<SubmitResultDto | null>(null);
  const [isInfoPanelCollapsed, setIsInfoPanelCollapsed] = useState(false);
  const [validationMode, setValidationMode] = useState<ValidationMode>("local");
  const [aiProviders, setAiProviders] = useState<AiProviderConfigDto[]>([]);
  const [isAiSettingsOpen, setIsAiSettingsOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [isRunning, setIsRunning] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const coursesRef = useRef<CourseSummaryDto[]>([]);
  const selectedCourseIdRef = useRef<number | null>(null);

  const activeMapItem = useMemo(() => (lesson ? findLessonInMap(courseMap, lesson.id) : null), [courseMap, lesson]);
  const activeEditorLanguage = intermediateBoss?.editorLanguage ?? lesson?.editorLanguage ?? "csharp";

  const refreshProgress = useCallback(async () => {
    const [nextProfile, nextProgress] = await Promise.all([apiClient.getProfile(), apiClient.getProgress()]);
    setProfile(nextProfile);
    setProgress(nextProgress);
  }, []);

  const refreshSkillProgress = useCallback(async (courseId?: number | null) => {
    const id = courseId ?? selectedCourseIdRef.current;
    const course = coursesRef.current.find((candidate) => candidate.id === id);
    if (!course) {
      setSkillProgress([]);
      reviewSkillIdMap.current = new Map();
      return;
    }

    const progress = await apiClient.getSkillProgress(course.language);
    setSkillProgress(progress);
    reviewSkillIdMap.current = new Map(progress.map((item) => [item.skillSlug, item.skillId]));
  }, []);

  const refreshReviewQueue = useCallback(async (courseId?: number | null) => {
    const id = courseId ?? selectedCourseIdRef.current;
    const course = coursesRef.current.find((candidate) => candidate.id === id);
    if (!course) {
      setReviewQueue([]);
      return;
    }

    const due = await apiClient.getDueReviews();
    setReviewQueue(due.filter((item) => item.courseLanguage === course.language));
  }, []);

  const loadLesson = useCallback(async (item: LessonMapItemDto, courseIdOverride?: number) => {
    if (item.isLocked) {
      return;
    }

    setError(null);
    setLastSubmit(null);
    setOutput("");
    setDiagnostics([]);
    setSqlColumns([]);
    setSqlRows([]);
    setIntermediateBoss(null);
    setIntermediateBossHints([]);
    setIntermediateBossSolution(null);
    const courseId = courseIdOverride ?? selectedCourseIdRef.current;
    const course = coursesRef.current.find((candidate) => candidate.id === courseId);
    const detail = course?.language === "sqlserver" ? await apiClient.getSqlLesson(item.id) : await apiClient.getLesson(item.id);
    setLesson(detail);
    setCode(initialEditorCode(detail.editorLanguage));

    if (detail.editorLanguage === "sql") {
      setSqlSchema(await apiClient.getSqlSchema());
    } else {
      setSqlSchema(null);
    }
  }, []);

  const loadIntermediateBoss = useCallback(async (item: IntermediateBossMapItemDto) => {
    if (item.isLocked) {
      return;
    }

    setError(null);
    setLastSubmit(null);
    setOutput("");
    setDiagnostics([]);
    setSqlColumns([]);
    setSqlRows([]);
    setLesson(null);
    setIntermediateBossHints([]);
    setIntermediateBossSolution(null);

    const detail = await apiClient.getIntermediateBoss(item.moduleId);
    setIntermediateBoss(detail);
    setCode(initialEditorCode(detail.editorLanguage));

    if (detail.editorLanguage === "sql") {
      setSqlSchema(await apiClient.getSqlSchema());
    } else {
      setSqlSchema(null);
    }
  }, []);

  const loadCourse = useCallback(async (courseId: number, preferredLessonId?: number, preferBossFinal = false) => {
    const [nextMap, nextProgress] = await Promise.all([apiClient.getCourseMap(courseId), apiClient.getProgress()]);
    selectedCourseIdRef.current = courseId;
    setSelectedCourseId(courseId);
    setCourseMap(nextMap);
    setProgress(nextProgress);
    await refreshSkillProgress(courseId);
    await refreshReviewQueue(courseId);

    const initialLesson =
      (preferredLessonId ? findLessonInMap(nextMap, preferredLessonId) : null) ??
      (preferBossFinal ? nextMap.bossFinal : null) ??
      findFirstAvailableLesson(nextMap);

    if (initialLesson && !initialLesson.isLocked) {
      await loadLesson(initialLesson, courseId);
    } else {
      setLesson(null);
      setIntermediateBoss(null);
      setCode("");
    }
  }, [loadLesson, refreshReviewQueue, refreshSkillProgress]);

  const loadInitialData = useCallback(async () => {
    setIsLoading(true);
    setError(null);

    try {
      const [nextCourses, nextProfile] = await Promise.all([apiClient.getCourses(), apiClient.getProfile()]);
      coursesRef.current = nextCourses;
      setCourses(nextCourses);
      setProfile(nextProfile);

      const firstCourse = nextCourses[0];
      if (!firstCourse) {
        setError("Aucun cours disponible.");
        return;
      }

      await loadCourse(firstCourse.id, initialLessonId, initialBossFinal);
    } catch (caught) {
      setError(caught instanceof Error ? caught.message : "Impossible de charger l'application.");
    } finally {
      setIsLoading(false);
    }
  }, [initialBossFinal, initialLessonId, loadCourse]);

  useEffect(() => {
    // eslint-disable-next-line react-hooks/set-state-in-effect
    void loadInitialData();
  }, [loadInitialData]);

  useEffect(() => {
    try {
      const storedMode = localStorage.getItem(VALIDATION_MODE_STORAGE_KEY);
      if (storedMode === "local" || storedMode === "ai") {
        setValidationMode(storedMode);
      }

      const storedProviders = localStorage.getItem(AI_PROVIDERS_STORAGE_KEY);
      if (storedProviders) {
        const parsed = JSON.parse(storedProviders) as AiProviderConfigDto[];
        if (Array.isArray(parsed)) {
          setAiProviders(parsed);
        }
      }
    } catch {
      setAiProviders([]);
    }
  }, []);

  useEffect(() => {
    localStorage.setItem(VALIDATION_MODE_STORAGE_KEY, validationMode);
  }, [validationMode]);

  useEffect(() => {
    localStorage.setItem(AI_PROVIDERS_STORAGE_KEY, JSON.stringify(aiProviders));
  }, [aiProviders]);

  const runCode = async () => {
    if (!lesson && !intermediateBoss) {
      return;
    }

    setIsRunning(true);
    setOutput("Execution en cours...\n");
    setDiagnostics([]);
    setError(null);

    try {
      const result: ExecutionResultDto = intermediateBoss
        ? await apiClient.runIntermediateBoss(intermediateBoss.id, code)
        : lesson!.editorLanguage === "sql"
          ? await apiClient.runSqlLesson(lesson!.id, code)
          : lesson!.isBossFinal && lesson!.editorLanguage === "csharp"
            ? await apiClient.runBossFinal(code)
            : await apiClient.runLesson(lesson!.id, code);
      setOutput(result.output);
      setDiagnostics(result.diagnostics);
      setSqlColumns(result.columns ?? []);
      setSqlRows(result.rows ?? []);
    } catch (caught) {
      setError(caught instanceof Error ? caught.message : "Erreur pendant l'execution.");
    } finally {
      setIsRunning(false);
    }
  };

  const submitCode = async () => {
    if (!lesson && !intermediateBoss) {
      return;
    }

    setIsRunning(true);
    setOutput("Correction en cours...\n");
    setDiagnostics([]);
    setError(null);

    try {
      if (validationMode === "ai" && aiProviders.length === 0) {
        setIsAiSettingsOpen(true);
        throw new Error("Ajoute au moins un fournisseur IA avant de soumettre avec l'IA.");
      }

      const submitOptions = {
        validationMode,
        aiProviders: validationMode === "ai" ? aiProviders : [],
      };
      const result = intermediateBoss
        ? await apiClient.submitIntermediateBoss(intermediateBoss.id, code, submitOptions)
        : lesson!.editorLanguage === "sql"
          ? await apiClient.submitSqlLesson(lesson!.id, code, submitOptions)
          : lesson!.isBossFinal && lesson!.editorLanguage === "csharp"
            ? await apiClient.submitBossFinal(code, submitOptions)
            : await apiClient.submitLesson(lesson!.id, code, submitOptions);
      setLastSubmit(result);
      setOutput(result.output);
      setSqlColumns([]);
      setSqlRows([]);
      await refreshProgress();
      await refreshSkillProgress(selectedCourseId);
      await refreshReviewQueue(selectedCourseId);
      if (selectedCourseId) {
        setCourseMap(await apiClient.getCourseMap(selectedCourseId));
      }
      if (intermediateBoss) {
        setIntermediateBoss(await apiClient.getIntermediateBoss(intermediateBoss.moduleId));
      }
    } catch (caught) {
      setError(caught instanceof Error ? caught.message : "Erreur pendant la correction.");
    } finally {
      setIsRunning(false);
    }
  };

  const updateAiProvider = (id: string, patch: Partial<AiProviderConfigDto>) => {
    setAiProviders((providers) =>
      providers.map((provider) => {
        if (provider.id !== id) {
          return provider;
        }

        const nextType = patch.type ?? provider.type;
        const defaults = providerDefaults[nextType] ?? providerDefaults.custom;
        return {
          ...provider,
          ...patch,
          name: patch.type ? defaults.name : patch.name ?? provider.name,
          model: patch.type ? defaults.model : patch.model ?? provider.model,
          baseUrl: patch.type ? defaults.baseUrl : patch.baseUrl ?? provider.baseUrl,
        };
      }),
    );
  };

  const reviewSkill = async (lessonSlug: string | null, skillId?: number) => {
    if (skillId && skillId > 0) {
      await apiClient.completeReview(skillId);
      await refreshSkillProgress(selectedCourseId);
      await refreshReviewQueue(selectedCourseId);
    }
    if (!lessonSlug || !courseMap) {
      return;
    }

    const target = courseMap.chapters.flatMap((chapter) => chapter.lessons).find((item) => item.slug === lessonSlug);
    if (target && !target.isLocked) {
      await loadLesson(target);
    }
  };

  const revealIntermediateBossHint = async () => {
    if (!intermediateBoss) {
      return;
    }

    setIsRunning(true);
    setError(null);
    try {
      const result = await apiClient.revealIntermediateBossHint(intermediateBoss.id);
      setIntermediateBossHints(result.hints);
      setIntermediateBoss(await apiClient.getIntermediateBoss(intermediateBoss.moduleId));
    } catch (caught) {
      setError(caught instanceof Error ? caught.message : "Impossible d'afficher un indice.");
    } finally {
      setIsRunning(false);
    }
  };

  const revealIntermediateBossSolution = async () => {
    if (!intermediateBoss) {
      return;
    }

    setIsRunning(true);
    setError(null);
    try {
      const result = await apiClient.revealIntermediateBossSolution(intermediateBoss.id);
      setIntermediateBossSolution(result.solution);
    } catch (caught) {
      setError(caught instanceof Error ? caught.message : "La solution est disponible apres une tentative echouee.");
    } finally {
      setIsRunning(false);
    }
  };

  return (
    <div className="flex h-screen flex-col bg-[var(--color-background)] text-[var(--color-text)]">
      <ProgressHeader profile={profile} progress={progress} />

      <div className="flex items-center gap-2 border-b border-[var(--color-border)] bg-[#161b22] px-4 py-2">
        {courses.map((course) => (
          <button
            key={course.id}
            type="button"
            onClick={() => void loadCourse(course.id)}
            className={`rounded-md px-3 py-2 text-sm font-semibold transition ${
              selectedCourseId === course.id
                ? "bg-[var(--color-primary)] text-white"
                : "border border-[var(--color-border)] bg-[#1e2329] text-[var(--color-text-muted)] hover:text-white"
            }`}
          >
            {course.title}
          </button>
        ))}
      </div>

      <main className="flex min-h-0 flex-1 flex-col md:flex-row">
        <CourseSidebar
          courseMap={courseMap}
          activeLessonId={lesson?.id ?? null}
          activeIntermediateBossId={intermediateBoss?.id ?? null}
          onSelectLesson={loadLesson}
          onSelectIntermediateBoss={loadIntermediateBoss}
        />

        <section
          className={`grid min-h-0 flex-1 grid-rows-[minmax(220px,36%)_1fr] md:grid-rows-1 ${
            isInfoPanelCollapsed
              ? "md:grid-cols-[minmax(280px,36%)_1fr_48px]"
              : "md:grid-cols-[minmax(280px,34%)_1fr_360px]"
          }`}
        >
          <div className="min-h-0 border-b border-[var(--color-border)] bg-[var(--color-surface)] md:border-b-0 md:border-r">
            {intermediateBoss ? <IntermediateBossContent boss={intermediateBoss} /> : <LessonContent lesson={lesson} />}
          </div>

          <div className="flex min-h-0 flex-col bg-[#0d1117]">
            {lesson?.isBossFinal && lesson.editorLanguage === "csharp" && <BossFinalWorkspace />}
            {lastSubmit?.passed && intermediateBoss ? (
              <div className="border-b border-[var(--color-success)]/40 bg-[#102018] px-4 py-3 text-sm font-semibold text-[var(--color-success)]">
                Monstre vaincu
              </div>
            ) : null}
            <div className="flex items-center justify-between border-b border-[var(--color-border)] bg-[#161b22] px-4 py-3">
              <div>
                <p className="text-sm font-semibold text-white">{activeEditorLanguage === "sql" ? "query.sql" : "Program.cs"}</p>
                <p className="text-xs text-[var(--color-text-muted)]">
                  {intermediateBoss ? intermediateBoss.status : activeMapItem?.status ?? "Locked"}
                </p>
              </div>
              <div className="flex items-center gap-2">
                <div className="hidden rounded-md border border-[var(--color-border)] bg-[#1e2329] p-1 md:flex">
                  <button
                    type="button"
                    onClick={() => setValidationMode("local")}
                    className={`rounded px-2 py-1 text-xs font-semibold transition ${
                      validationMode === "local" ? "bg-[var(--color-primary)] text-white" : "text-[var(--color-text-muted)] hover:text-white"
                    }`}
                  >
                    Local
                  </button>
                  <button
                    type="button"
                    onClick={() => setValidationMode("ai")}
                    className={`inline-flex items-center gap-1 rounded px-2 py-1 text-xs font-semibold transition ${
                      validationMode === "ai" ? "bg-[var(--color-primary)] text-white" : "text-[var(--color-text-muted)] hover:text-white"
                    }`}
                  >
                    <Bot size={13} />
                    IA
                  </button>
                </div>
                <button
                  type="button"
                  onClick={() => setIsAiSettingsOpen(true)}
                  className="inline-flex h-9 w-9 items-center justify-center rounded-md border border-[var(--color-border)] bg-[#1e2329] text-[var(--color-text-muted)] transition hover:border-[var(--color-primary)] hover:text-white"
                  aria-label="Configurer la validation IA"
                  title="Configurer la validation IA"
                >
                  <Settings size={16} />
                </button>
                <button
                  type="button"
                  onClick={() => setIsInfoPanelCollapsed((value) => !value)}
                  className="hidden items-center gap-2 rounded-md border border-[var(--color-border)] bg-[#1e2329] px-3 py-2 text-sm font-semibold text-white transition hover:border-[var(--color-primary)] md:inline-flex"
                  aria-label={isInfoPanelCollapsed ? "Afficher le panneau d'informations" : "Replier le panneau d'informations"}
                  aria-expanded={!isInfoPanelCollapsed}
                  title={isInfoPanelCollapsed ? "Afficher le panneau d'informations" : "Replier le panneau d'informations"}
                >
                  {isInfoPanelCollapsed ? <PanelRightOpen size={16} /> : <PanelRightClose size={16} />}
                  Infos
                </button>
                <button
                  type="button"
                  onClick={runCode}
                  disabled={(!lesson && !intermediateBoss) || isRunning}
                  className="inline-flex items-center gap-2 rounded-md border border-[var(--color-border)] bg-[#1e2329] px-3 py-2 text-sm font-semibold text-white transition hover:border-[var(--color-primary)] disabled:cursor-not-allowed disabled:opacity-50"
                >
                  {isRunning ? <Loader2 size={16} className="animate-spin" /> : <Play size={16} />}
                  Executer
                </button>
                <button
                  type="button"
                  onClick={submitCode}
                  disabled={(!lesson && !intermediateBoss) || isRunning}
                  className="inline-flex items-center gap-2 rounded-md bg-[var(--color-success)] px-3 py-2 text-sm font-semibold text-white transition hover:bg-[#3fb950] disabled:cursor-not-allowed disabled:opacity-50"
                >
                  <Send size={16} />
                  Soumettre
                </button>
              </div>
            </div>

            {activeEditorLanguage === "sql" ? <SqlSafetyNotice schema={sqlSchema} /> : null}

            <div className="min-h-0 flex-1">
              <CodeEditor code={code} language={activeEditorLanguage} onChange={setCode} />
            </div>

            {activeEditorLanguage === "sql" ? <SqlResultGrid columns={sqlColumns} rows={sqlRows} /> : null}

            <div className="h-48 border-t border-[var(--color-border)]">
              <RunConsole output={output} diagnostics={diagnostics} onClear={() => { setOutput(""); setDiagnostics([]); }} />
            </div>
          </div>

          <div className="hidden min-h-0 border-l border-[var(--color-border)] bg-[var(--color-surface)] md:flex">
            {isInfoPanelCollapsed ? (
              <div className="flex h-full w-full flex-col items-center border-l border-[var(--color-border)] bg-[#161b22] py-3">
                <button
                  type="button"
                  onClick={() => setIsInfoPanelCollapsed(false)}
                  className="inline-flex h-9 w-9 items-center justify-center rounded-md border border-[var(--color-border)] bg-[#1e2329] text-[var(--color-text-muted)] transition hover:border-[var(--color-primary)] hover:text-white"
                  aria-label="Afficher le panneau d'informations"
                  aria-expanded={!isInfoPanelCollapsed}
                  title="Afficher le panneau d'informations"
                >
                  <PanelRightOpen size={18} />
                </button>
              </div>
            ) : (
              <aside className="flex min-h-0 w-full flex-col">
                <div className="flex items-center justify-between border-b border-[var(--color-border)] bg-[#161b22] px-4 py-3">
                  <p className="text-sm font-semibold text-white">Informations</p>
                  <button
                    type="button"
                    onClick={() => setIsInfoPanelCollapsed(true)}
                    className="inline-flex h-8 w-8 items-center justify-center rounded-md border border-[var(--color-border)] bg-[#1e2329] text-[var(--color-text-muted)] transition hover:border-[var(--color-primary)] hover:text-white"
                    aria-label="Replier le panneau d'informations"
                    aria-expanded={!isInfoPanelCollapsed}
                    title="Replier le panneau d'informations"
                  >
                    <PanelRightClose size={17} />
                  </button>
                </div>
                <div className="min-h-0 flex-1 overflow-y-auto">
                  <FeedbackPanel result={lastSubmit} />
                  <IntermediateBossHelp
                    boss={intermediateBoss}
                    result={lastSubmit}
                    hints={intermediateBossHints}
                    solution={intermediateBossSolution}
                    isBusy={isRunning}
                    onRevealHint={revealIntermediateBossHint}
                    onRevealSolution={revealIntermediateBossSolution}
                  />
                  {activeEditorLanguage === "sql" ? <SqlSchemaPanel schema={sqlSchema} /> : null}
                  <div className="space-y-3 border-t border-[var(--color-border)] bg-[var(--color-surface)] p-4">
                    <LessonUnlockCard unlockedCount={lastSubmit?.unlockedLessonIds.length ?? 0} />
                    <XpLevelCard profile={profile} />
                    <BadgeGrid badges={profile?.badges ?? []} />
                  </div>
                  <ReviewQueue
                    reviews={reviewQueue}
                    onReview={(skillSlug, lessonSlug) => void reviewSkill(lessonSlug, reviewSkillIdMap.current.get(skillSlug))}
                  />
                  <SkillMap skills={skillProgress} onReview={(lessonSlug, skillId) => void reviewSkill(lessonSlug, skillId)} />
                </div>
              </aside>
            )}
          </div>
        </section>
      </main>

      {isAiSettingsOpen && (
        <div className="fixed inset-0 z-40 flex items-center justify-center bg-black/60 p-4">
          <div className="flex max-h-[88vh] w-full max-w-3xl flex-col overflow-hidden rounded-md border border-[var(--color-border)] bg-[#161b22] shadow-2xl">
            <div className="flex items-center justify-between border-b border-[var(--color-border)] px-4 py-3">
              <div>
                <p className="text-sm font-semibold text-white">Validation IA</p>
                <p className="text-xs text-[var(--color-text-muted)]">Les cles restent dans ce navigateur et sont essayees dans l'ordre.</p>
              </div>
              <button
                type="button"
                onClick={() => setIsAiSettingsOpen(false)}
                className="rounded-md border border-[var(--color-border)] bg-[#1e2329] px-3 py-2 text-sm font-semibold text-white transition hover:border-[var(--color-primary)]"
              >
                Fermer
              </button>
            </div>

            <div className="flex items-center gap-2 border-b border-[var(--color-border)] px-4 py-3">
              <button
                type="button"
                onClick={() => setValidationMode("local")}
                className={`rounded-md px-3 py-2 text-sm font-semibold transition ${
                  validationMode === "local" ? "bg-[var(--color-primary)] text-white" : "border border-[var(--color-border)] bg-[#1e2329] text-[var(--color-text-muted)] hover:text-white"
                }`}
              >
                Code local
              </button>
              <button
                type="button"
                onClick={() => setValidationMode("ai")}
                className={`inline-flex items-center gap-2 rounded-md px-3 py-2 text-sm font-semibold transition ${
                  validationMode === "ai" ? "bg-[var(--color-primary)] text-white" : "border border-[var(--color-border)] bg-[#1e2329] text-[var(--color-text-muted)] hover:text-white"
                }`}
              >
                <Bot size={16} />
                IA
              </button>
              <button
                type="button"
                onClick={() => setAiProviders((providers) => [...providers, createAiProvider()])}
                className="ml-auto inline-flex items-center gap-2 rounded-md border border-[var(--color-border)] bg-[#1e2329] px-3 py-2 text-sm font-semibold text-white transition hover:border-[var(--color-primary)]"
              >
                <Plus size={16} />
                Ajouter
              </button>
            </div>

            <div className="min-h-0 flex-1 space-y-3 overflow-y-auto p-4">
              {aiProviders.length === 0 ? (
                <div className="rounded-md border border-dashed border-[var(--color-border)] p-4 text-sm text-[var(--color-text-muted)]">
                  Aucun fournisseur configure. Ajoute Google, OpenRouter, Ollama Cloud, Ollama local ou une API compatible OpenAI.
                </div>
              ) : null}

              {aiProviders.map((provider, index) => (
                <div key={provider.id} className="rounded-md border border-[var(--color-border)] bg-[#0d1117] p-3">
                  <div className="mb-3 flex items-center justify-between">
                    <p className="text-sm font-semibold text-white">Priorite {index + 1}</p>
                    <button
                      type="button"
                      onClick={() => setAiProviders((providers) => providers.filter((item) => item.id !== provider.id))}
                      className="inline-flex h-8 w-8 items-center justify-center rounded-md border border-[var(--color-border)] bg-[#1e2329] text-[var(--color-text-muted)] transition hover:border-[var(--color-error)] hover:text-white"
                      aria-label="Supprimer ce fournisseur"
                      title="Supprimer"
                    >
                      <Trash2 size={15} />
                    </button>
                  </div>
                  <div className="grid gap-3 md:grid-cols-2">
                    <label className="text-xs font-semibold text-[var(--color-text-muted)]">
                      Fournisseur
                      <select
                        value={provider.type}
                        onChange={(event) => updateAiProvider(provider.id, { type: event.target.value })}
                        className="mt-1 w-full rounded-md border border-[var(--color-border)] bg-[#161b22] px-3 py-2 text-sm text-white outline-none focus:border-[var(--color-primary)]"
                      >
                        <option value="google">Google Gemini</option>
                        <option value="openrouter">OpenRouter</option>
                        <option value="ollama-cloud">Ollama Cloud</option>
                        <option value="ollama-local">Ollama local</option>
                        <option value="custom">Compatible OpenAI</option>
                      </select>
                    </label>
                    <label className="text-xs font-semibold text-[var(--color-text-muted)]">
                      Nom
                      <input
                        value={provider.name}
                        onChange={(event) => updateAiProvider(provider.id, { name: event.target.value })}
                        className="mt-1 w-full rounded-md border border-[var(--color-border)] bg-[#161b22] px-3 py-2 text-sm text-white outline-none focus:border-[var(--color-primary)]"
                      />
                    </label>
                    <label className="text-xs font-semibold text-[var(--color-text-muted)]">
                      Modele
                      <input
                        value={provider.model}
                        onChange={(event) => updateAiProvider(provider.id, { model: event.target.value })}
                        className="mt-1 w-full rounded-md border border-[var(--color-border)] bg-[#161b22] px-3 py-2 text-sm text-white outline-none focus:border-[var(--color-primary)]"
                      />
                    </label>
                    <label className="text-xs font-semibold text-[var(--color-text-muted)]">
                      Cle API
                      <input
                        type="password"
                        value={provider.apiKey}
                        onChange={(event) => updateAiProvider(provider.id, { apiKey: event.target.value })}
                        placeholder={provider.type === "ollama-local" ? "Optionnel" : "sk-..."}
                        className="mt-1 w-full rounded-md border border-[var(--color-border)] bg-[#161b22] px-3 py-2 text-sm text-white outline-none focus:border-[var(--color-primary)]"
                      />
                    </label>
                    <label className="text-xs font-semibold text-[var(--color-text-muted)] md:col-span-2">
                      URL API
                      <input
                        value={provider.baseUrl ?? ""}
                        onChange={(event) => updateAiProvider(provider.id, { baseUrl: event.target.value })}
                        placeholder="Utilise l'URL par defaut si vide"
                        className="mt-1 w-full rounded-md border border-[var(--color-border)] bg-[#161b22] px-3 py-2 text-sm text-white outline-none focus:border-[var(--color-primary)]"
                      />
                    </label>
                  </div>
                </div>
              ))}
            </div>
          </div>
        </div>
      )}

      {(isLoading || error) && (
        <div className="fixed bottom-4 left-4 rounded-md border border-[var(--color-border)] bg-[#161b22] px-4 py-3 text-sm shadow-lg">
          {isLoading ? (
            <span className="inline-flex items-center gap-2 text-[var(--color-text-muted)]">
              <Loader2 size={16} className="animate-spin" />
              Chargement...
            </span>
          ) : (
            <span className="inline-flex items-center gap-2 text-[var(--color-error)]">
              <AlertTriangle size={16} />
              {error}
            </span>
          )}
        </div>
      )}
    </div>
  );
}
