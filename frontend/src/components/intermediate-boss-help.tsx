"use client";

import { Eye, Lightbulb, Lock, ShieldCheck } from "lucide-react";
import type { IntermediateBossDetailDto, IntermediateBossHintDto, SubmitResultDto } from "@/types/api";

type IntermediateBossHelpProps = {
  boss: IntermediateBossDetailDto | null;
  result: SubmitResultDto | null;
  hints: IntermediateBossHintDto[];
  solution: string | null;
  isBusy: boolean;
  onRevealHint: () => void;
  onRevealSolution: () => void;
};

export function IntermediateBossHelp({
  boss,
  result,
  hints,
  solution,
  isBusy,
  onRevealHint,
  onRevealSolution,
}: IntermediateBossHelpProps) {
  if (!boss) {
    return null;
  }

  const canRevealSolution = boss.canRevealSolution || (result?.passed === false);
  const defeated = result?.passed || boss.status === "Completed";

  return (
    <div className="space-y-3 border-l border-t border-[var(--color-border)] bg-[var(--color-surface)] p-4">
      {defeated ? (
        <div className="rounded-md border border-[var(--color-success)]/50 bg-[#102018] p-3 text-sm text-white">
          <div className="mb-1 flex items-center gap-2 font-semibold text-[var(--color-success)]">
            <ShieldCheck size={16} />
            Monstre vaincu
          </div>
          Le module suivant peut maintenant se deverrouiller.
        </div>
      ) : null}

      <div className="flex flex-col gap-2">
        <button
          type="button"
          onClick={onRevealHint}
          disabled={isBusy}
          className="inline-flex items-center justify-center gap-2 rounded-md border border-[var(--color-border)] bg-[#1e2329] px-3 py-2 text-sm font-semibold text-white transition hover:border-[var(--color-primary)] disabled:cursor-not-allowed disabled:opacity-50"
        >
          <Lightbulb size={16} />
          Voir indice
        </button>
        <button
          type="button"
          onClick={onRevealSolution}
          disabled={isBusy || !canRevealSolution}
          className="inline-flex items-center justify-center gap-2 rounded-md border border-[var(--color-border)] bg-[#1e2329] px-3 py-2 text-sm font-semibold text-white transition hover:border-[var(--color-error)] disabled:cursor-not-allowed disabled:opacity-50"
        >
          {canRevealSolution ? <Eye size={16} /> : <Lock size={16} />}
          Voir solution
        </button>
        {!canRevealSolution ? (
          <p className="text-xs text-[var(--color-text-muted)]">Disponible apres au moins une tentative echouee.</p>
        ) : null}
      </div>

      {hints.length > 0 ? (
        <div className="rounded-md border border-[var(--color-border)] bg-[#0d1117] p-3">
          <p className="mb-2 text-sm font-semibold text-white">Indices</p>
          <div className="space-y-2">
            {hints.map((hint) => (
              <p key={hint.index} className="text-sm leading-6 text-[var(--color-text-muted)]">
                <span className="font-semibold text-[var(--color-primary)]">Indice {hint.index}.</span> {hint.content}
              </p>
            ))}
          </div>
        </div>
      ) : null}

      {solution ? (
        <div className="rounded-md border border-[var(--color-error)]/50 bg-[#0d1117]">
          <div className="border-b border-[var(--color-border)] px-3 py-2 text-sm font-semibold text-white">Solution revelee</div>
          <pre className="overflow-x-auto p-3 text-xs leading-5 text-[#a5d6ff]">
            <code>{solution}</code>
          </pre>
        </div>
      ) : null}
    </div>
  );
}
