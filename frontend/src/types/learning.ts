import type { ExecutionResultDto, LessonDetailDto, SubmitResultDto } from "./api";

export type WorkspaceState = {
  lesson: LessonDetailDto | null;
  code: string;
  output: string;
  diagnostics: string[];
  isRunning: boolean;
  lastRun: ExecutionResultDto | null;
  lastSubmit: SubmitResultDto | null;
};
