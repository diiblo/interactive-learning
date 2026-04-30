"use client";

import { Unlock } from "lucide-react";

export function LessonUnlockCard({ unlockedCount }: { unlockedCount: number }) {
  if (unlockedCount <= 0) {
    return null;
  }

  return (
    <div className="flex items-center gap-2 rounded-md border border-[var(--color-primary)]/50 bg-[#0d1117] px-3 py-2 text-sm text-white">
      <Unlock size={16} className="text-[var(--color-primary)]" />
      {unlockedCount} nouveau contenu debloque
    </div>
  );
}
