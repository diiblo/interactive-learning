"use client";

import { Database } from "lucide-react";
import type { SqlSchemaDto } from "@/types/api";

type SqlSchemaPanelProps = {
  schema: SqlSchemaDto | null;
};

export function SqlSchemaPanel({ schema }: SqlSchemaPanelProps) {
  if (!schema) {
    return null;
  }

  return (
    <section className="border-l border-t border-[var(--color-border)] bg-[var(--color-surface)]">
      <div className="flex items-center gap-2 border-b border-[var(--color-border)] px-4 py-3 text-sm font-semibold text-white">
        <Database size={16} className="text-[var(--color-primary)]" />
        Schema {schema.scenario}
      </div>
      <div className="max-h-80 space-y-3 overflow-y-auto p-4">
        {schema.tables.map((table) => (
          <div key={table.name} className="rounded-md border border-[var(--color-border)] bg-[#0d1117]">
            <div className="border-b border-[var(--color-border)] px-3 py-2">
              <p className="text-sm font-semibold text-white">{table.name}</p>
              <p className="mt-1 text-xs leading-5 text-[var(--color-text-muted)]">{table.description}</p>
            </div>
            <div className="divide-y divide-[var(--color-border)]">
              {table.columns.map((column) => (
                <div key={`${table.name}-${column.name}`} className="px-3 py-2">
                  <div className="flex items-center justify-between gap-3">
                    <span className="font-mono text-xs font-semibold text-[#a5d6ff]">{column.name}</span>
                    <span className="shrink-0 font-mono text-[11px] text-[var(--color-text-muted)]">{column.type}</span>
                  </div>
                  {column.description ? <p className="mt-1 text-xs leading-5 text-[var(--color-text-muted)]">{column.description}</p> : null}
                </div>
              ))}
            </div>
          </div>
        ))}
      </div>
    </section>
  );
}
