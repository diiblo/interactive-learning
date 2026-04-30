"use client";

import { Terminal, Trash2 } from "lucide-react";

type RunConsoleProps = {
  output: string;
  diagnostics: string[];
  onClear: () => void;
};

export function RunConsole({ output, diagnostics, onClear }: RunConsoleProps) {
  const hasErrors = diagnostics.length > 0;

  return (
    <section className="flex h-full flex-col bg-[#010409]">
      <div className="flex items-center justify-between border-b border-[var(--color-border)] bg-[#161b22] px-4 py-2">
        <div className="flex items-center gap-2 text-xs font-semibold uppercase tracking-wide text-[var(--color-text-muted)]">
          <Terminal size={16} />
          Sortie
        </div>
        <button
          type="button"
          onClick={onClear}
          className="rounded p-1 text-[var(--color-text-muted)] transition hover:bg-[#1e2329] hover:text-white"
          aria-label="Effacer la sortie"
        >
          <Trash2 size={15} />
        </button>
      </div>
      <div className="flex-1 overflow-y-auto p-4 font-mono text-sm leading-6">
        {output || hasErrors ? (
          <pre className={`whitespace-pre-wrap ${hasErrors ? "text-[var(--color-error)]" : "text-[var(--color-success)]"}`}>
            {hasErrors ? diagnostics.join("\n") : output || "Programme termine sans sortie console."}
          </pre>
        ) : (
          <div className="flex h-full items-center justify-center text-[var(--color-text-muted)]">La sortie du programme apparaitra ici.</div>
        )}
      </div>
    </section>
  );
}
