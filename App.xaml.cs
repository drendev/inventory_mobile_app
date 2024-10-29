namespace inventory_mobile_app
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SplashScreenPage();
        }

        protected override async void OnStart()
        {
            await Task.Delay(3000);

            MainPage = new NavigationPage(new MainPage());
        }
    }
}
