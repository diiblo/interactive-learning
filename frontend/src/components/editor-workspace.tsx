"use client";

import { useMemo, useState } from "react";
import type { LessonDetailDto } from "@/types/api";
import { CodeEditor } from "./code-editor";
import { LivePreviewPanel } from "./live-preview-panel";

type EditorTabRole = "html" | "css" | "js" | "react" | "single";

type EditorTab = {
  id: string;
  label: string;
  language: string;
  initialCode: string;
  role: EditorTabRole;
  isPrimary: boolean;
};

type EditorWorkspaceProps = {
  code: string;
  language: string;
  onCodeChange: (code: string) => void;
  preview?: Pick<LessonDetailDto, "previewMode" | "previewHtml" | "previewCss" | "previewJs" | "previewEntry" | "supportsPreview"> | null;
};

export function EditorWorkspace({ code, language, onCodeChange, preview }: EditorWorkspaceProps) {
  const tabs = useMemo(() => createEditorTabs({ code, language, preview }), [code, language, preview]);
  const primaryTab = tabs.find((tab) => tab.isPrimary) ?? tabs[0];
  const [activeTabId, setActiveTabId] = useState(primaryTab.id);
  const [tabCodes, setTabCodes] = useState(() => Object.fromEntries(tabs.map((tab) => [tab.id, tab.initialCode])));
  const activeTab = tabs.find((tab) => tab.id === activeTabId) ?? primaryTab;
  const activeCode = tabCodes[activeTab.id] ?? activeTab.initialCode;
  const previewSource = buildPreviewSource(preview, tabs, tabCodes);

  function updateActiveTab(nextCode: string) {
    setTabCodes((current) => ({ ...current, [activeTab.id]: nextCode }));
    if (activeTab.isPrimary) {
      onCodeChange(nextCode);
    }
  }

  return (
    <div className={`min-h-0 flex-1 ${preview?.supportsPreview ? "grid grid-rows-[minmax(300px,1fr)_minmax(320px,42%)] lg:grid-cols-2 lg:grid-rows-1" : "flex"}`}>
      <div className="flex min-h-0 flex-1 flex-col">
        {tabs.length > 1 ? (
          <div className="flex flex-wrap gap-1 border-b border-[var(--color-border)] bg-[#10151d] px-3 py-2">
            {tabs.map((tab) => (
              <button
                key={tab.id}
                type="button"
                onClick={() => setActiveTabId(tab.id)}
                className={`rounded-md px-3 py-1.5 text-xs font-semibold transition ${
                  activeTab.id === tab.id ? "bg-[var(--color-primary)] text-white" : "border border-[var(--color-border)] bg-[#1e2329] text-[var(--color-text-muted)] hover:text-white"
                }`}
              >
                {tab.label}
              </button>
            ))}
          </div>
        ) : null}
        <div className="min-h-0 flex-1">
          <CodeEditor code={activeCode} language={activeTab.language} onChange={updateActiveTab} />
        </div>
      </div>
      {preview?.supportsPreview ? (
        <LivePreviewPanel
          code={previewSource.code}
          mode={preview.previewMode}
          baseHtml={previewSource.baseHtml}
          baseCss={previewSource.baseCss}
          baseJs={previewSource.baseJs}
          entry={preview.previewEntry}
        />
      ) : null}
    </div>
  );
}

function createEditorTabs({
  code,
  language,
  preview,
}: {
  code: string;
  language: string;
  preview?: Pick<LessonDetailDto, "previewMode" | "previewHtml" | "previewCss" | "previewJs" | "supportsPreview"> | null;
}): EditorTab[] {
  if (!preview?.supportsPreview || preview.previewMode === "none") {
    return [{ id: "single", label: "Code", language, initialCode: code, role: "single", isPrimary: true }];
  }

  if (preview.previewMode === "html-css") {
    return [
      { id: "html", label: "HTML", language: "html", initialCode: preview.previewHtml, role: "html", isPrimary: false },
      { id: "css", label: "CSS", language: "css", initialCode: code || preview.previewCss, role: "css", isPrimary: true },
    ];
  }

  if (preview.previewMode === "javascript-dom") {
    return [
      { id: "html", label: "HTML", language: "html", initialCode: preview.previewHtml, role: "html", isPrimary: false },
      { id: "js", label: "JavaScript", language: "javascript", initialCode: code || preview.previewJs, role: "js", isPrimary: true },
    ];
  }

  if (preview.previewMode === "tailwind") {
    return [{ id: "html", label: "HTML", language: "html", initialCode: code || preview.previewHtml, role: "html", isPrimary: true }];
  }

  if (preview.previewMode === "react") {
    return [{ id: "react", label: "React", language: "typescript", initialCode: code, role: "react", isPrimary: true }];
  }

  return [{ id: "single", label: "Code", language, initialCode: code, role: "single", isPrimary: true }];
}

function buildPreviewSource(
  preview: Pick<LessonDetailDto, "previewMode" | "previewHtml" | "previewCss" | "previewJs" | "supportsPreview"> | null | undefined,
  tabs: EditorTab[],
  tabCodes: Record<string, string>,
) {
  const codeFor = (role: EditorTabRole) => {
    const tab = tabs.find((item) => item.role === role);
    return tab ? tabCodes[tab.id] ?? tab.initialCode : "";
  };

  if (!preview?.supportsPreview) {
    return { code: "", baseHtml: "", baseCss: "", baseJs: "" };
  }

  if (preview.previewMode === "html-css") {
    return { code: codeFor("css"), baseHtml: codeFor("html") || preview.previewHtml, baseCss: preview.previewCss, baseJs: preview.previewJs };
  }

  if (preview.previewMode === "javascript-dom") {
    return { code: codeFor("js"), baseHtml: codeFor("html") || preview.previewHtml, baseCss: preview.previewCss, baseJs: preview.previewJs };
  }

  if (preview.previewMode === "tailwind") {
    return { code: codeFor("html"), baseHtml: preview.previewHtml, baseCss: preview.previewCss, baseJs: preview.previewJs };
  }

  if (preview.previewMode === "react") {
    return { code: codeFor("react"), baseHtml: preview.previewHtml, baseCss: preview.previewCss, baseJs: preview.previewJs };
  }

  return { code: codeFor("single"), baseHtml: preview.previewHtml, baseCss: preview.previewCss, baseJs: preview.previewJs };
}
