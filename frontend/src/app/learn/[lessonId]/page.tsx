import { AppShell } from "@/components/app-shell";

export default async function LessonPage({ params }: { params: Promise<{ lessonId: string }> }) {
  const { lessonId } = await params;
  return <AppShell initialLessonId={Number(lessonId)} />;
}
