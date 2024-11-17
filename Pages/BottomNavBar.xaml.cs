namespace inventory_mobile_app.Pages;
using inventory_mobile_app.ViewModels;
using inventory_mobile_app.Models;
using System.ComponentModel;

public partial class BottomNavBar : ContentView
{
    private readonly BottomNavBarViewModel bottomNavBarViewModel;

    public BottomNavBar()
    {
        InitializeComponent();
        bottomNavBarViewModel = new BottomNavBarViewModel();
        BindingContext = bottomNavBarViewModel;
    }

    // Home/Dashboard page
    private async void OnHomeClicked(object sender, EventArgs e)
    {
        try
        {
            var viewModel = (BottomNavBarViewModel)BindingContext;
            viewModel.IsHomeSelected = true;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = false;
            viewModel.IsHistorySelected = false;
            viewModel.IsSettingsSelected = false;

            await Shell.Current.GoToAsync("//HomePage");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    // Inventory page
    private async void OnInventoryClicked(object sender, EventArgs e)
    {
        try
        {
            var viewModel = (BottomNavBarViewModel)BindingContext;
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = true;
            viewModel.IsScanSelected = false;
            viewModel.IsHistorySelected = false;
            viewModel.IsSettingsSelected = false;

            await Shell.Current.GoToAsync("//InventoryPage");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    // Scan page
    private async void OnScanClicked(object sender, EventArgs e)
    {
        try
        {
            var viewModel = (BottomNavBarViewModel)BindingContext;
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = true;
            viewModel.IsHistorySelected = false;
            viewModel.IsSettingsSelected = false;


            await Shell.Current.GoToAsync("//ScanPage");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    // History page
    private async void OnHistoryClicked(object sender, EventArgs e)
    {
        try
        {
            var viewModel = (BottomNavBarViewModel)BindingContext;
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = false;
            viewModel.IsHistorySelected = true;
            viewModel.IsSettingsSelected = false;


            await Shell.Current.GoToAsync("//HistoryPage");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    // Settings page
    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        try
        {
            var viewModel = (BottomNavBarViewModel)BindingContext;
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = false;
            viewModel.IsHistorySelected = false;
            viewModel.IsSettingsSelected = true;


            await Shell.Current.GoToAsync("//SettingsPage");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }
}