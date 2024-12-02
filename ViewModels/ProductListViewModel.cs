using CommunityToolkit.Mvvm.ComponentModel;
using inventory_mobile_app.Models;
using inventory_mobile_app.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace inventory_mobile_app.ViewModels
{
    public partial class ProductListViewModel : ObservableObject
    {
        private readonly ClientService clientService;

        // ObservableCollection to store products
        public ObservableCollection<ProductList> Products { get; set; } = [];
        public ICommand ViewProductCommand { get; }

        public ProductListViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            ViewProductCommand = new Command<ProductList>(OnViewProduct);
            Initialize();
        }

        private async void OnViewProduct(ProductList product)
        {
            if (product != null)
            {
                // Navigate to the product detail page, passing the product
                await Shell.Current.GoToAsync("ProductViewPage", true,
                    new Dictionary<string, object> { { "Product", product } });
            }
        }

        // Initialize async method
        private async void Initialize()
        {
            await LoadProductList();
        }

        public async Task LoadProductList()
        {
            try
            {
                // Fetch the product list from the service
                var response = await clientService.GetProductListsAsync();

                // Clear existing products
                Products.Clear();

                if (response != null && response.Any())
                {
                    // Add the products to the ObservableCollection
                    foreach (var product in response)
                    {
                        Products.Add(product);
                    }
                }
                else
                {
                    // Provide a detailed message for debugging
                    string errorMessage = response == null ? "The response is null." : "The response contains no products.";
                    await Shell.Current.DisplayAlert("Error", $"An error occurred while fetching products: {errorMessage}", "OK");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during data fetching
                await Shell.Current.DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            }
        }
    }
}
