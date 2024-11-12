namespace inventory_mobile_app.Pages;
using inventory_mobile_app.ViewModels;
using static inventory_mobile_app.ViewModels.MainPageViewModel;
using Microsoft.Maui.Graphics;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        BindingContext = App.MainViewModel;
        AvailabilityChart.Drawable = new AvailabilityChartDrawable();
    }

   public class AvailabilityChartDrawable : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        float centerX = dirtyRect.Width / 2;
        float centerY = dirtyRect.Height / 2;
        float radius = Math.Min(centerX, centerY) * 0.8f;

        // Sample data: adjust as needed
        float total = 67f;
        float inStockAngle = (60f / total) * 360f;
        float lowStockAngle = (5f / total) * 360f;
        float outOfStockAngle = (2f / total) * 360f;

        float currentAngle = 0;

        // In-stock segment (Yellow)
        canvas.StrokeColor = Colors.Transparent;
        canvas.FillColor = Color.FromArgb("#FFD700");
        canvas.FillArc(centerX - radius, centerY - radius, radius * 2, radius * 2,
                       currentAngle, inStockAngle, true);
        currentAngle += inStockAngle;

        // Low stock segment (Orange)
        canvas.FillColor = Color.FromArgb("#FF6B35");
        canvas.FillArc(centerX - radius, centerY - radius, radius * 2, radius * 2,
                       currentAngle, lowStockAngle, true);
        currentAngle += lowStockAngle;

        // Out of stock segment (Red)
        canvas.FillColor = Colors.Red;
        canvas.FillArc(centerX - radius, centerY - radius, radius * 2, radius * 2,
                       currentAngle, outOfStockAngle, true);

        // Inner circle for donut effect
        canvas.FillColor = Colors.White;
        float innerRadius = radius * 0.6f;
        canvas.FillCircle(centerX, centerY, innerRadius);
    }
}


    // Home/Dashboard page
    private async void OnHomeClicked(object sender, EventArgs e)
    {
        try
        {
            var viewModel = BindingContext as MainViewModel;
            viewModel.IsHomeSelected = true;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = false;
            viewModel.IsSalesSelected = false;
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
            viewModel.IsHomeSelected = true;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = false;
            viewModel.IsSalesSelected = false;
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
            viewModel.IsHomeSelected = true;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = false;
            viewModel.IsSalesSelected = false;
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
            viewModel.IsHomeSelected = true;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = false;
            viewModel.IsSalesSelected = false;
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
            viewModel.IsHomeSelected = true;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = false;
            viewModel.IsSalesSelected = false;
            viewModel.IsSettingsSelected = false;

            await Shell.Current.GoToAsync("//SettingsPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}