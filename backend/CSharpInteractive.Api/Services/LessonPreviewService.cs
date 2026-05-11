using CSharpInteractive.Api.Models;

namespace CSharpInteractive.Api.Services;

public sealed record LessonPreviewMetadata(
    string PreviewMode,
    string PreviewHtml,
    string PreviewCss,
    string PreviewJs,
    string PreviewEntry,
    bool SupportsPreview);

public static class LessonPreviewService
{
    public static LessonPreviewMetadata For(Lesson lesson)
    {
        var mode = PreviewModeFor(lesson);
        return new LessonPreviewMetadata(
            mode,
            PreviewHtmlFor(lesson),
            "",
            "",
            "root",
            mode != "none");
    }

    private static string PreviewModeFor(Lesson lesson) =>
        lesson.Chapter?.Course?.Language switch
        {
            "css" => "html-css",
            "tailwindcss" => "tailwind",
            "javascript" => "javascript-dom",
            "react" => "react",
            _ => "none"
        };

    private static string PreviewHtmlFor(Lesson lesson) =>
        lesson.Chapter?.Course?.Language switch
        {
            "css" => CssPreviewHtmlFor(lesson.Slug),
            "javascript" => JavaScriptPreviewHtmlFor(lesson.Slug),
            _ => ""
        };

    private static string CssPreviewHtmlFor(string slug) =>
        slug switch
        {
            "css-responsive-grid" or "css-project-responsive-grid" => """
                <main class="preview-page">
                  <h1 class="product-title">Responsive Products</h1>
                  <section class="products">
                    <article class="product-card"><h2>Book</h2><p>Modern CSS guide</p></article>
                    <article class="product-card"><h2>Keyboard</h2><p>Low profile</p></article>
                    <article class="product-card"><h2>Notebook</h2><p>Product notes</p></article>
                    <article class="product-card"><h2>Mouse</h2><p>Wireless</p></article>
                  </section>
                </main>
                """,
            "css-fluid-width" => """
                <main class="preview-page">
                  <article class="product-card">
                    <h1>Book</h1>
                    <p>Centre cette page et limite sa largeur sans casser le mobile.</p>
                  </article>
                </main>
                """,
            "css-clamp-font-size" => """
                <main class="preview-page">
                  <h1 class="product-title">Product Dashboard</h1>
                  <p>Le titre doit rester lisible entre mobile, tablette et desktop.</p>
                </main>
                """,
            "css-project-product-hero" => """
                <main class="preview-page">
                  <section class="product-hero">
                    <div>
                      <p>Featured product</p>
                      <h1 class="product-title">Book Pro</h1>
                      <button>Buy now</button>
                    </div>
                    <article class="product-card">Stock: 12</article>
                  </section>
                </main>
                """,
            "css-project-form-section" or "css-form" => """
                <main class="preview-page">
                  <form class="product-form">
                    <input placeholder="Product name" />
                    <input placeholder="Price" />
                    <button>Save product</button>
                  </form>
                </main>
                """,
            _ => """
                <main class="preview-page">
                  <header class="site-header">Product Dashboard</header>
                  <section class="product-hero">
                    <div>
                      <h1 class="product-title">Product Page</h1>
                      <button>Add product</button>
                    </div>
                    <article class="product-card">Featured product</article>
                  </section>
                  <section class="product-grid products">
                    <article class="product-card">
                      <h2>Book</h2>
                      <p>Modern CSS guide</p>
                      <button>Add product</button>
                    </article>
                    <article class="product-card">
                      <h2>Keyboard</h2>
                      <p>Low profile</p>
                      <button>Add product</button>
                    </article>
                  </section>
                  <form class="product-form">
                    <input placeholder="Product name" />
                    <button>Save</button>
                  </form>
                </main>
                """
        };

    private static string JavaScriptPreviewHtmlFor(string slug) =>
        slug switch
        {
            "js-event-listener" => """
                <main class="preview-page">
                  <h1>Counter</h1>
                  <button id="counter">0</button>
                </main>
                """,
            "js-render-product-list" => """
                <main class="preview-page">
                  <h1>Product List</h1>
                  <ul id="products"></ul>
                </main>
                """,
            "js-add-product" or "js-form-submit" => """
                <main class="preview-page">
                  <h1>Add Product</h1>
                  <form id="product-form">
                    <input id="product-name" placeholder="Product name" />
                    <button type="submit">Add</button>
                  </form>
                  <ul id="products"></ul>
                </main>
                """,
            "js-delete-product" or "js-filter-products" or "js-empty-state" or "js-local-storage-products" or "javascript-boss-final-product-list" => """
                <main class="preview-page">
                  <h1>Product List</h1>
                  <form id="product-form">
                    <input id="product-name" placeholder="Product name" />
                    <button type="submit">Add</button>
                  </form>
                  <input id="product-filter" placeholder="Filter" />
                  <ul id="products"></ul>
                  <p id="empty-state"></p>
                  <p id="status"></p>
                </main>
                """,
            _ => """
                <main class="preview-page">
                  <h1>Product List</h1>
                  <form id="product-form">
                    <input id="product-name" placeholder="Product name" />
                    <button type="submit">Add</button>
                  </form>
                  <input id="product-filter" placeholder="Filter" />
                  <ul id="product-list"></ul>
                  <ul id="products"></ul>
                  <p id="status"></p>
                </main>
                """
        };
}
