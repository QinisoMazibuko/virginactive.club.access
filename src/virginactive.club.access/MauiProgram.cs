using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using virginactive.club.access.repository;
using virginactive.club.access.services;
using ZXing.Net.Maui.Controls;

namespace virginactive.club.access;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseBarcodeReader()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiCommunityToolkit();
        // add Dependency injection and register services
        string databasePath = Path.Combine(FileSystem.AppDataDirectory, "clubaccess1.db");
        builder
            .Services
            .AddDbContext<AppDbContext>(
                options => options.UseSqlite($"Data Source={databasePath}")
            );
        builder.Services.AddScoped<IAccessLogRepository, AccessLogRepository>();
        builder.Services.AddScoped<IMemberRepository, memberRepository>();
        builder.Services.AddScoped<IAccessLogService, accessLogService>();
        builder.Services.AddScoped<IMemberService, memberService>();
        builder.Services.AddSingleton<MainPageViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        var app = builder.Build();
        // Trigger database seeding
        SeedDatabase(app.Services);
        return app;
    }

    private static void SeedDatabase(IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            try
            {
                dbContext.Database.EnsureCreated();
                dbContext.SeedDatabaseAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                //  logging the exception or handling it as per application's error handling policy
                Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
            }
        }
    }
}
