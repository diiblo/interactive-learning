"use client";

import Editor from "@monaco-editor/react";

type CodeEditorProps = {
  code: string;
  language?: string;
  onChange: (code: string) => void;
};

export function CodeEditor({ code, language = "csharp", onChange }: CodeEditorProps) {
  return (
    <Editor
      height="100%"
      language={language}
      theme="vs-dark"
      value={code}
      onChange={(value) => onChange(value ?? "")}
      options={{
        minimap: { enabled: false },
        fontSize: 15,
        fontFamily: "var(--font-geist-mono), Consolas, monospace",
        padding: { top: 16 },
        scrollBeyondLastLine: false,
        smoothScrolling: true,
        lineHeight: 24,
        automaticLayout: true,
      }}
    />
  );
}
