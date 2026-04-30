export type LessonProgressStatus = "Locked" | "Available" | "Started" | "Completed";

export type BadgeDto = {
  id: number;
  slug: string;
  name: string;
  description: string;
  iconName: string;
  earnedAt: string | null;
};

export type ProfileDto = {
  id: number;
  displayName: string;
  totalXp: number;
  level: number;
  badges: BadgeDto[];
};

export type CourseSummaryDto = {
  id: number;
  slug: string;
  title: string;
  description: string;
  language: string;
};

export type LessonMapItemDto = {
  id: number;
  slug: string;
  title: string;
  xpReward: number;
  status: LessonProgressStatus;
  isLocked: boolean;
  isBossFinal: boolean;
};

export type ChapterMapDto = {
  id: number;
  title: string;
  description: string;
  requiredXp: number;
  lessons: LessonMapItemDto[];
};

export type CourseMapDto = {
  courseId: number;
  title: string;
  chapters: ChapterMapDto[];
  bossFinal: LessonMapItemDto | null;
};

export type LessonDetailDto = {
  id: number;
  slug: string;
  title: string;
  editorLanguage: string;
  objective: string;
  conceptSummary: string;
  commonMistakes: string;
  explanation: string;
  exampleCode: string;
  exercisePrompt: string;
  starterCode: string;
  successFeedback: string;
  failureFeedback: string;
  finalCorrection: string;
  xpReward: number;
  isBossFinal: boolean;
  status: LessonProgressStatus;
};

export type ExecutionResultDto = {
  success: boolean;
  output: string;
  diagnostics: string[];
  durationMs: number;
  columns?: string[];
  rows?: Record<string, string | number | boolean | null>[];
};

export type SqlColumnSchemaDto = {
  name: string;
  type: string;
  isNullable: boolean;
  description: string | null;
};

export type SqlTableSchemaDto = {
  name: string;
  description: string;
  columns: SqlColumnSchemaDto[];
};

export type SqlSchemaDto = {
  scenario: string;
  tables: SqlTableSchemaDto[];
  safetyRules: string[];
};

export type TestResultDto = {
  name: string;
  passed: boolean;
  message: string;
};

export type SubmitResultDto = {
  passed: boolean;
  output: string;
  testResults: TestResultDto[];
  feedback: string;
  xpEarned: number;
  newLevel: number;
  unlockedLessonIds: number[];
  earnedBadges: BadgeDto[];
};

export type ProgressDto = {
  profile: ProfileDto;
  completedLessons: number;
  totalLessons: number;
  completedLessonIds: number[];
  availableLessonIds: number[];
};
