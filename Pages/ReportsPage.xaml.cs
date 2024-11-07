namespace inventory_mobile_app.Pages;

public partial class ReportsPage : ContentPage
{
	public ReportsPage()
	{
		InitializeComponent();
	}

    // Home/Dashboard page
    private async void OnHomeClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync("//HomePage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    // Reports page
    private async void OnReportsPageClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync("//ReportsPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    // Scan page
    private async void OnScanClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//ScanPage");
    }

    // Inventory page
    private async void OnInventoryClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//InventoryPage");
    }

    // Settings page
    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//SettingsPage");
    }
}
