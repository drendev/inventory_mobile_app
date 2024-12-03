using CommunityToolkit.Mvvm.ComponentModel;
using inventory_mobile_app.Models;
using inventory_mobile_app.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace inventory_mobile_app.ViewModels
{
    public partial class StocksViewModel : ObservableObject
    {
        private readonly ClientService clientService;

        private System.Timers.Timer timer;

        [ObservableProperty]
        private string currentDate;

        [ObservableProperty]
        private string currentTime;

        [ObservableProperty]
        private string searchText; // Bind this to the SearchBar Text

        public ObservableCollection<ProductList> OutOfStockProducts { get; set; } = new();
        public ObservableCollection<ProductList> LowStockProducts { get; set; } = new();
        public ObservableCollection<ProductList> Products { get; set; } = new();
        public ObservableCollection<ProductList> FilteredProducts { get; set; } = new();

        public StocksViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            UpdateDateTime();
            StartTimer();
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

                // Clear existing products in all collections
                OutOfStockProducts.Clear();
                LowStockProducts.Clear();
                Products.Clear();
                FilteredProducts.Clear();

                if (response != null && response.Any())
                {
                    foreach (var product in response)
                    {
                        Products.Add(product);
                    }

                    ApplySearch(); // Apply search after loading products
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

        private void ApplySearch()
        {
            // Filter the main Products collection based on the SearchText
            var filteredProducts = string.IsNullOrWhiteSpace(SearchText)
                ? Products
                : new ObservableCollection<ProductList>(
                    Products.Where(p => p.ProductName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                        p.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));

            // Update FilteredProducts for "All Items"
            FilteredProducts.Clear();
            foreach (var product in filteredProducts)
            {
                FilteredProducts.Add(product);
            }

            // Update OutOfStockProducts and LowStockProducts based on filtered results
            OutOfStockProducts.Clear();
            LowStockProducts.Clear();

            foreach (var product in filteredProducts)
            {
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

        partial void OnSearchTextChanged(string value)
        {
            ApplySearch(); // Update the filtered collections whenever SearchText changes
        }

        private void UpdateDateTime()
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Manila");
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);

            CurrentDate = now.ToString("MM/dd/yyyy");
            CurrentTime = now.ToString("hh:mm tt");
        }

        private void StartTimer()
        {
            timer = new System.Timers.Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += (s, e) =>
            {
                MainThread.BeginInvokeOnMainThread(() => UpdateDateTime());
            };
            timer.Start();
        }

        public void StopTimer()
        {
            timer?.Stop();
            timer?.Dispose();
        }
    }
}
