"use client";

import { BookOpen, CheckCircle2, Goal, Lightbulb, TriangleAlert } from "lucide-react";
import type { LessonDetailDto } from "@/types/api";

export function LessonContent({ lesson }: { lesson: LessonDetailDto | null }) {
  if (!lesson) {
    return (
      <section className="flex h-full items-center justify-center p-8 text-[var(--color-text-muted)]">
        Selectionne une lecon disponible pour commencer.
      </section>
    );
  }

  return (
    <section className="h-full overflow-y-auto p-6">
      <div className="mb-6">
        <p className="text-xs uppercase tracking-wide text-[var(--color-primary)]">{lesson.isBossFinal ? "Boss Final" : "Lecon"}</p>
        <h2 className="mt-1 text-2xl font-bold text-white">{lesson.title}</h2>
        <p className="mt-2 text-sm text-[var(--color-text-muted)]">{lesson.xpReward} XP a gagner</p>
      </div>

      <div className="space-y-5">
        <InfoBlock icon={<Goal size={18} />} title="Objectif" content={lesson.objective} />
        {lesson.conceptSummary ? <InfoBlock icon={<CheckCircle2 size={18} />} title="A retenir" content={lesson.conceptSummary} /> : null}
        {lesson.commonMistakes ? <InfoBlock icon={<TriangleAlert size={18} />} title="Erreurs frequentes" content={lesson.commonMistakes} /> : null}
        <InfoBlock icon={<BookOpen size={18} />} title="Explication" content={lesson.explanation} />

        <div className="rounded-md border border-[var(--color-border)] bg-[#0d1117]">
          <div className="border-b border-[var(--color-border)] px-4 py-2 text-sm font-semibold text-white">Exemple</div>
          <pre className="overflow-x-auto p-4 text-sm text-[#d2a8ff]">
            <code>{lesson.exampleCode}</code>
          </pre>
        </div>

        <InfoBlock icon={<Lightbulb size={18} />} title="Exercice" content={lesson.exercisePrompt} />

        {lesson.finalCorrection ? (
          <div className="rounded-md border border-[var(--color-border)] bg-[#0d1117]">
            <div className="border-b border-[var(--color-border)] px-4 py-2 text-sm font-semibold text-white">Correction finale</div>
            <pre className="overflow-x-auto p-4 text-sm text-[#a5d6ff]">
              <code>{lesson.finalCorrection}</code>
            </pre>
          </div>
        ) : null}
      </div>
    </section>
  );
}

function InfoBlock({ icon, title, content }: { icon: React.ReactNode; title: string; content: string }) {
  return (
    <div className="rounded-md border border-[var(--color-border)] bg-[#161b22] p-4">
      <div className="mb-2 flex items-center gap-2 text-sm font-semibold text-white">
        <span className="text-[var(--color-primary)]">{icon}</span>
        {title}
      </div>
      <p className="text-sm leading-6 text-[var(--color-text-muted)]">{content}</p>
    </div>
  );
}
