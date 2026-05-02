"use client";

import { useCallback, useEffect, useMemo, useRef, useState } from "react";
import { Play, Send, Loader2, AlertTriangle } from "lucide-react";
import { apiClient } from "@/lib/api-client";
import { findFirstAvailableLesson, findLessonInMap } from "@/lib/lesson-state";
import type {
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
import { SqlResultGrid } from "./sql-result-grid";
import { SqlSafetyNotice } from "./sql-safety-notice";
import { SqlSchemaPanel } from "./sql-schema-panel";
import { XpLevelCard } from "./xp-level-card";

type AppShellProps = {
  initialLessonId?: number;
  initialBossFinal?: boolean;
};

export function AppShell({ initialLessonId, initialBossFinal = false }: AppShellProps) {
  const [courses, setCourses] = useState<CourseSummaryDto[]>([]);
  const [selectedCourseId, setSelectedCourseId] = useState<number | null>(null);
  const [courseMap, setCourseMap] = useState<CourseMapDto | null>(null);
  const [profile, setProfile] = useState<ProfileDto | null>(null);
  const [progress, setProgress] = useState<ProgressDto | null>(null);
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
    setCode(detail.starterCode);

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
    setCode(detail.starterCode);

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
  }, [loadLesson]);

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
      const result = intermediateBoss
        ? await apiClient.submitIntermediateBoss(intermediateBoss.id, code)
        : lesson!.editorLanguage === "sql"
          ? await apiClient.submitSqlLesson(lesson!.id, code)
          : lesson!.isBossFinal && lesson!.editorLanguage === "csharp"
            ? await apiClient.submitBossFinal(code)
            : await apiClient.submitLesson(lesson!.id, code);
      setLastSubmit(result);
      setOutput(result.output);
      setSqlColumns([]);
      setSqlRows([]);
      await refreshProgress();
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

        <section className="grid min-h-0 flex-1 grid-rows-[minmax(220px,36%)_1fr] md:grid-cols-[minmax(280px,36%)_1fr_280px] md:grid-rows-1">
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

          <div className="hidden min-h-0 flex-col md:flex">
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
            <div className="space-y-3 border-l border-t border-[var(--color-border)] bg-[var(--color-surface)] p-4">
              <LessonUnlockCard unlockedCount={lastSubmit?.unlockedLessonIds.length ?? 0} />
              <XpLevelCard profile={profile} />
              <BadgeGrid badges={profile?.badges ?? []} />
            </div>
          </div>
        </section>
      </main>

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
