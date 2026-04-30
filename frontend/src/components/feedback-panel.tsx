"use client";

import { CheckCircle, CircleX, Sparkles } from "lucide-react";
import type { SubmitResultDto } from "@/types/api";

export function FeedbackPanel({ result }: { result: SubmitResultDto | null }) {
  if (!result) {
    return (
      <aside className="border-l border-[var(--color-border)] bg-[var(--color-surface)] p-4 text-sm text-[var(--color-text-muted)]">
        Soumets ton code pour recevoir une correction automatique.
      </aside>
    );
  }

  return (
    <aside className="overflow-y-auto border-l border-[var(--color-border)] bg-[var(--color-surface)] p-4">
      <div className="mb-4 flex items-center gap-2">
        {result.passed ? (
          <CheckCircle className="text-[var(--color-success)]" size={20} />
        ) : (
          <CircleX className="text-[var(--color-error)]" size={20} />
        )}
        <h3 className="font-bold text-white">{result.passed ? "Mission validee" : "Correction a revoir"}</h3>
      </div>
      <p className="mb-4 text-sm leading-6 text-[var(--color-text-muted)]">{result.feedback}</p>

      {result.xpEarned > 0 && (
        <div className="mb-4 flex items-center gap-2 rounded-md border border-[var(--color-success)]/40 bg-[#0d1117] px-3 py-2 text-sm text-white">
          <Sparkles size={16} className="text-[var(--color-success)]" />
          +{result.xpEarned} XP
        </div>
      )}

      <div className="space-y-2">
        {result.testResults.map((test) => (
          <div key={test.name} className="rounded-md border border-[var(--color-border)] bg-[#0d1117] p-3">
            <div className="flex items-center gap-2 text-sm font-semibold text-white">
              {test.passed ? (
                <CheckCircle size={15} className="text-[var(--color-success)]" />
              ) : (
                <CircleX size={15} className="text-[var(--color-error)]" />
              )}
              {test.name}
            </div>
            <p className="mt-1 text-xs text-[var(--color-text-muted)]">{test.message}</p>
          </div>
        ))}
      </div>
    </aside>
  );
}
