"use client";

import { useMemo, useState } from "react";
import type { LessonDetailDto } from "@/types/api";

type PreviewMode = LessonDetailDto["previewMode"];

type LivePreviewPanelProps = {
  code: string;
  mode: PreviewMode;
  baseHtml?: string;
  baseCss?: string;
  baseJs?: string;
  entry?: string;
};

const sizes = [
  { label: "Mobile", width: 375 },
  { label: "Tablet", width: 768 },
  { label: "Desktop", width: 1280 },
];

const helpByMode: Record<PreviewMode, string> = {
  none: "Preview non disponible pour cette lecon.",
  "html-css": "Le CSS est applique au HTML de l'exercice.",
  tailwind: "Les classes Tailwind sont rendues via le CDN Tailwind.",
  "javascript-dom": "Le JavaScript agit sur le HTML de depart de l'exercice.",
  react: "Declare un composant App ou ProductManager. Les imports/exports ne sont pas necessaires dans ce mode.",
};

export function LivePreviewPanel({ code, mode, baseHtml = "", baseCss = "", baseJs = "", entry = "root" }: LivePreviewPanelProps) {
  const [width, setWidth] = useState(768);
  const srcDoc = useMemo(() => buildPreviewDocument({ code, mode, baseHtml, baseCss, baseJs, entry }), [baseCss, baseHtml, baseJs, code, entry, mode]);

  return (
    <section className="flex min-h-0 flex-col border-t border-[var(--color-border)] bg-[#0d1117] lg:border-l lg:border-t-0">
      <div className="flex flex-wrap items-center justify-between gap-2 border-b border-[var(--color-border)] bg-[#161b22] px-3 py-2">
        <p className="text-sm font-semibold text-white">Preview</p>
        <div className="flex rounded-md border border-[var(--color-border)] bg-[#1e2329] p-1">
          {sizes.map((size) => (
            <button
              key={size.width}
              type="button"
              onClick={() => setWidth(size.width)}
              className={`rounded px-2 py-1 text-xs font-semibold transition ${
                width === size.width ? "bg-[var(--color-primary)] text-white" : "text-[var(--color-text-muted)] hover:text-white"
              }`}
            >
              {size.label}
            </button>
          ))}
        </div>
      </div>
      <p className="border-b border-[var(--color-border)] bg-[#10151d] px-3 py-2 text-xs text-[var(--color-text-muted)]">{helpByMode[mode] ?? helpByMode.none}</p>
      <div className="min-h-0 flex-1 overflow-auto bg-[#05070a] p-3">
        <div className="mx-auto h-full max-w-full overflow-hidden rounded-md border border-[var(--color-border)] bg-white" style={{ width }}>
          <iframe title="Live preview" sandbox="allow-scripts" className="h-full min-h-[320px] w-full bg-white" srcDoc={srcDoc} />
        </div>
      </div>
    </section>
  );
}

function buildPreviewDocument({
  code,
  mode,
  baseHtml,
  baseCss,
  baseJs,
  entry,
}: {
  code: string;
  mode: PreviewMode;
  baseHtml: string;
  baseCss: string;
  baseJs: string;
  entry: string;
}) {
  const reset = `
    *, *::before, *::after { box-sizing: border-box; }
    body { margin: 0; min-height: 100vh; font-family: Inter, ui-sans-serif, system-ui, -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif; background: #f8fafc; color: #0f172a; }
    button, input { font: inherit; }
  `;
  const errorTools = previewErrorTools();

  if (mode === "tailwind") {
    return documentShell(`
      <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <script src="https://cdn.tailwindcss.com"></script>
        <style>${reset}${previewErrorStyles()}</style>
      </head>
      <body>
        ${code || baseHtml}
        <script>${errorTools}</script>
      </body>
    `);
  }

  if (mode === "javascript-dom") {
    return documentShell(`
      <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <style>${reset}${previewErrorStyles()}${baseCss}</style>
      </head>
      <body>
        ${baseHtml}
        <script>${errorTools}</script>
        <script>${baseJs}</script>
        <script>try { ${code} } catch (error) { window.__showPreviewError(error && error.message ? error.message : String(error)); }</script>
      </body>
    `);
  }

  if (mode === "react") {
    const rootId = escapeScript(entry || "root");
    const reactCode = prepareReactCode(code);
    return documentShell(`
      <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <style>${reset}${previewErrorStyles()}${baseCss}</style>
      </head>
      <body>
        <div id="${escapeAttribute(entry || "root")}"></div>
        <script>${errorTools}</script>
        <script src="https://unpkg.com/react@18/umd/react.development.js"></script>
        <script src="https://unpkg.com/react-dom@18/umd/react-dom.development.js"></script>
        <script src="https://unpkg.com/@babel/standalone/babel.min.js"></script>
        <script type="text/babel" data-presets="env,react,typescript">
          try {
            const { useState, useEffect, useMemo, useCallback, useReducer, createContext, useContext } = React;
            Object.assign(window, { React, ReactDOM, useState, useEffect, useMemo, useCallback, useReducer, createContext, useContext });
            ${baseJs}
            ${reactCode}
            const PreviewComponent = typeof App !== 'undefined' ? App : (typeof ProductManager !== 'undefined' ? ProductManager : null);
            if (PreviewComponent) {
              ReactDOM.createRoot(document.getElementById('${rootId}')).render(<PreviewComponent />);
            } else {
              window.__showPreviewError('Aucun composant App ou ProductManager detecte.');
            }
          } catch (error) {
            window.__showPreviewError(error && error.message ? error.message : String(error));
          }
        </script>
      </body>
    `);
  }

  if (mode === "html-css") {
    return documentShell(`
      <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <style>${reset}${previewErrorStyles()}${code || baseCss}</style>
      </head>
      <body>
        ${baseHtml}
        <script>${errorTools}</script>
      </body>
    `);
  }

  return documentShell(`
    <head>
      <meta charset="utf-8">
      <meta name="viewport" content="width=device-width, initial-scale=1">
      <style>${reset}${previewErrorStyles()}</style>
    </head>
    <body>
      <script>${errorTools}window.__showPreviewError('Mode de preview inconnu.');</script>
    </body>
  `);
}

function previewErrorStyles() {
  return `
    #preview-error {
      margin: 16px;
      border: 1px solid #fecaca;
      border-radius: 10px;
      background: #fef2f2;
      color: #7f1d1d;
      padding: 12px 14px;
      font: 14px/1.45 Inter, ui-sans-serif, system-ui, sans-serif;
      box-shadow: 0 10px 24px rgba(127, 29, 29, 0.12);
    }
    #preview-error strong { display: block; margin-bottom: 4px; color: #991b1b; }
    #preview-error code { display: block; margin: 6px 0; color: #450a0a; word-break: break-word; }
    #preview-error span { display: block; color: #7f1d1d; }
  `;
}

function previewErrorTools() {
  return `
    window.__showPreviewError = function(message) {
      var detail = message && message.message ? message.message : String(message || 'Erreur inconnue');
      var error = document.getElementById('preview-error');
      if (!error) {
        error = document.createElement('div');
        error.id = 'preview-error';
        document.body.prepend(error);
      }
      error.innerHTML = '<strong>Erreur de preview</strong><code></code><span>Conseil : verifier le nom du composant, la syntaxe JSX ou les elements HTML cibles.</span>';
      error.querySelector('code').textContent = detail;
    };
    window.onerror = function(message) {
      window.__showPreviewError(message);
      return false;
    };
    window.addEventListener('unhandledrejection', function(event) {
      var reason = event.reason;
      window.__showPreviewError(reason && reason.message ? reason.message : reason);
    });
  `;
}

function documentShell(content: string) {
  return `<!doctype html><html>${content}</html>`;
}

function escapeAttribute(value: string) {
  return value.replaceAll('"', "&quot;");
}

function escapeScript(value: string) {
  return value.replaceAll("\\", "\\\\").replaceAll("'", "\\'");
}

function prepareReactCode(code: string) {
  return code
    .split("\n")
    .filter((line) => !line.trim().startsWith("import "))
    .join("\n")
    .replaceAll("export default function", "function")
    .replaceAll("export function", "function")
    .replaceAll("export const", "const")
    .replaceAll("export type", "type")
    .replaceAll("export interface", "interface");
}
