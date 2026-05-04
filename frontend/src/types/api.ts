export type LessonProgressStatus = "Locked" | "Available" | "Started" | "Completed";
export type SkillProgressStatus = "New" | "Learning" | "Fragile" | "Solid" | "Mastered" | "ReviewDue";
export type SubmissionErrorCategory =
  | "SyntaxError"
  | "MissingRequiredSnippet"
  | "ForbiddenSnippetUsed"
  | "WrongOutput"
  | "WrongLogic"
  | "EmptyCode"
  | "CompilationError"
  | "RuntimeError"
  | "PartialSolution"
  | "HardcodedSolution"
  | "Unknown";

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

export type IntermediateBossMapItemDto = {
  id: number;
  moduleId: number;
  slug: string;
  title: string;
  xpReward: number;
  status: LessonProgressStatus;
  isLocked: boolean;
  isRequiredToUnlockNextModule: boolean;
};

export type ChapterMapDto = {
  id: number;
  title: string;
  description: string;
  requiredXp: number;
  lessons: LessonMapItemDto[];
  intermediateBoss: IntermediateBossMapItemDto | null;
};

export type CourseMapDto = {
  courseId: number;
  title: string;
  chapters: ChapterMapDto[];
  bossFinal: LessonMapItemDto | null;
};

export type SkillDto = {
  id: number;
  courseLanguage: string;
  slug: string;
  name: string;
  description: string;
};

export type LessonHintDto = {
  hintLevel: number;
  content: string;
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
  hints?: LessonHintDto[];
  skills?: SkillDto[];
};

export type IntermediateBossDetailDto = {
  id: number;
  moduleId: number;
  slug: string;
  title: string;
  editorLanguage: string;
  objective: string;
  instructions: string;
  starterCode: string;
  expectedResult: string;
  validationRules: string[];
  xpReward: number;
  isRequiredToUnlockNextModule: boolean;
  status: LessonProgressStatus;
  attempts: number;
  failedAttempts: number;
  hintsRevealed: number;
  canRevealSolution: boolean;
};

export type IntermediateBossHintDto = {
  index: number;
  content: string;
};

export type IntermediateBossHintResultDto = {
  hints: IntermediateBossHintDto[];
  hasMoreHints: boolean;
};

export type IntermediateBossSolutionDto = {
  solution: string;
};

export type ExecutionResultDto = {
  success: boolean;
  output: string;
  diagnostics: string[];
  durationMs: number;
  columns?: string[];
  rows?: Record<string, string | number | boolean | null>[];
};

export type AiProviderConfigDto = {
  id: string;
  type: string;
  name: string;
  apiKey: string;
  model: string;
  baseUrl?: string | null;
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

export type RelatedSkillDto = {
  slug: string;
  name: string;
  masteryPercent: number;
  status: SkillProgressStatus;
};

export type SubmissionFeedbackDto = {
  isSuccess: boolean;
  summary: string;
  whatWentWell: string[];
  whatIsMissing: string[];
  errorCategory: SubmissionErrorCategory;
  progressiveHints: string[];
  relatedSkills: RelatedSkillDto[];
  suggestedReviewLessonSlugs: string[];
};

export type SkillResultDto = {
  skillSlug: string;
  skillName: string;
  scorePercent: number;
  status: SkillProgressStatus;
  feedback: string;
};

export type BossResultDto = {
  isSuccess: boolean;
  scorePercent: number;
  summary: string;
  skillResults: SkillResultDto[];
  strengths: string[];
  weaknesses: string[];
  suggestedReviews: string[];
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
  structuredFeedback?: SubmissionFeedbackDto | null;
  bossResult?: BossResultDto | null;
};

export type ProgressDto = {
  profile: ProfileDto;
  completedLessons: number;
  totalLessons: number;
  completedLessonIds: number[];
  availableLessonIds: number[];
};

export type SkillProgressDto = {
  skillId: number;
  courseLanguage: string;
  skillSlug: string;
  skillName: string;
  description: string;
  masteryPercent: number;
  successfulAttempts: number;
  failedAttempts: number;
  nextReviewAt: string | null;
  status: SkillProgressStatus;
  isReviewDue: boolean;
  reviewLessonSlug: string | null;
};

export type ReviewItemDto = {
  skillSlug: string;
  skillName: string;
  courseLanguage: string;
  masteryPercent: number;
  status: SkillProgressStatus;
  nextReviewAt: string | null;
  suggestedLessonSlugs: string[];
};
