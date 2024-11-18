using inventory_mobile_app.ViewModels;

namespace inventory_mobile_app.Pages;

public partial class Scan : ContentPage
{
    private bool isScanning = true;

    public Scan(ScannerViewModel scannerViewModel)
	{
        InitializeComponent();
        BindingContext = scannerViewModel;

        RequestCameraPermission();

        barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
        {
            TryHarder = true,
            AutoRotate = true,
            Formats = ZXing.Net.Maui.BarcodeFormat.Code128,
        };

        barcodeReader.BarcodesDetected += BarcodeReader_BarcodesDetected;
    }

    private async void RequestCameraPermission()
    {
        var status = await Permissions.RequestAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            await DisplayAlert("Camera Permission", "Camera permission is required to scan QR codes.", "OK");
            return;
        }
    }

    private void BarcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {

        if (!isScanning || e.Results == null)
            return;

        isScanning = false;

        var firstResult = e.Results[0].Value;

        Dispatcher.DispatchAsync(async () =>
        {

            if (BindingContext is ScannerViewModel viewModel)
            {
                await viewModel.ScannerViewProduct(firstResult);
            }

            await Task.Delay(2000);
            isScanning = true;
        });
    }
}