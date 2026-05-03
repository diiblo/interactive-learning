import type {
  CourseMapDto,
  CourseSummaryDto,
  ExecutionResultDto,
  IntermediateBossDetailDto,
  IntermediateBossHintResultDto,
  IntermediateBossSolutionDto,
  LessonDetailDto,
  ProfileDto,
  ProgressDto,
  ReviewItemDto,
  SkillProgressDto,
  SqlSchemaDto,
  SubmitResultDto,
} from "@/types/api";

const API_BASE_URL = process.env.NEXT_PUBLIC_API_BASE_URL ?? "http://localhost:5000/api";

async function request<T>(path: string, init?: RequestInit): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    ...init,
    headers: {
      "Content-Type": "application/json",
      ...init?.headers,
    },
  });

  if (!response.ok) {
    throw new Error(`API error ${response.status} for ${path}`);
  }

  if (response.status === 204) {
    return undefined as T;
  }

  return response.json() as Promise<T>;
}

export const apiClient = {
  getProfile: () => request<ProfileDto>("/profile"),
  getProgress: () => request<ProgressDto>("/progress"),
  getSkillProgress: (courseLanguage?: string) =>
    request<SkillProgressDto[]>(courseLanguage ? `/skills/progress/${courseLanguage}` : "/skills/progress"),
  getDueReviews: () => request<ReviewItemDto[]>("/reviews/due"),
  completeReview: (skillId: number) =>
    request<void>("/progress/review-completed", {
      method: "POST",
      body: JSON.stringify({ skillId }),
    }),
  getCourses: () => request<CourseSummaryDto[]>("/courses"),
  getCourseMap: (courseId: number) => request<CourseMapDto>(`/courses/${courseId}/map`),
  getLesson: (lessonId: number) => request<LessonDetailDto>(`/lessons/${lessonId}`),
  getIntermediateBoss: (moduleId: number) => request<IntermediateBossDetailDto>(`/intermediate-bosses/modules/${moduleId}`),
  getSqlSchema: () => request<SqlSchemaDto>("/sql/lessons/schema"),
  getSqlLesson: (lessonId: number) => request<LessonDetailDto>(`/sql/lessons/${lessonId}`),
  runLesson: (lessonId: number, code: string) =>
    request<ExecutionResultDto>(`/lessons/${lessonId}/run`, {
      method: "POST",
      body: JSON.stringify({ code }),
    }),
  submitLesson: (lessonId: number, code: string) =>
    request<SubmitResultDto>(`/lessons/${lessonId}/submit`, {
      method: "POST",
      body: JSON.stringify({ code }),
    }),
  runSqlLesson: (lessonId: number, code: string) =>
    request<ExecutionResultDto>(`/sql/lessons/${lessonId}/run`, {
      method: "POST",
      body: JSON.stringify({ code }),
    }),
  submitSqlLesson: (lessonId: number, code: string) =>
    request<SubmitResultDto>(`/sql/lessons/${lessonId}/submit`, {
      method: "POST",
      body: JSON.stringify({ code }),
    }),
  runIntermediateBoss: (bossId: number, code: string) =>
    request<ExecutionResultDto>(`/intermediate-bosses/${bossId}/run`, {
      method: "POST",
      body: JSON.stringify({ code }),
    }),
  submitIntermediateBoss: (bossId: number, code: string) =>
    request<SubmitResultDto>(`/intermediate-bosses/${bossId}/submit`, {
      method: "POST",
      body: JSON.stringify({ code }),
    }),
  revealIntermediateBossHint: (bossId: number) =>
    request<IntermediateBossHintResultDto>(`/intermediate-bosses/${bossId}/hint`, {
      method: "POST",
    }),
  revealIntermediateBossSolution: (bossId: number) =>
    request<IntermediateBossSolutionDto>(`/intermediate-bosses/${bossId}/solution`, {
      method: "POST",
    }),
  runBossFinal: (code: string) =>
    request<ExecutionResultDto>("/boss-final/run", {
      method: "POST",
      body: JSON.stringify({ code }),
    }),
  submitBossFinal: (code: string) =>
    request<SubmitResultDto>("/boss-final/submit", {
      method: "POST",
      body: JSON.stringify({ code }),
    }),
};
