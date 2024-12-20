﻿using inventory_mobile_app.Pages;
using inventory_mobile_app.Pages.Auth;

namespace inventory_mobile_app
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(Category), typeof(Category));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute("scanpage", typeof(ScanPage));
            Routing.RegisterRoute(nameof(InventoryPage), typeof(InventoryPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(HistoryPage), typeof(HistoryPage));
            Routing.RegisterRoute(nameof(Inventory), typeof(Inventory));
            Routing.RegisterRoute(nameof(BottomNavBar), typeof(BottomNavBar));
            Routing.RegisterRoute(nameof(ProductViewPage), typeof(ProductViewPage));

        }
    }
}
