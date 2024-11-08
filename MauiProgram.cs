using inventory_mobile_app.Pages;
using inventory_mobile_app.Pages.Auth;
using inventory_mobile_app.Services;
using inventory_mobile_app.ViewModels;
using Microsoft.Extensions.Logging;

namespace inventory_mobile_app
{
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
                    fonts.AddFont("MaterialIconsOutlined-Regular.otf", "MaterialIcon");
                    fonts.AddFont("bootstrap-icons.ttf", "BootstrapIcon");
                });


            builder.Services.AddHttpClient("custom-httpclient", httpClient =>
            {
                var baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://147.79.74.213" : "https://147.79.74.213";
                httpClient.BaseAddress = new Uri(baseAddress);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();

                // Disable SSL validation for testing purposes (only in development)
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                return handler;
            });

            builder.Services.AddSingleton<ClientService>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<CategoryViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<Category>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<LoginPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
