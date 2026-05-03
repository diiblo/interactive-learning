"use client";

import type { ReviewItemDto } from "@/types/api";

type ReviewQueueProps = {
  reviews: ReviewItemDto[];
  onReview: (skillSlug: string, lessonSlug: string | null) => void;
};

export function ReviewQueue({ reviews, onReview }: ReviewQueueProps) {
  if (reviews.length === 0) {
    return (
      <aside className="border-l border-t border-[var(--color-border)] bg-[var(--color-surface)] p-4 text-sm text-[var(--color-text-muted)]">
        Aucune revision prevue pour le moment.
      </aside>
    );
  }

  return (
    <aside className="space-y-3 border-l border-t border-[var(--color-border)] bg-[var(--color-surface)] p-4">
      <p className="text-sm font-bold text-white">Revision a faire</p>
      <div className="space-y-3">
        {reviews.map((review) => (
          <div key={review.skillSlug} className="rounded-md border border-[var(--color-border)] bg-[#0d1117] p-3">
            <div className="flex items-center justify-between gap-2">
              <div>
                <p className="text-sm font-semibold text-white">{review.skillName}</p>
                <p className="text-xs text-[var(--color-text-muted)]">Maitrise {review.masteryPercent}%</p>
              </div>
              <button
                type="button"
                onClick={() => onReview(review.skillSlug, review.suggestedLessonSlugs[0] ?? null)}
                className="rounded-md border border-[var(--color-border)] px-2 py-1 text-xs font-semibold text-white hover:border-[var(--color-primary)]"
              >
                Reviser
              </button>
            </div>
            {review.suggestedLessonSlugs.length > 0 ? (
              <div className="mt-2 text-xs text-[var(--color-text-muted)]">
                Lecons suggerees: {review.suggestedLessonSlugs.join(", ")}
              </div>
            ) : null}
          </div>
        ))}
      </div>
    </aside>
  );
}
