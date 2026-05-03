"use client";

import { BookOpen, RotateCcw } from "lucide-react";
import type { SkillProgressDto } from "@/types/api";

type SkillMapProps = {
  skills: SkillProgressDto[];
  onReview: (lessonSlug: string | null, skillId: number) => void;
};

export function SkillMap({ skills, onReview }: SkillMapProps) {
  if (skills.length === 0) {
    return (
      <aside className="border-l border-t border-[var(--color-border)] bg-[var(--color-surface)] p-4 text-sm text-[var(--color-text-muted)]">
        La carte de competences apparaitra apres le chargement du parcours.
      </aside>
    );
  }

  const due = skills.filter((skill) => skill.isReviewDue);

  return (
    <aside className="space-y-4 border-l border-t border-[var(--color-border)] bg-[var(--color-surface)] p-4">
      <section>
        <div className="mb-3 flex items-center gap-2 text-sm font-bold text-white">
          <BookOpen size={16} className="text-[var(--color-primary)]" />
          Competences
        </div>
        <div className="space-y-3">
          {skills.slice(0, 8).map((skill) => (
            <SkillProgressCard key={skill.skillId} skill={skill} onReview={onReview} />
          ))}
        </div>
      </section>

      <section>
        <div className="mb-2 flex items-center gap-2 text-sm font-bold text-white">
          <RotateCcw size={16} className="text-[var(--color-primary)]" />
          A reviser
        </div>
        {due.length === 0 ? (
          <p className="text-xs leading-5 text-[var(--color-text-muted)]">Aucune revision due pour le moment.</p>
        ) : (
          <div className="space-y-2">
            {due.slice(0, 4).map((skill) => (
              <button
                key={skill.skillId}
                type="button"
                onClick={() => onReview(skill.reviewLessonSlug, skill.skillId)}
                className="w-full rounded-md border border-[var(--color-border)] bg-[#0d1117] px-3 py-2 text-left text-xs text-white transition hover:border-[var(--color-primary)]"
              >
                {skill.skillName}
              </button>
            ))}
          </div>
        )}
      </section>
    </aside>
  );
}

function SkillProgressCard({ skill, onReview }: { skill: SkillProgressDto; onReview: (lessonSlug: string | null, skillId: number) => void }) {
  return (
    <div className="rounded-md border border-[var(--color-border)] bg-[#0d1117] p-3">
      <div className="mb-2 flex items-start justify-between gap-2">
        <div>
          <p className="text-sm font-semibold text-white">{skill.skillName}</p>
          <p className="text-xs text-[var(--color-text-muted)]">{skill.successfulAttempts} reussites / {skill.failedAttempts} echecs</p>
        </div>
        <span className={`rounded px-2 py-1 text-[11px] font-semibold ${skill.isReviewDue ? "bg-[#3b2600] text-[#f2cc60]" : "bg-[#1e2329] text-[var(--color-text-muted)]"}`}>
          {skill.status}
        </span>
      </div>
      <div className="h-2 overflow-hidden rounded bg-[#1e2329]">
        <div className="h-full bg-[var(--color-primary)]" style={{ width: `${skill.masteryPercent}%` }} />
      </div>
      <div className="mt-2 flex items-center justify-between">
        <span className="text-xs text-[var(--color-text-muted)]">{skill.masteryPercent}%</span>
        <button
          type="button"
          onClick={() => onReview(skill.reviewLessonSlug, skill.skillId)}
          className="text-xs font-semibold text-[var(--color-primary)] hover:text-white"
        >
          Reviser
        </button>
      </div>
    </div>
  );
}
