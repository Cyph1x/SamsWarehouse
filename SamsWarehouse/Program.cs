using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using SamsWarehouse.Models.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SQLDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SamsWarehouse")));

builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHsts(options =>
{
    options.IncludeSubDomains = true;
    //10 years
    options.MaxAge = TimeSpan.FromDays(3650);
});

var app = builder.Build();
app.UseResponseCompression();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Product/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Cache static content (images) for one year
        ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public, max-age=31536000";
    }
});
app.UseSession();
app.UseRouting();
// Custom Middleware (inline)
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self' http://www.w3.org");
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("Strict-Transport-Security", "max-age=63072000; includeSubDomains; ");
    await next(context);
});
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
