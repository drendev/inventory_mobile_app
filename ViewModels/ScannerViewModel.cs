using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using inventory_mobile_app.Models;
using inventory_mobile_app.Services;

namespace inventory_mobile_app.ViewModels
{
    public partial class ScannerViewModel : ObservableObject
    {
        private readonly ClientService clientService;

        [ObservableProperty]
        private StockModel stockModel;

        [ObservableProperty]
        private ProductList productList = new ProductList();

        [ObservableProperty]
        private string productId;

        public ScannerViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            StockModel = new();
            Initialize();
        }

        private async void Initialize()
        {
            await LoadProduct(ProductId);
        }

        private async Task LoadProduct(string productId)
        {
            if (!string.IsNullOrWhiteSpace(productId))
            {
                var response = await clientService.GetProductAsync(productId);
                if (response != null)
                {
                    ProductList = response;
                }
            }
        }

        public async Task ScannerViewProduct(string scannedProductId)
        {
            ProductId = scannedProductId;

            var response = await clientService.GetProductAsync(ProductId);

            if (response == null)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Product Not Found", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alert", $"Found Product: {response.ProductName}", "OK");
                ProductList = response;

                StockModel.ProductId = ProductId;
            }
        }

        [RelayCommand]

        private async Task AddStock()
        {
            bool stockAdded = await clientService.AddStock(StockModel);

            if (!stockAdded)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to add stock", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Stock added successfully", "OK");
            }
        }
    }
}
