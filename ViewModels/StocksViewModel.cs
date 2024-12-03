using CommunityToolkit.Mvvm.ComponentModel;
using inventory_mobile_app.Models;
using inventory_mobile_app.Services;
using System.Collections.ObjectModel;

namespace inventory_mobile_app.ViewModels
{
    public partial class StocksViewModel: ObservableObject
    {
        private readonly ClientService clientService;

        public ObservableCollection<ProductList> OutOfStockProducts { get; set; } = [];
        public ObservableCollection<ProductList> LowStockProducts { get; set; } = [];
        public ObservableCollection<ProductList> Products { get; set; } = [];

        public StocksViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            Initialize();
        }

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

                // Clear existing products in both collections
                OutOfStockProducts.Clear();
                LowStockProducts.Clear();
                Products.Clear();

                if (response != null && response.Any())
                {
                    // Separate products into "Out of Stock" and "Low Stock" categories
                    foreach (var product in response)
                    {
                        Products.Add(product);

                        if (product.Stock == 0)
                        {
                            OutOfStockProducts.Add(product);
                        }
                        else if (product.Stock > 0 && product.Stock <= 5)
                        {
                            LowStockProducts.Add(product);
                        }
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
