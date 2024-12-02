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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Refresh the product list each time the page appears
        if (BindingContext is ProductListViewModel viewModel)
        {
            viewModel.LoadProductList();  // This will reload the products from the service
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
        ViewProductModal.IsVisible = false;
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
}