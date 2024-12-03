using CommunityToolkit.Mvvm.ComponentModel;
using inventory_mobile_app.Models;
using inventory_mobile_app.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace inventory_mobile_app.ViewModels
{
    public partial class ProductListViewModel : ObservableObject
    {
        private readonly ClientService clientService;

        // ObservableCollection to store all products
        public ObservableCollection<ProductList> Products { get; set; } = new();

        // Filtered products based on search input
        [ObservableProperty]
        private ObservableCollection<ProductList> filteredProducts = new();

        // Search text
        [ObservableProperty]
        private string searchText;

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
                await Shell.Current.GoToAsync("ProductViewPage", true,
                    new Dictionary<string, object> { { "Product", product } });
            }
        }

        // Initialize and load products
        private async void Initialize()
        {
            await LoadProductList();
        }

        public async Task LoadProductList()
        {
            try
            {
                var response = await clientService.GetProductListsAsync();
                Products.Clear();

                if (response != null && response.Any())
                {
                    foreach (var product in response)
                    {
                        Products.Add(product);
                    }

                    // Initialize FilteredProducts
                    FilterProducts();
                }
                else
                {
                    string errorMessage = response == null ? "The response is null." : "The response contains no products.";
                    await Shell.Current.DisplayAlert("Error", $"An error occurred while fetching products: {errorMessage}", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            }
        }

        partial void OnSearchTextChanged(string value)
        {
            FilterProducts();
        }

        private void FilterProducts()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                // If no search text, show all products
                FilteredProducts = new ObservableCollection<ProductList>(Products);
            }
            else
            {
                // Apply filtering based on product name
                FilteredProducts = new ObservableCollection<ProductList>(
                    Products.Where(p => p.ProductName.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                );
            }
        }
    }
}
