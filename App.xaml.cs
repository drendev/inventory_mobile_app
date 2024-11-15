using inventory_mobile_app.Pages;
using inventory_mobile_app.Pages.Auth;
using static inventory_mobile_app.ViewModels.MainPageViewModel;

namespace inventory_mobile_app
{
    public partial class App : Application
    {
        public static MainViewModel MainViewModel { get; private set; }

        public App()
        {
            InitializeComponent();

            MainViewModel = new MainViewModel();
            MainPage = new SplashScreenPage();
            
            StartApp();
        }

        private async void StartApp()
        {
            await Task.Delay(2000);

            var authenticationData = await SecureStorage.Default.GetAsync("Authentication");
            MainPage = new AppShell();

            if (!string.IsNullOrEmpty(authenticationData))
            {
                await Shell.Current.GoToAsync(nameof(HomePage));
            }
        }
    }
}
