namespace inventory_mobile_app.Pages;
using inventory_mobile_app.ViewModels;
using ZXing;
using static inventory_mobile_app.ViewModels.MainPageViewModel;

public partial class ScanPage : ContentPage
{
    public ScanPage(ScannerViewModel scannerViewModel)
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
        ExpiryDatePicker.IsVisible = false;


        RequestCameraPermission();

        barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
        {
            TryHarder = true,
            AutoRotate = true,
            Formats = ZXing.Net.Maui.BarcodeFormat.Code128,
        };
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

    void OnScanProductClicked(object sender, EventArgs e)
    {
        ScanProductModal.IsVisible = true;
        ScanButton.IsEnabled = false;
    }

    private void OnCloseScanProductClicked(object sender, EventArgs e)
    {
        ScanProductModal.IsVisible = false;
        ScanButton.IsEnabled = true;
    }

    void OnScanNotExistClicked(object sender, EventArgs e)
    {
        ScanNotExistModal.IsVisible = true;
        ScanButton.IsEnabled = false; 
    }

    private void OnCloseScanNotExistClicked(object sender, EventArgs e)
    {
        ScanNotExistModal.IsVisible = false;
        AddedProductSuccessfullyModal.IsVisible = false;
        ScanButton.IsEnabled = true; 
    }

    private void OnAddStockClicked(object sender, EventArgs e)
    {
        AddStockModal.IsVisible = true;
        SoldStockModal.IsVisible = false;
        ScanProductModal.IsVisible = false;
        ScanButton.IsEnabled = false; 
    }

    private void OnSoldStockClicked(object sender, EventArgs e)
    {
        SoldStockModal.IsVisible = true;
        AddStockModal.IsVisible = false;
        ScanProductModal.IsVisible = false;
        ScanButton.IsEnabled = false; 
    }

    private void OnCloseAddStockModalClicked(object sender, EventArgs e)
    {
        AddStockModal.IsVisible = false;
        ScanButton.IsEnabled = true;
    }

        private void OnCloseSoldStockModalClicked(object sender, EventArgs e)
    {
        SoldStockModal.IsVisible = false;
        ScanButton.IsEnabled = true; 
    }

    void OnAddProductClicked(object sender, EventArgs e)
    {
        AddProductModal.IsVisible = true;
        ScanNotExistModal.IsVisible = false;
        ScanProductModal.IsVisible = false;
        AddedProductSuccessfullyModal.IsVisible = false;
        ScanButton.IsEnabled = false; 
    }

    private void OnCloseModalClicked(object sender, EventArgs e)
    {
        AddProductModal.IsVisible = false;
        AddedProductSuccessfullyModal.IsVisible = false;
        ScanButton.IsEnabled = true; 
    }

    private void OnCloseSuccessAddedClicked(object sender, EventArgs e)
    {
        AddedProductSuccessfullyModal.IsVisible = false;
        ScanButton.IsEnabled = true; 
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
}