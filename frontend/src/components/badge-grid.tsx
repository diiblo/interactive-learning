"use client";

import { Award, Lock } from "lucide-react";
import type { BadgeDto } from "@/types/api";

export function BadgeGrid({ badges }: { badges: BadgeDto[] }) {
  return (
    <div className="grid grid-cols-1 gap-2">
      {badges.map((badge) => {
        const earned = Boolean(badge.earnedAt);
        return (
          <div key={badge.slug} className="flex items-center gap-3 rounded-md border border-[var(--color-border)] bg-[#0d1117] p-3">
            {earned ? <Award size={16} className="text-[var(--color-success)]" /> : <Lock size={16} className="text-[var(--color-text-muted)]" />}
            <div className="min-w-0">
              <p className="truncate text-sm font-semibold text-white">{badge.name}</p>
              <p className="truncate text-xs text-[var(--color-text-muted)]">{badge.description}</p>
            </div>
          </div>
        );
      })}
    </div>
  );
}
