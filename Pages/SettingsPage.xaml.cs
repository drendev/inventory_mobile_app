namespace inventory_mobile_app.Pages;
using inventory_mobile_app.ViewModels;
using System.Diagnostics;
using static inventory_mobile_app.ViewModels.MainPageViewModel;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }
}