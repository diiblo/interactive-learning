"use client";

import { Swords } from "lucide-react";

export function BossFinalWorkspace() {
  return (
    <div className="flex items-center gap-2 border-b border-[var(--color-border)] bg-[#260f16] px-4 py-2 text-sm font-semibold text-white">
      <Swords size={16} className="text-[var(--color-error)]" />
      Boss Final
    </div>
  );
}
