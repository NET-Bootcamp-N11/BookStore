using BookStore.Application;
using BookStore.Domain.Entities.Auth;
using BookStore.Infrastructure;
using MVC.Middlewares;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddMemoryCache();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<AppDBContext>();

/*builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = "733255891197-6kjvhbtu55ioc4uansrsuhh7bn4gi4ig.apps.googleusercontent.com";
        options.ClientSecret = "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";
    });*/

// Logger
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
//builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

app.Run();
