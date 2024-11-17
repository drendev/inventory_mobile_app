using inventory_mobile_app.Pages;
using inventory_mobile_app.Pages.Auth;
using inventory_mobile_app.Services;
using inventory_mobile_app.ViewModels;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;

namespace inventory_mobile_app
{
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
                    fonts.AddFont("MaterialIconsOutlined-Regular.otf", "MaterialIcon");
                    fonts.AddFont("bootstrap-icons.ttf", "BootstrapIcon");
                });


            builder.Services.AddHttpClient("custom-httpclient", httpClient =>
            {
                var baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://217.15.170.62" : "https://217.15.170.62";
                httpClient.BaseAddress = new Uri(baseAddress);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;
                return handler;
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

            builder.Services.AddSingleton<InventoryPage>();

            builder.Services.AddSingleton<ProductListViewModel>();
            builder.Services.AddSingleton<Inventory>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
