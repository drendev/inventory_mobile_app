namespace inventory_mobile_app.Pages;
using inventory_mobile_app.ViewModels;
using static inventory_mobile_app.ViewModels.MainPageViewModel;
using Microsoft.Maui.Controls;

public partial class InventoryPage : ContentPage
{
    public int Quantity { get; set; } = 0;
    public InventoryPage(ProductListViewModel productListViewModel)
	{
		InitializeComponent();
        BindingContext = productListViewModel;
       
    }

    void OnEditProductClicked(object sender, EventArgs e)
    {
        EditProductModal.IsVisible = true;
        EditProductButton.IsEnabled = true;
    }

    private void OnCloseEditModalClicked(object sender, EventArgs e)
    {
        EditProductModal.IsVisible = false;
    }

    void OnViewProductClicked(object sender, EventArgs e)
    {
        ViewProductModal.IsVisible = true;
    }

    private void OnCloseViewModalClicked(object sender, EventArgs e)
    {
        ViewProductModal.IsVisible = false;
    }

    void OnRemoveProductClicked(object sender, EventArgs e)
    {
        RemoveProductModal.IsVisible = true;
        EditProductModal.IsVisible = false;
        EditProductButton.IsEnabled = false;
    }

    private void OnCloseDeleteModalClicked(object sender, EventArgs e)
    {
        RemoveProductModal.IsVisible = false;
        EditProductButton.IsEnabled = true;
    }

    void OnYesRemoveClicked(object sender, EventArgs e)
    {
        RemoveProductSuccessfullyModal.IsVisible = true;
    }

    private void OnCancelRemoveModalClicked(object sender, EventArgs e)
    {
        RemoveProductSuccessfullyModal.IsVisible = false;
        RemoveProductModal.IsVisible = false; 
        EditProductButton.IsEnabled = true;
    }

    private void OnCloseSuccessRemoveModalClicked(object sender, EventArgs e)
    {
        RemoveProductSuccessfullyModal.IsVisible = false;
        RemoveProductModal.IsVisible = false; 
        EditProductButton.IsEnabled = true;
    }

   

    void OnSaveEditProductClicked(object sender, EventArgs e)
    {
        var editedProductName = EditedProductNameEntry.Text;
        var editedProductDescription = EditedProductDescriptionEntry.Text;
        var editedBuyingPrice = EditedBuyingPriceEntry.Text;
        var editedSellingPrice = EditedSellingPriceEntry.Text;
        //var editedExpiryDate = EditedExpiryDatePicker.Date;
        var editedProductQuantity = EditedProductQuantityEntry.Text;

        if (!string.IsNullOrWhiteSpace(editedProductName) &&
            !string.IsNullOrWhiteSpace(editedProductDescription) &&
            !string.IsNullOrWhiteSpace(editedBuyingPrice) &&
            !string.IsNullOrWhiteSpace(editedSellingPrice) &&
            //editedExpiryDate != DateTime.MinValue &&
            !string.IsNullOrWhiteSpace(editedProductQuantity))
        {
            EditProductModal.IsVisible = false;
        }
        else
        {
            DisplayAlert("Error", "Please fill in all fields", "OK");
        }
    }


        // Home/Dashboard page
        private async void OnHomeClicked(object sender, EventArgs e)
    {
        try
        {
            var viewModel = BindingContext as MainViewModel;
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = true;
            viewModel.IsScanSelected = false;
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
            viewModel.IsInventorySelected = true;
            viewModel.IsScanSelected = false;
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
            viewModel.IsInventorySelected = true;
            viewModel.IsScanSelected = false;
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
            viewModel.IsInventorySelected = true;
            viewModel.IsScanSelected = false;
            viewModel.IsSettingsSelected = false;

            await Shell.Current.GoToAsync("//SettingsPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}