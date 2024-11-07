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
                });


            builder.Services.AddHttpClient("custom-httpclient", httpClient =>
            {
                var baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://drensharp.dev" : "https://drensharp.dev";
                httpClient.BaseAddress = new Uri(baseAddress);
            });

            builder.Services.AddSingleton<ClientService>();
            builder.Services.AddSingleton<CategoryViewModel>();
            builder.Services.AddSingleton<Category>();

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginViewModel>();

            builder.Services.AddSingleton<SignupPage>();
            builder.Services.AddSingleton<SignupViewModel>();

            builder.Services.AddSingleton<SetPassword>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
