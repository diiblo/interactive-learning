"use client";

import { CheckCircle2, Goal, ListChecks, Shield, Sparkles } from "lucide-react";
import type { IntermediateBossDetailDto } from "@/types/api";

export function IntermediateBossContent({ boss }: { boss: IntermediateBossDetailDto | null }) {
  if (!boss) {
    return null;
  }

  return (
    <section className="h-full overflow-y-auto p-6">
      <div className="mb-6">
        <p className="inline-flex items-center gap-2 text-xs uppercase tracking-wide text-[var(--color-error)]">
          <Shield size={14} />
          Monstre intermediaire
        </p>
        <h2 className="mt-1 text-2xl font-bold text-white">{boss.title}</h2>
        <p className="mt-2 text-sm text-[var(--color-text-muted)]">{boss.xpReward} XP a gagner</p>
      </div>

      <div className="space-y-5">
        <InfoBlock icon={<Goal size={18} />} title="Objectif" content={boss.objective} />
        <InfoBlock icon={<Sparkles size={18} />} title="Instructions" content={boss.instructions} />
        <InfoBlock icon={<CheckCircle2 size={18} />} title="Resultat attendu" content={boss.expectedResult} />

        <div className="rounded-md border border-[var(--color-border)] bg-[#161b22] p-4">
          <div className="mb-3 flex items-center gap-2 text-sm font-semibold text-white">
            <span className="text-[var(--color-primary)]">
              <ListChecks size={18} />
            </span>
            Criteres de validation
          </div>
          <ul className="space-y-2 text-sm text-[var(--color-text-muted)]">
            {boss.validationRules.map((rule) => (
              <li key={rule} className="flex gap-2">
                <span className="mt-2 size-1.5 shrink-0 rounded-full bg-[var(--color-primary)]" />
                <span>{rule}</span>
              </li>
            ))}
          </ul>
        </div>
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
      <p className="whitespace-pre-line text-sm leading-6 text-[var(--color-text-muted)]">{content}</p>
    </div>
  );
}
