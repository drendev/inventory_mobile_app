using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using inventory_mobile_app.Models;
using inventory_mobile_app.Services;

namespace inventory_mobile_app.ViewModels
{
    public partial class ScannerViewModel : ObservableObject
    {
        private bool _isAddStock;
        public bool IsAddStock
        {
            get => _isAddStock;
            set
            {
                SetProperty(ref _isAddStock, value);
            }
        }

        private bool _isSoldStock;
        public bool IsSoldStock
        {
            get => _isSoldStock;
            set
            {
                SetProperty(ref _isSoldStock, value);
            }
        }

        private bool _isProduct;
        public bool IsProduct
        {
            get => _isProduct;
            set
            {
                SetProperty(ref _isProduct, value);
            }
        }

        private readonly ClientService clientService;

        [ObservableProperty]
        private StockModel stockModel;

        [ObservableProperty]
        private int? addedStockQuantity;

        [ObservableProperty]
        private int? soldStockQuantity;

        [ObservableProperty]
        private Product product = new Product();

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
                    Product = response;
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
                Product = response;
                IsProduct = true;
                StockModel.ProductId = ProductId;
                
            }
        }

        [RelayCommand]

        private async Task AddStock()
        {
            bool stockAdded = await clientService.AddStock(StockModel);

            if (StockModel.Stock <= 0 || StockModel.Stock == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid Quantity", "Close");
            }
            else if (stockAdded)
            {
                await LoadProduct(StockModel.ProductId);
                AddedStockQuantity = StockModel.Stock;
                IsAddStock = true;
                IsProduct = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to add stock", "OK");
            }
        }

        [RelayCommand]

        private async Task SoldStock()
        {
            bool stockSold = await clientService.SoldStock(StockModel);

            if (StockModel.Stock <= 0 || StockModel.Stock == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid Quantity", "Close");
            }
            else if (stockSold)
            {
                await LoadProduct(StockModel.ProductId);
                SoldStockQuantity = StockModel.Stock;
                IsSoldStock = true;
                IsProduct = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to sell stock", "OK");
            }
        }

        public void ResetProperties()
        {
            IsAddStock = false;
            IsSoldStock = false;
            IsProduct = false;
            AddedStockQuantity = 0; 
            StockModel = new StockModel();
            ProductId = string.Empty;
            Product = new Product();
        }
    }
}
