"use client";

import { useState } from "react";
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

  const resultKey = `${result.passed}-${result.feedback}-${result.testResults.map((test) => `${test.name}:${test.passed}`).join("|")}`;

  return <FeedbackPanelResult key={resultKey} result={result} />;
}

function FeedbackPanelResult({ result }: { result: SubmitResultDto }) {
  const [revealedHints, setRevealedHints] = useState(1);

  const structured = result.structuredFeedback;
  const hints = structured?.progressiveHints ?? [];
  const visibleHints = hints.slice(0, revealedHints);

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
      <p className="mb-4 text-sm leading-6 text-[var(--color-text-muted)]">{structured?.summary ?? result.feedback}</p>

      {result.xpEarned > 0 && (
        <div className="mb-4 flex items-center gap-2 rounded-md border border-[var(--color-success)]/40 bg-[#0d1117] px-3 py-2 text-sm text-white">
          <Sparkles size={16} className="text-[var(--color-success)]" />
          +{result.xpEarned} XP
        </div>
      )}

      {structured ? (
        <div className="mb-4 space-y-3">
          {structured.whatWentWell.length > 0 ? (
            <FeedbackList title="Reussi" items={structured.whatWentWell} success />
          ) : null}
          {structured.whatIsMissing.length > 0 ? (
            <FeedbackList title="A corriger" items={structured.whatIsMissing} />
          ) : null}
          {visibleHints.length > 0 ? (
            <div className="rounded-md border border-[var(--color-border)] bg-[#0d1117] p-3">
              <p className="mb-2 text-sm font-semibold text-white">Indices progressifs</p>
              <div className="space-y-2">
                {visibleHints.map((hint, index) => (
                  <p key={`${hint}-${index}`} className="text-xs leading-5 text-[var(--color-text-muted)]">
                    <span className="font-semibold text-[var(--color-primary)]">Indice {index + 1}.</span> {hint}
                  </p>
                ))}
              </div>
              {revealedHints < hints.length ? (
                <button
                  type="button"
                  onClick={() => setRevealedHints((value) => value + 1)}
                  className="mt-3 rounded-md border border-[var(--color-border)] px-2 py-1 text-xs font-semibold text-white hover:border-[var(--color-primary)]"
                >
                  Reveler indice {revealedHints + 1}
                </button>
              ) : null}
            </div>
          ) : null}
          {structured.relatedSkills.length > 0 ? (
            <div className="rounded-md border border-[var(--color-border)] bg-[#0d1117] p-3">
              <p className="mb-2 text-sm font-semibold text-white">Competences concernees</p>
              <div className="flex flex-wrap gap-2">
                {structured.relatedSkills.map((skill) => (
                  <span key={skill.slug} className="rounded bg-[#1e2329] px-2 py-1 text-xs text-[var(--color-text-muted)]">
                    {skill.name} - {skill.masteryPercent}%
                  </span>
                ))}
              </div>
            </div>
          ) : null}
        </div>
      ) : null}

      {result.bossResult ? (
        <div className="mb-4 rounded-md border border-[var(--color-border)] bg-[#0d1117] p-3">
          <p className="text-sm font-semibold text-white">Rapport du boss: {result.bossResult.scorePercent}%</p>
          <p className="mt-1 text-xs leading-5 text-[var(--color-text-muted)]">{result.bossResult.summary}</p>
          <div className="mt-3 space-y-2">
            {result.bossResult.skillResults.map((skill) => (
              <div key={skill.skillSlug}>
                <div className="flex justify-between text-xs text-white">
                  <span>{skill.skillName}</span>
                  <span>{skill.scorePercent}%</span>
                </div>
                <div className="mt-1 h-2 overflow-hidden rounded bg-[#1e2329]">
                  <div className="h-full bg-[var(--color-primary)]" style={{ width: `${skill.scorePercent}%` }} />
                </div>
              </div>
            ))}
          </div>
        </div>
      ) : null}

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

function FeedbackList({ title, items, success = false }: { title: string; items: string[]; success?: boolean }) {
  return (
    <div className="rounded-md border border-[var(--color-border)] bg-[#0d1117] p-3">
      <p className={`mb-2 text-sm font-semibold ${success ? "text-[var(--color-success)]" : "text-white"}`}>{title}</p>
      <ul className="space-y-1 text-xs leading-5 text-[var(--color-text-muted)]">
        {items.map((item) => (
          <li key={item}>{item}</li>
        ))}
      </ul>
    </div>
  );
}
