import type { Metadata } from "next";
import "./globals.css";

export const metadata: Metadata = {
  title: "interactive-learning",
  description: "Apprendre C# par la pratique avec un parcours gamifie.",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html
      lang="fr"
      className="h-full antialiased"
      suppressHydrationWarning
    >
      <body className="min-h-full flex flex-col">{children}</body>
    </html>
  );
}
