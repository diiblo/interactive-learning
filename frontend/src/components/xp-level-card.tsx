"use client";

import { Star } from "lucide-react";
import type { ProfileDto } from "@/types/api";

export function XpLevelCard({ profile }: { profile: ProfileDto | null }) {
  const level = profile?.level ?? 1;
  const xp = profile?.totalXp ?? 0;
  const percent = Math.min(100, xp % 100);

  return (
    <div className="rounded-md border border-[var(--color-border)] bg-[#0d1117] p-4">
      <div className="mb-3 flex items-center gap-2 text-sm font-semibold text-white">
        <Star size={16} className="text-[var(--color-primary)]" />
        Niveau {level}
      </div>
      <div className="h-2 rounded-full bg-[#1e2329]">
        <div className="h-full rounded-full bg-[var(--color-primary)]" style={{ width: `${percent}%` }} />
      </div>
      <p className="mt-2 text-xs text-[var(--color-text-muted)]">{xp} XP total</p>
    </div>
  );
}
