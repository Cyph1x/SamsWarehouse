using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using SamsWarehouse.Models;
using SamsWarehouse.Models.Data;
using System;

var builder = WebApplication.CreateBuilder(args);
//set the environment to development
builder.Environment.EnvironmentName = Environments.Development;

// Enable response compression.
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});
// Add services to the container.
builder.Services.AddControllersWithViews();
// Add the database context.
if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME")))
{
    builder.Services.AddDbContext<SQLDBContext>();
}
else
{
    builder.Services.AddDbContext<SQLDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SamsWarehouse")));
}


// Add session support.
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
// Add HSTS to tell the browser to only use HTTPS.
builder.Services.AddHsts(options =>
{
    options.IncludeSubDomains = true;
    // 10 years.
    options.MaxAge = TimeSpan.FromDays(3650);
});

var app = builder.Build();
app.UseResponseCompression();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Product/Error");
    app.UseHsts();
    // Use caching on static files.
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
        {
            // Cache static content (images) for one year.
            ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public, max-age=31536000";
        }
    });
}
else
{
    app.UseStaticFiles();
}

// Redirect HTTP to HTTPS.
app.UseHttpsRedirection();
// Add session support.
app.UseSession();
app.UseRouting();
// Custom Middleware (inline) for security.
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self' http://www.w3.org/2000/svg; img-src data: 'self' http://www.w3.org/2000/svg");
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    await next(context);
});



app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();