using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using inventory_mobile_app.ViewModels;
using System.Threading;

namespace inventory_mobile_app.Pages;

public partial class ProductViewPage : ContentPage
{
    private readonly ProductViewModel _viewModel;

    public ProductViewPage(ProductViewModel productViewModel)
	{
		InitializeComponent();
        _viewModel = productViewModel;
        BindingContext = productViewModel;
    }


    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is ProductViewModel viewModel)
        {
            viewModel.Product = null;
            BindingContext = null;
            viewModel.UpdateProduct = null;
        }
    }

    void OnEditProductClicked(object sender, EventArgs e)
    {
        EditProductModal.IsVisible = true;
        EditProductButton.IsEnabled = true;
        ViewProductModal.IsVisible = false;
    }

    private void OnCloseEditModalClicked(object sender, EventArgs e)
    {
        EditProductModal.IsVisible = false;
    }


    private async void OnCloseIconTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//InventoryPage");
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
                    string imageUrl = uploadResult.SecureUrl?.ToString() ?? uploadResult.Url.ToString();

                    if (BindingContext is ProductViewModel viewModel)
                    {
                        viewModel.UpdateProduct.ImageUrl = imageUrl;
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Image not valid please try again.", "OK");
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
}