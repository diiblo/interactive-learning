"use client";

import { CheckCircle, Lock, Swords } from "lucide-react";
import type { CourseMapDto, LessonMapItemDto } from "@/types/api";

type CourseSidebarProps = {
  courseMap: CourseMapDto | null;
  activeLessonId: number | null;
  onSelectLesson: (lesson: LessonMapItemDto) => void;
};

export function CourseSidebar({ courseMap, activeLessonId, onSelectLesson }: CourseSidebarProps) {
  return (
    <aside className="flex w-full flex-col border-r border-[var(--color-border)] bg-[var(--color-surface)] md:w-80">
      <div className="border-b border-[var(--color-border)] px-5 py-4">
        <p className="text-xs uppercase tracking-wide text-[var(--color-text-muted)]">Cours</p>
        <h2 className="mt-1 text-lg font-bold text-white">{courseMap?.title ?? "Chargement..."}</h2>
      </div>
      <div className="flex-1 overflow-y-auto p-4">
        {courseMap?.chapters.map((chapter) => (
          <section key={chapter.id} className="mb-6">
            <div className="mb-2">
              <h3 className="text-sm font-semibold text-white">{chapter.title}</h3>
              <p className="text-xs text-[var(--color-text-muted)]">{chapter.description}</p>
            </div>
            <div className="space-y-2">
              {chapter.lessons.map((lesson) => (
                <LessonButton
                  key={lesson.id}
                  lesson={lesson}
                  isActive={lesson.id === activeLessonId}
                  onSelectLesson={onSelectLesson}
                />
              ))}
            </div>
          </section>
        ))}

        {courseMap?.bossFinal && (
          <section>
            <div className="mb-2 flex items-center gap-2 text-sm font-semibold text-white">
              <Swords size={16} className="text-[var(--color-error)]" />
              Boss Final
            </div>
            <LessonButton lesson={courseMap.bossFinal} isActive={courseMap.bossFinal.id === activeLessonId} onSelectLesson={onSelectLesson} />
          </section>
        )}
      </div>
    </aside>
  );
}

function LessonButton({
  lesson,
  isActive,
  onSelectLesson,
}: {
  lesson: LessonMapItemDto;
  isActive: boolean;
  onSelectLesson: (lesson: LessonMapItemDto) => void;
}) {
  const isCompleted = lesson.status === "Completed";

  return (
    <button
      type="button"
      disabled={lesson.isLocked}
      onClick={() => onSelectLesson(lesson)}
      className={`flex w-full items-center gap-3 rounded-md border px-3 py-3 text-left transition ${
        isActive
          ? "border-[var(--color-primary)] bg-[#1f2937] text-white"
          : "border-[var(--color-border)] bg-[#0d1117] text-[var(--color-text)] hover:border-[var(--color-primary)]/60"
      } disabled:cursor-not-allowed disabled:opacity-45`}
    >
      {lesson.isLocked ? (
        <Lock size={16} className="shrink-0 text-[var(--color-text-muted)]" />
      ) : isCompleted ? (
        <CheckCircle size={16} className="shrink-0 text-[var(--color-success)]" />
      ) : (
        <span className="size-2 shrink-0 rounded-full bg-[var(--color-primary)]" />
      )}
      <span className="min-w-0 flex-1">
        <span className="block truncate text-sm font-medium">{lesson.title}</span>
        <span className="text-xs text-[var(--color-text-muted)]">{lesson.xpReward} XP</span>
      </span>
    </button>
  );
}
