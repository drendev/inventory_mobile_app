namespace inventory_mobile_app.Pages;
using inventory_mobile_app.ViewModels;
using ZXing;
using static inventory_mobile_app.ViewModels.MainPageViewModel;

public partial class ScanPage : ContentPage
{
    public ScanPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();


        RequestCameraPermission();

        barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
        {
            TryHarder = true,
            AutoRotate = true,
            Formats = ZXing.Net.Maui.BarcodeFormat.Code128,
        };
    }

    void OnScanProductClicked(object sender, EventArgs e)
    {
        ScanProductModal.IsVisible = true;
    }

    private void OnCloseScanProductClicked(object sender, EventArgs e)
    {
        ScanProductModal.IsVisible = false;
    }

    // Home/Dashboard page
    private async void OnHomeClicked(object sender, EventArgs e)
    {
        try
        {
            var viewModel = BindingContext as MainViewModel;
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = true;
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
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = true;
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
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = false;
            viewModel.IsScanSelected = true;
            viewModel.IsSalesSelected = false;
            viewModel.IsSettingsSelected = false;

            await Shell.Current.GoToAsync("//ScanPage");
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
            viewModel.IsScanSelected = true;
            viewModel.IsSalesSelected = false;
            viewModel.IsSettingsSelected = false;

            await Shell.Current.GoToAsync("//SettingsPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    // Camera permission
    private async void RequestCameraPermission()
    {
        var status = await Permissions.RequestAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            await DisplayAlert("Camera Permission", "Camera permission is required to scan QR codes.", "OK");
            return;
        }
    }

    // Barcode reader {Not fully functional}

    private void BarcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {

        var first = e.Results[0];

        Dispatcher.DispatchAsync(async () =>
        {
            await DisplayAlert("Barcode Result", first.Value, "OK");
        }
        );
    }
}