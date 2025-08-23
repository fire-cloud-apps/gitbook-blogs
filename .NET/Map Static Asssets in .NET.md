---
icon: map
---

# Map Static Assets in ASP.NET

### Key Points

* MapStaticAssets in ASP.NET Core 9.0 seems likely to optimize static asset delivery, making web applications faster by compressing and fingerprinting files like CSS and JavaScript.
* It appears to be a simple replacement for older methods, potentially reducing file sizes by up to 92%, which could improve user experience.
* Research suggests it’s easy to implement, requiring just a line of code, and works well with modern frameworks like Blazor and Razor Pages.

***

### Why This Topic?

If you’re looking to learn or refresh your knowledge in .NET, focusing on **MapStaticAssets** in ASP.NET Core 9.0 is a great choice. It’s a new feature in the latest stable version of .NET (released in November 2024), addressing a common challenge in web development: efficiently serving static assets like CSS, JavaScript, and images. This can significantly boost application performance, which is crucial for user satisfaction in 2025.

***

### What It Does

**MapStaticAssets** is a middleware that optimizes how static files are delivered. It:

* **Fingerprints files**: Adds a unique hash to filenames (e.g., `style.css` becomes `style.abc123.css`), ensuring browsers cache the right version and only redownload when content changes.
* **Compresses files**: Uses gzip and brotli at build time, reducing file sizes dramatically—for example, a Razor Pages template might shrink from 331.1 KB to 65.5 KB (80.20% reduction).
* **Handles caching**: Sets proper headers to avoid unnecessary browser requests, improving load times.

This seems likely to make your web apps faster and more efficient, especially for high-traffic sites.

***

### How to Use It

Using MapStaticAssets is straightforward. You replace `app.UseStaticFiles();` with `app.MapStaticAssets();` in your application pipeline. Here’s a quick example:

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
var app = builder.Build();
app.MapStaticAssets(); // Replace the old line here
app.MapRazorPages();
app.Run();
```

It works with frameworks like Blazor, Razor Pages, and MVC, and is designed for assets known at build time. If you need to serve files from disk at runtime, stick with `UseStaticFiles()`.

***

***

### Survey Note: Deep Dive into MapStaticAssets in ASP.NET Core 9.0

As of August 5, 2025, the .NET ecosystem continues to evolve, with .NET 9 being the current stable release (launched in November 2024) and .NET 10 in preview. Given your objective to learn or refresh knowledge in .NET, I’ve selected **MapStaticAssets**, a new feature in ASP.NET Core 9.0, as an interesting and practical topic. This feature, detailed in Microsoft’s official documentation, addresses a critical aspect of web development: optimizing static asset delivery. Below, I’ll explore why it’s important, what it does, how to use it, and provide visual and tabular insights to enhance understanding.

#### Background and Relevance

The .NET platform, as of 2025, is a cross-platform, open-source framework for building various applications, including web, mobile, and desktop. ASP.NET Core, a key component, has seen significant updates in .NET 9, with MapStaticAssets being one of the notable additions. This feature is particularly relevant given the emphasis on performance in modern web development, where static assets (e.g., CSS, JavaScript, images) can significantly impact load times and user experience.

From the documentation, .NET 9 is supported for three years as a long-term support (LTS) release, making it a stable choice for learning. The official resources, such as [What’s new in ASP.NET Core in .NET 9 | Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-9.0?view=aspnetcore-9.0), highlight MapStaticAssets as a middleware that replaces `UseStaticFiles` in many scenarios, optimizing delivery for UI frameworks like Blazor, Razor Pages, and MVC.

#### Why MapStaticAssets Matters

In web applications, static assets are essential for rendering and functionality, but inefficient delivery can lead to slow load times and poor performance. MapStaticAssets automates best practices, addressing issues like:

* **Unnecessary requests**: Without proper caching, browsers may repeatedly request files, slowing down the application.
* **Large file sizes**: Uncompressed files consume bandwidth, impacting load times, especially on mobile devices.
* **Caching mismatches**: Without fingerprinting, browsers might serve stale content, affecting user experience.

By optimizing these aspects, MapStaticAssets seems likely to enhance performance, which is critical for user satisfaction and SEO rankings in 2025. The documentation emphasizes its role in reducing latency and improving scalability, particularly for high-traffic applications.

#### What Does MapStaticAssets Do?

MapStaticAssets is designed to optimize static asset delivery through several mechanisms:

* **Fingerprinting**: It adds a content-based hash to filenames (e.g., `style.css` becomes `style.abc123.css`), ensuring browsers cache the correct version. This is achieved using Base64 encoded SHA-256 hashes, as per the documentation.
* **Compression**: At build time, it compresses files using gzip in development and both gzip and brotli during publish, significantly reducing sizes. For instance:
  * Razor Pages template: 331.1 KB to 65.5 KB (80.20% reduction).
  * Fluent UI Blazor: 478 KB to 84 KB (82.43% reduction).
  * MudBlazor: 588.4 KB to 46.7 KB (92.07% reduction).
* **Caching**: It sets ETags and caching headers to ensure browsers store files efficiently, avoiding unnecessary requests.

These optimizations are particularly beneficial for applications where static assets constitute a large portion of the payload, such as single-page applications (SPAs) built with Blazor.

#### How to Use MapStaticAssets

Implementing MapStaticAssets is remarkably simple, as evidenced by the documentation. You replace the traditional `app.UseStaticFiles();` with `app.MapStaticAssets();` in your application pipeline. Here’s a detailed example:

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets(); // Replace app.UseStaticFiles() with this
app.MapRazorPages();
app.Run();
```

**Key Considerations**:

* **Compatibility**: It works seamlessly with Blazor, Razor Pages, and MVC, making it versatile for various web development scenarios.
* **Limitations**: Use `UseStaticFiles` for assets served from disk or embedded resources at runtime, as MapStaticAssets is optimized for assets known at build/publish time.
* **Performance Impact**: The documentation notes that compared to dynamic compression (e.g., IIS gzip), MapStaticAssets offers better performance due to build-time optimization. For example, MudBlazor files are reduced to 37.5 KB with MapStaticAssets versus \~90 KB with IIS gzip, a 59% further reduction.

#### Visual Representation

To better understand the process, let’s visualize how MapStaticAssets works using a Mermaid flowchart. This diagram breaks down the workflow into build-time and runtime phases:

```mermaid
flowchart TD
    subgraph Build Time
        A[Build application] --> B[Fingerprint and compress static assets]
    end
    subgraph Runtime
        C[Browser] --> D[Request static file]
        D --> E[MapStaticAssets middleware]
        E --> F[Serve optimized file]
        F --> C
    end
```

* **Build Time**: During the build, static assets are fingerprinted (e.g., adding hashes to filenames) and compressed using gzip and brotli.
* **Runtime**: When a browser requests a static file, the MapStaticAssets middleware serves the optimized version with proper caching headers, ensuring efficient delivery.

This visualization highlights the automation and efficiency of MapStaticAssets, making it easier to grasp its impact on performance.

#### Comparative Analysis

To quantify the benefits, let’s look at a table comparing file size reductions for different templates, as provided in the documentation:

| Template         | Original Size (KB) | Compressed Size (KB) | Reduction Percentage |
| ---------------- | ------------------ | -------------------- | -------------------- |
| Razor Pages      | 331.1              | 65.5                 | 80.20%               |
| Fluent UI Blazor | 478.0              | 84.0                 | 82.43%               |
| MudBlazor        | 588.4              | 46.7                 | 92.07%               |

This table underscores the significant performance gains, with MudBlazor achieving the highest reduction, demonstrating MapStaticAssets’ effectiveness across different scenarios.

#### Benefits and Best Practices

MapStaticAssets offers several benefits, enhancing both developer experience and application performance:

* **Build-time compression** 🛠️: Reduces file sizes at build time, minimizing server load and improving download speeds.
* **Content-based ETags** 🔒: Ensures browsers only redownload changed files, saving bandwidth and reducing latency.
* **Improved performance** ⚡: Faster load times, especially beneficial for mobile users and high-traffic applications.
* **Simplified development** 📦: Automates best practices, reducing the need for manual configuration and server-specific setups.

For best practices, ensure your assets are included in the build process (e.g., in `wwwroot`), and consider using it alongside other ASP.NET Core 9.0 features like Blazor enhancements for a holistic performance boost.

#### Conclusion

MapStaticAssets in ASP.NET Core 9.0 is a powerful feature for optimizing static asset delivery, aligning with the latest trends in web development for 2025. By automating fingerprinting, compression, and caching, it simplifies the process of building high-performance web applications, particularly with frameworks like Blazor and Razor Pages. Whether you’re refreshing your knowledge or learning anew, implementing MapStaticAssets can significantly enhance your .NET projects, ensuring faster load times and better user experiences.

For further reading, refer to:

* [MapStaticAssets routing endpoint conventions | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.staticassetsendpointroutebuilderextensions.mapstaticassets)
* [What’s new in ASP.NET Core in .NET 9 | Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-9.0?view=aspnetcore-9.0)

This detailed exploration should equip you with the knowledge to leverage MapStaticAssets effectively, contributing to your learning journey in the .NET ecosystem. 🌟
