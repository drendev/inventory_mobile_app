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
                httpClient.BaseAddress = new Uri("https://217.15.170.62");
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    // Only disable certificate validation in development environments
#if DEBUG
                    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;
#endif
                }
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

            builder.Services.AddSingleton<Inventory>();

            builder.Services.AddSingleton<ScannerViewModel>();
            builder.Services.AddSingleton<ScanPage>();
            builder.Services.AddSingleton<Scan>();
            builder.Services.AddSingleton<SettingsViewModel>();
            builder.Services.AddSingleton<SettingsPage>();

            builder.Services.AddTransient<ProductListViewModel>();
            builder.Services.AddTransient<ProductViewPage>();
            builder.Services.AddTransient<ProductViewModel>();
            builder.Services.AddTransient<HistoryPage>();
            builder.Services.AddTransient<ReportListViewModel>();
            builder.Services.AddTransient<DashboardViewModel>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<StocksViewModel>();
            builder.Services.AddTransient<ViewStocksPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
