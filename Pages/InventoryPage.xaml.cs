namespace inventory_mobile_app.Pages;
using inventory_mobile_app.ViewModels;
using static inventory_mobile_app.ViewModels.MainPageViewModel;
using Microsoft.Maui.Controls;

public partial class InventoryPage : ContentPage
{
    public int Quantity { get; set; } = 0;
    public InventoryPage()
	{
		InitializeComponent();
        BindingContext = new MainViewModel();
        ExpiryDatePicker.IsVisible = false;
    }

    void OnAddProductClicked(object sender, EventArgs e)
    {
        AddProductModal.IsVisible = true;
    }

    private void OnCloseModalClicked(object sender, EventArgs e)
    {
        AddProductModal.IsVisible = false;
    }

    void OnEditProductClicked(object sender, EventArgs e)
    {
        EditProductModal.IsVisible = true;
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
        }
        else
        {
            DisplayAlert("Error", "Please fill in all fields", "OK");
        }
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
            viewModel.IsInventorySelected = true;
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
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = true;
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
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = true;
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
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = true;
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
            viewModel.IsHomeSelected = false;
            viewModel.IsInventorySelected = true;
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