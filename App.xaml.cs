using inventory_mobile_app.Pages;
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

            MainPage = new AppShell();
        }
    }
}
