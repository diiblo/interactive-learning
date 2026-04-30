"use client";

import { Award, Gauge, Sparkles } from "lucide-react";
import type { ProfileDto, ProgressDto } from "@/types/api";

type ProgressHeaderProps = {
  profile: ProfileDto | null;
  progress: ProgressDto | null;
};

export function ProgressHeader({ profile, progress }: ProgressHeaderProps) {
  const completed = progress?.completedLessons ?? 0;
  const total = progress?.totalLessons ?? 0;
  const percent = total > 0 ? Math.round((completed / total) * 100) : 0;
  const xp = profile?.totalXp ?? 0;
  const level = profile?.level ?? 1;
  const nextLevelXp = level * 100;
  const currentLevelStart = (level - 1) * 100;
  const levelPercent = Math.min(100, Math.round(((xp - currentLevelStart) / 100) * 100));

  return (
    <header className="flex min-h-16 items-center justify-between border-b border-[var(--color-border)] bg-[var(--color-surface)] px-5">
      <div className="flex items-center gap-3">
        <div className="flex size-10 items-center justify-center rounded-md border border-[var(--color-border)] bg-[#1e2329]">
          <Sparkles className="text-[var(--color-primary)]" size={20} />
        </div>
        <div>
          <h1 className="text-base font-bold text-white">interactive-learning</h1>
          <p className="text-xs text-[var(--color-text-muted)]">Parcours progressif et gamifie</p>
        </div>
      </div>

      <div className="flex items-center gap-4 text-sm">
        <div className="hidden items-center gap-2 md:flex">
          <Gauge size={16} className="text-[var(--color-primary)]" />
          <span className="text-[var(--color-text-muted)]">Progression</span>
          <span className="font-semibold text-white">{percent}%</span>
        </div>
        <div className="h-2 w-28 rounded-full bg-[#0d1117]">
          <div className="h-full rounded-full bg-[var(--color-success)]" style={{ width: `${percent}%` }} />
        </div>
        <div className="flex items-center gap-2 rounded-md border border-[var(--color-border)] bg-[#1e2329] px-3 py-2">
          <Award size={16} className="text-[var(--color-primary)]" />
          <span className="font-semibold text-white">Niv. {level}</span>
          <span className="text-[var(--color-text-muted)]">{xp}/{nextLevelXp} XP</span>
          <span className="sr-only">{levelPercent}% du niveau courant</span>
        </div>
      </div>
    </header>
  );
}
