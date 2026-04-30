"use client";

import { useCallback, useEffect, useMemo, useRef, useState } from "react";
import { Play, Send, Loader2, AlertTriangle } from "lucide-react";
import { apiClient } from "@/lib/api-client";
import { findFirstAvailableLesson, findLessonInMap } from "@/lib/lesson-state";
import type {
  CourseMapDto,
  CourseSummaryDto,
  ExecutionResultDto,
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
    if (!lesson) {
      return;
    }

    setIsRunning(true);
    setOutput("Execution en cours...\n");
    setDiagnostics([]);
    setError(null);

    try {
      const result: ExecutionResultDto = lesson.editorLanguage === "sql"
        ? await apiClient.runSqlLesson(lesson.id, code)
        : lesson.isBossFinal
          ? await apiClient.runBossFinal(code)
          : await apiClient.runLesson(lesson.id, code);
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
    if (!lesson) {
      return;
    }

    setIsRunning(true);
    setOutput("Correction en cours...\n");
    setDiagnostics([]);
    setError(null);

    try {
      const result = lesson.editorLanguage === "sql"
        ? await apiClient.submitSqlLesson(lesson.id, code)
        : lesson.isBossFinal
          ? await apiClient.submitBossFinal(code)
          : await apiClient.submitLesson(lesson.id, code);
      setLastSubmit(result);
      setOutput(result.output);
      setSqlColumns([]);
      setSqlRows([]);
      await refreshProgress();
      if (selectedCourseId) {
        setCourseMap(await apiClient.getCourseMap(selectedCourseId));
      }
    } catch (caught) {
      setError(caught instanceof Error ? caught.message : "Erreur pendant la correction.");
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
        <CourseSidebar courseMap={courseMap} activeLessonId={lesson?.id ?? null} onSelectLesson={loadLesson} />

        <section className="grid min-h-0 flex-1 grid-rows-[minmax(220px,36%)_1fr] md:grid-cols-[minmax(280px,36%)_1fr_280px] md:grid-rows-1">
          <div className="min-h-0 border-b border-[var(--color-border)] bg-[var(--color-surface)] md:border-b-0 md:border-r">
            <LessonContent lesson={lesson} />
          </div>

          <div className="flex min-h-0 flex-col bg-[#0d1117]">
            {lesson?.isBossFinal && <BossFinalWorkspace />}
            <div className="flex items-center justify-between border-b border-[var(--color-border)] bg-[#161b22] px-4 py-3">
              <div>
                <p className="text-sm font-semibold text-white">{lesson?.editorLanguage === "sql" ? "query.sql" : "Program.cs"}</p>
                <p className="text-xs text-[var(--color-text-muted)]">{activeMapItem?.status ?? "Locked"}</p>
              </div>
              <div className="flex items-center gap-2">
                <button
                  type="button"
                  onClick={runCode}
                  disabled={!lesson || isRunning}
                  className="inline-flex items-center gap-2 rounded-md border border-[var(--color-border)] bg-[#1e2329] px-3 py-2 text-sm font-semibold text-white transition hover:border-[var(--color-primary)] disabled:cursor-not-allowed disabled:opacity-50"
                >
                  {isRunning ? <Loader2 size={16} className="animate-spin" /> : <Play size={16} />}
                  Executer
                </button>
                <button
                  type="button"
                  onClick={submitCode}
                  disabled={!lesson || isRunning}
                  className="inline-flex items-center gap-2 rounded-md bg-[var(--color-success)] px-3 py-2 text-sm font-semibold text-white transition hover:bg-[#3fb950] disabled:cursor-not-allowed disabled:opacity-50"
                >
                  <Send size={16} />
                  Soumettre
                </button>
              </div>
            </div>

            {lesson?.editorLanguage === "sql" ? <SqlSafetyNotice schema={sqlSchema} /> : null}

            <div className="min-h-0 flex-1">
              <CodeEditor code={code} language={lesson?.editorLanguage ?? "csharp"} onChange={setCode} />
            </div>

            {lesson?.editorLanguage === "sql" ? <SqlResultGrid columns={sqlColumns} rows={sqlRows} /> : null}

            <div className="h-48 border-t border-[var(--color-border)]">
              <RunConsole output={output} diagnostics={diagnostics} onClear={() => { setOutput(""); setDiagnostics([]); }} />
            </div>
          </div>

          <div className="hidden min-h-0 flex-col md:flex">
            <FeedbackPanel result={lastSubmit} />
            {lesson?.editorLanguage === "sql" ? <SqlSchemaPanel schema={sqlSchema} /> : null}
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
