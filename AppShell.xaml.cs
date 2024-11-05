using inventory_mobile_app.Pages;
using inventory_mobile_app.Pages.Auth;

namespace inventory_mobile_app
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Category), typeof(Category));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(ReportsPage), typeof(ReportsPage));

        }
    }
}
