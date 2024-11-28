namespace inventory_mobile_app.Pages;
using CloudinaryDotNet.Actions;

using CloudinaryDotNet;
using inventory_mobile_app.ViewModels;
using System.Diagnostics;
using ZXing;
using ZXing.Net.Maui;
using static inventory_mobile_app.ViewModels.MainPageViewModel;
using inventory_mobile_app.Models;

public partial class ScanPage : ContentPage
{
    private bool isScanning = true;

    public ScanPage(ScannerViewModel scannerViewModel)
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
        if (BindingContext is ScannerViewModel viewModel)
        {
            viewModel.ResetProperties();
        }
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

    private async void OnLabelTapped(object sender, EventArgs e)
    {
        var selectedImage = await PickImageAsync();

        if (selectedImage != null)
        {
            try
            {
                // Display the selected image locally
                PlaceholderImage.Source = ImageSource.FromFile(selectedImage.FullPath);

                // Convert the selected image to a stream
                using var stream = await selectedImage.OpenReadAsync();

                // Upload the image to Cloudinary
                var uploadResult = await UploadImageToCloudinary(stream);

                if (uploadResult != null)
                {
                    // Display a success message and optionally update your model with the URL
                    await DisplayAlert("Success", "Image uploaded successfully", "OK");
                    string imageUrl = uploadResult.SecureUrl?.ToString() ?? uploadResult.Url.ToString();

                    if (BindingContext is ScannerViewModel viewModel)
                    {
                        viewModel.Product.ImageUrl = imageUrl;
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Failed to upload image", "OK");
                }
            }
            catch (Exception ex)
            {
                // Handle errors during image selection or upload
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
        else
        {
            // Handle case where no image was selected
            await DisplayAlert("Error", "No image selected or an error occurred.", "OK");
        }
    }

    private async Task<ImageUploadResult> UploadImageToCloudinary(Stream imageStream)
    {
        try
        {
            // Initialize Cloudinary account details
            var cloudinary = new Cloudinary(new Account("djz4hdunq", "171682539679361", "yli7gOBkqWJNJYISmw-X2jWxUhk"))
            {
                Api = { Secure = true }
            };

            // Define upload parameters
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription("uploaded_image", imageStream),
                PublicId = $"products/{Guid.NewGuid()}", // Optional: Unique public ID
                UseFilename = true,
                Overwrite = false
            };

            // Upload the image
            var uploadResult = await Task.Run(() => cloudinary.Upload(uploadParams));

            return uploadResult;
        }
        catch (Exception ex)
        {
            // Log or display error details
            Console.WriteLine($"Error uploading image: {ex.Message}");
            return null;
        }
    }

    private async Task<FileResult> PickImageAsync()
    {
        try
        {
            // Ensure the device supports media picking
            if (!MediaPicker.IsCaptureSupported)
            {
                await DisplayAlert("Error", "Image picking is not supported on this device.", "OK");
                return null;
            }

            // Request permission if needed
            var status = await Permissions.RequestAsync<Permissions.Photos>();
            if (status != PermissionStatus.Granted)
            {
                await DisplayAlert("Permission Denied", "Permission to access photos is required.", "OK");
                return null;
            }

            // Let the user pick a photo
            var photo = await MediaPicker.PickPhotoAsync();

            // Return the selected file or handle cancellation
            return photo;
        }
        catch (FeatureNotSupportedException)
        {
            await DisplayAlert("Error", "Image picking is not supported on this device.", "OK");
            return null;
        }
        catch (PermissionException)
        {
            await DisplayAlert("Permission Denied", "You have not granted permission to pick images.", "OK");
            return null;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            return null;
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