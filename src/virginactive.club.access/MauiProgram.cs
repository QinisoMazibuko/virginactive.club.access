using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using virginactive.club.access.repository;
using virginactive.club.access.services;

namespace virginactive.club.access;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// add Dependency injection and register services 

		builder.Services.AddDbContext<AppDbContext>(options =>
		{
			options.UseSqlite("Filename=ClubAccess.db");
		});

		builder.Services.AddScoped<IAccessLogRepository, AccessLogRepository>();
		builder.Services.AddScoped<IAccessLogService, accessLogService>();


#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
