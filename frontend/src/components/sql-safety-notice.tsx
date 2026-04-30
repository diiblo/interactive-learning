"use client";

import { ShieldAlert } from "lucide-react";
import type { SqlSchemaDto } from "@/types/api";

type SqlSafetyNoticeProps = {
  schema: SqlSchemaDto | null;
};

export function SqlSafetyNotice({ schema }: SqlSafetyNoticeProps) {
  if (!schema) {
    return null;
  }

  return (
    <div className="border-b border-[var(--color-border)] bg-[#101820] px-4 py-2">
      <div className="flex items-start gap-2 text-xs leading-5 text-[var(--color-text-muted)]">
        <ShieldAlert size={15} className="mt-0.5 shrink-0 text-[var(--color-primary)]" />
        <span>{schema.safetyRules.join(" ")}</span>
      </div>
    </div>
  );
}
