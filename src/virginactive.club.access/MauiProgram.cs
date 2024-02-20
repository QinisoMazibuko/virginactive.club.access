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

        // register local Db context
        string databasePath = Path.Combine(FileSystem.AppDataDirectory, "clubaccess2.db");
        builder
            .Services
            .AddDbContext<AppDbContext>(
                options => options.UseSqlite($"Data Source={databasePath}")
            );

        // register Cloud sync Db context
        string dbconnectionstring = "dbconection-String";
        builder
            .Services
            .AddDbContext<CloudDbContext>(Options => Options.UseSqlServer(dbconnectionstring));

        // register app services for DI
        builder.Services.RegisterApplicationServices();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        var app = builder.Build();
        // Trigger database seeding
        SeedDatabase(app.Services);
        return app;
    }

    public static void RegisterApplicationServices(this IServiceCollection Services)
    {
        Services.AddScoped<IAccessLogRepository, AccessLogRepository>();
        Services.AddScoped<IMemberRepository, memberRepository>();
        Services.AddScoped<ICloudAccessLogRepository, CloudAccessLogRepository>();

        Services.AddScoped<IAccessLogService, accessLogService>();
        Services.AddScoped<IMemberService, memberService>();
        Services.AddScoped<IDataSyncService, DataSyncService>();
        Services.AddSingleton<MainPageViewModel>();
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

        using (var scope = services.CreateScope())
        {
            var cloudContext = scope.ServiceProvider.GetRequiredService<CloudDbContext>();
            try
            {
                cloudContext.Database.EnsureCreated();
                cloudContext.SeedDatabaseAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                //  logging the exception or handling it as per application's error handling policy
                Console.WriteLine(
                    $"An error occurred while seeding the Cloud database: {ex.Message}"
                );
            }
        }
    }
}
