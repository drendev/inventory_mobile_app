using inventory_mobile_app.Pages;

namespace inventory_mobile_app
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SplashScreenPage();

            StartApp();
        }

        private async void StartApp()
        {
            await Task.Delay(2000);
 
            var authenticationData = await SecureStorage.Default.GetAsync("Authentication");

            if (!string.IsNullOrEmpty(authenticationData))
            {
                MainPage = new AppShell();
                await Shell.Current.GoToAsync(nameof(Category));
            }
            else
            {
                MainPage = new AppShell();
            }
        }
    }
}
