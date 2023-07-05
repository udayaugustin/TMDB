using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using LocalizationResourceManager.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using TMDB.Interfaces;
using TMDB.Resources;
using TMDB.Services;
using TMDB.ViewModels;

namespace TMDB;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseMauiCommunityToolkitMarkup()
			.RegisterViewModels()
			.RegisterServices()
			.UseLocalizationResourceManager(settings =>
			{
				settings.RestoreLatestCulture(true);
				settings.AddResource(AppResources.ResourceManager);
			})
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("fa-solid-900.ttf", "FA");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

	public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
	{
		mauiAppBuilder.Services.AddSingleton<LoginViewModel>();
        mauiAppBuilder.Services.AddSingleton<DetailPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<DashboardPageViewModel>();

        return mauiAppBuilder;
	}

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
		mauiAppBuilder.Services.AddSingleton<IHttpClient, HttpClientService>();		

        return mauiAppBuilder;
    }
}
