namespace inventory_mobile_app.Pages;
using inventory_mobile_app.ViewModels;
using static inventory_mobile_app.ViewModels.MainPageViewModel;

public partial class SalesPage : ContentPage
{
	public SalesPage()
	{
		InitializeComponent();
        BindingContext = new MainViewModel();
    }

    // Home/Dashboard page
    private async void OnHomeClicked(object sender, EventArgs e)
    {
        try
        {
            var viewModel = BindingContext as MainViewModel;
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = false;
            viewModel.IsSalesSelected = true;
            viewModel.IsSettingsSelected = false;

            await Shell.Current.GoToAsync("//HomePage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    // Inventory page
    private async void OnInventoryClicked(object sender, EventArgs e)
    {
        try
        {
            var viewModel = BindingContext as MainViewModel;
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = false;
            viewModel.IsSalesSelected = true;
            viewModel.IsSettingsSelected = false;

            await Shell.Current.GoToAsync("//InventoryPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    // Scan page
    private async void OnScanClicked(object sender, EventArgs e)
    {
        try
        {
            var viewModel = BindingContext as MainViewModel;
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = false;
            viewModel.IsSalesSelected = true;
            viewModel.IsSettingsSelected = false;

            await Shell.Current.GoToAsync("//ScanPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    // Sales page
    private async void OnSalesClicked(object sender, EventArgs e)
    {
        try
        {
            var viewModel = BindingContext as MainViewModel;
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = false;
            viewModel.IsSalesSelected = true;
            viewModel.IsSettingsSelected = false;

            await Shell.Current.GoToAsync("//SalesPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }



    // Settings page
    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        try
        {
            var viewModel = BindingContext as MainViewModel;
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = false;
            viewModel.IsSalesSelected = true;
            viewModel.IsSettingsSelected = false;

            await Shell.Current.GoToAsync("//SettingsPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}