namespace inventory_mobile_app.Pages;
using inventory_mobile_app.ViewModels;
using System.Diagnostics;
using ZXing;
using ZXing.Net.Maui;
using static inventory_mobile_app.ViewModels.MainPageViewModel;

public partial class ScanPage : ContentPage
{
    private bool isScanning = true;

    public ScanPage(ScannerViewModel scannerViewModel)
    {
        InitializeComponent();
        BindingContext = scannerViewModel;

        ExpiryDatePicker.IsVisible = false;

        RequestCameraPermission();

        barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
        {
            TryHarder = true,
            AutoRotate = true,
            Formats = ZXing.Net.Maui.BarcodeFormat.Code128,
        };
        barcodeReader.BarcodesDetected += BarcodeReader_BarcodesDetected;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Debug.WriteLine("CameraPage OnAppearing");
        InitializeCamera();
    }

    private void InitializeCamera()
    {
        barcodeReader.IsVisible = true;
        barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
        {
            TryHarder = true,
            AutoRotate = true,
            Formats = ZXing.Net.Maui.BarcodeFormat.Code128,
        };

        barcodeReader.BarcodesDetected += BarcodeReader_BarcodesDetected;
        barcodeReader.CameraLocation = CameraLocation.Front;
        barcodeReader.CameraLocation = CameraLocation.Rear;
        barcodeReader.IsEnabled = true;
        barcodeReader.IsDetecting = true;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Debug.WriteLine("CameraPage OnDisappearing");
        DeinitializeCamera();
    }

    private void DeinitializeCamera()
    {
        barcodeReader.IsEnabled = false;
        barcodeReader.IsDetecting = false;
        barcodeReader.IsVisible = false;
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



    void OnScanProductClicked(object sender, EventArgs e)
    {
        ScanProductModal.IsVisible = true;
    }

    private void OnCloseScanProductClicked(object sender, EventArgs e)
    {
        if (BindingContext is ScannerViewModel viewModel)
        {
            viewModel.ResetProperties();
        }
    }

    void OnScanNotExistClicked(object sender, EventArgs e)
    {
        ScanNotExistModal.IsVisible = true;
    }

    private void OnCloseScanNotExistClicked(object sender, EventArgs e)
    {
        ScanNotExistModal.IsVisible = false;
        AddedProductSuccessfullyModal.IsVisible = false;
    }

    private void OnAddStockClicked(object sender, EventArgs e)
    {
        AddStockModal.IsVisible = true;
        SoldStockModal.IsVisible = false;
        ScanProductModal.IsVisible = false;
    }

    private void OnSoldStockClicked(object sender, EventArgs e)
    {
        SoldStockModal.IsVisible = true;
        AddStockModal.IsVisible = false;
        ScanProductModal.IsVisible = false;
    }

    private void OnCloseAddStockModalClicked(object sender, EventArgs e)
    {
        if (BindingContext is ScannerViewModel viewModel)
        {
            viewModel.ResetProperties();
        }
    }

    private void OnCloseSoldStockModalClicked(object sender, EventArgs e)
    {
        if (BindingContext is ScannerViewModel viewModel)
        {
            viewModel.ResetProperties();
        }
    }

    void OnAddProductClicked(object sender, EventArgs e)
    {
        AddProductModal.IsVisible = true;
        ScanNotExistModal.IsVisible = false;
        ScanProductModal.IsVisible = false;
        AddedProductSuccessfullyModal.IsVisible = false;
    }

    private void OnCloseModalClicked(object sender, EventArgs e)
    {
        AddProductModal.IsVisible = false;
        AddedProductSuccessfullyModal.IsVisible = false;
    }

    private void OnCloseSuccessAddedClicked(object sender, EventArgs e)
    {
        AddedProductSuccessfullyModal.IsVisible = false;
    }

    void OnSaveProductClicked(object sender, EventArgs e)
    {
        var productName = ProductNameEntry.Text;
        var productDescription = ProductDescriptionEntry.Text;
        var buyingPrice = BuyingPriceEntry.Text;
        var sellingPrice = SellingPriceEntry.Text;
        var expiryDate = ExpiryDatePicker.Date;
        var productQuantity = ProductQuantityEntry.Text;

        if (!string.IsNullOrWhiteSpace(productName) &&
            !string.IsNullOrWhiteSpace(productDescription) &&
            !string.IsNullOrWhiteSpace(buyingPrice) &&
            !string.IsNullOrWhiteSpace(sellingPrice) &&
            expiryDate != DateTime.MinValue &&
            !string.IsNullOrWhiteSpace(productQuantity))
        {
            AddProductModal.IsVisible = false;
            AddedProductSuccessfullyModal.IsVisible = true;
        }
        else
        {
            DisplayAlert("Error", "Please fill in all fields", "OK");
        }
    }

    private void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        if (e.NewDate != DateTime.MinValue)
        {
            ExpiryDatePicker.IsVisible = true;

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
}