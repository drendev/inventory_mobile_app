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

            MainPage = new AppShell();
        }
    }
}
