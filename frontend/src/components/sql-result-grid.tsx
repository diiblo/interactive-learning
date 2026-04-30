"use client";

type SqlResultGridProps = {
  columns: string[];
  rows: Record<string, string | number | boolean | null>[];
};

export function SqlResultGrid({ columns, rows }: SqlResultGridProps) {
  if (columns.length === 0) {
    return null;
  }

  return (
    <section className="h-44 border-t border-[var(--color-border)] bg-[#010409]">
      <div className="border-b border-[var(--color-border)] bg-[#161b22] px-4 py-2 text-xs font-semibold uppercase tracking-wide text-[var(--color-text-muted)]">
        Resultat SQL
      </div>
      <div className="h-[calc(100%-33px)] overflow-auto">
        <table className="min-w-full border-separate border-spacing-0 text-left font-mono text-xs">
          <thead className="sticky top-0 bg-[#0d1117] text-[#a5d6ff]">
            <tr>
              {columns.map((column) => (
                <th key={column} className="border-b border-r border-[var(--color-border)] px-3 py-2 font-semibold">
                  {column}
                </th>
              ))}
            </tr>
          </thead>
          <tbody className="text-[var(--color-text)]">
            {rows.map((row, index) => (
              <tr key={index} className="odd:bg-[#0d1117] even:bg-[#111820]">
                {columns.map((column) => (
                  <td key={`${index}-${column}`} className="border-b border-r border-[var(--color-border)] px-3 py-2">
                    {String(row[column] ?? "NULL")}
                  </td>
                ))}
              </tr>
            ))}
            {rows.length === 0 ? (
              <tr>
                <td className="px-3 py-4 text-[var(--color-text-muted)]" colSpan={columns.length}>
                  Aucune ligne retournee.
                </td>
              </tr>
            ) : null}
          </tbody>
        </table>
      </div>
    </section>
  );
}
