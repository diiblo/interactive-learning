import type { CourseMapDto, LessonMapItemDto } from "@/types/api";

export function findFirstAvailableLesson(courseMap: CourseMapDto | null): LessonMapItemDto | null {
  if (!courseMap) {
    return null;
  }

  for (const chapter of courseMap.chapters) {
    const lesson = chapter.lessons.find((item) => !item.isLocked);
    if (lesson) {
      return lesson;
    }
  }

  return courseMap.bossFinal && !courseMap.bossFinal.isLocked ? courseMap.bossFinal : null;
}

export function findLessonInMap(courseMap: CourseMapDto | null, lessonId: number): LessonMapItemDto | null {
  if (!courseMap) {
    return null;
  }

  for (const chapter of courseMap.chapters) {
    const lesson = chapter.lessons.find((item) => item.id === lessonId);
    if (lesson) {
      return lesson;
    }
  }

  return courseMap.bossFinal?.id === lessonId ? courseMap.bossFinal : null;
}
