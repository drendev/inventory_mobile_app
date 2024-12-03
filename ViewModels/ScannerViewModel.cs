using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using inventory_mobile_app.Models;
using inventory_mobile_app.Services;
using System.Text.Json;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;

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

        private bool _isClickedAdd;
        public bool IsClickedAdd
        {
            get => _isClickedAdd;
            set
            {
                SetProperty(ref _isClickedAdd, value);
            }
        }

        private bool _isAddProductSuccess;
        public bool IsAddProductSuccess
        {
            get => _isAddProductSuccess;
            set
            {
                SetProperty(ref _isAddProductSuccess, value);
            }
        }

        private bool _isNewProduct;
        public bool IsNewProduct
        {
            get => _isNewProduct;
            set
            {
                SetProperty(ref _isNewProduct, value);
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
        private RecordReportModel recordReportModel;

        [ObservableProperty]
        private string barcode;

        public ScannerViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            StockModel = new();
            RecordReportModel = new();
            Initialize();
            GetUserNameFromSecuredStorage();
        }

        private async void Initialize()
        {
            await LoadProduct(Barcode);
        }

        private async Task LoadProduct(string barcode)
        {
            if (!string.IsNullOrWhiteSpace(barcode))
            {
                var response = await clientService.GetProductAsync(barcode);
                if (response != null)
                {
                    Product = response;
                }
            }
        }

        public async Task ScannerViewProduct(string barcode)
        {
            Barcode = barcode;

            var response = await clientService.GetProductAsync(Barcode);

            if (response == null)
            {
                IsNewProduct = true;
            }
            else
            {
                Product = response;
                IsProduct = true;
                StockModel.Barcode = Barcode;
            }
        }

        private async void GetUserNameFromSecuredStorage()
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage != null)
            {
                RecordReportModel.UserName = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.UserName!;
                return;
            }
        }
        [RelayCommand]

        private async Task AddStock()
        {
            bool stockAdded = await clientService.AddStock(StockModel);

            RecordReportModel.ReportType = "ADD STOCK";
            RecordReportModel.ProductName = Product.ProductName;
            RecordReportModel.Quantity = StockModel.Stock;
            RecordReportModel.CurrentStock = Product.Stock + StockModel.Stock;

            bool recordReport = await clientService.RecordReport(RecordReportModel);

            if (StockModel.Stock <= 0 || StockModel.Stock == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid Quantity", "Close");
            }
            else if(!recordReport)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Recording Reports Failed", "Close");
            }
            else if (stockAdded)
            {
                await LoadProduct(StockModel.Barcode);
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

            RecordReportModel.ReportType = "SOLD STOCK";
            RecordReportModel.ProductName = Product.ProductName;
            RecordReportModel.Quantity = StockModel.Stock;
            RecordReportModel.CurrentStock = Product.Stock - StockModel.Stock;

            bool recordReport = await clientService.RecordReport(RecordReportModel);


            if (StockModel.Stock <= 0 || StockModel.Stock == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid Quantity", "Close");
            }
            else if (!recordReport)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to record report", "Close");
            }
            else if (stockSold)
            {
                await LoadProduct(StockModel.Barcode);
                
                SoldStockQuantity = StockModel.Stock;
                IsSoldStock = true;
                IsProduct = false;

                if (Product.Stock <= 5 && Product.Stock > 0)
                {
                    // Show local notification
                    var request = new NotificationRequest
                    {
                        Title = "Low Stock Warning",
                        Description = $"{Product.ProductName} stock is low (only {Product.Stock} left).",
                        BadgeNumber = 42,
                        CategoryType = NotificationCategoryType.Service,
                        Android = new AndroidOptions
                        {
                            Priority = AndroidPriority.High,
                        }
                    };

                    LocalNotificationCenter.Current.Show(request);
                }
                else if (Product.Stock == 0)
                {
                    // Show local notification
                    var request = new NotificationRequest
                    {
                        Title = "Out of Stock Warning",
                        Description = $"{Product.ProductName} is out of stock.",
                        BadgeNumber = 42,
                        CategoryType = NotificationCategoryType.Reminder,
                        Android = new AndroidOptions
                        {
                            Priority = AndroidPriority.High
                        }
                    };

                    LocalNotificationCenter.Current.Show(request);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to sell stock", "OK");
            }
        }

        [RelayCommand]

        private async Task AddProduct()
        {
            Product.Barcode = Barcode;
            Product.ExpiryDate = DateOnly.FromDateTime(DateTime.Now);

            bool addedSuccessfully = await clientService.AddProductAsync(Product);

            if (addedSuccessfully)
            {
                IsAddProductSuccess = true;
                IsClickedAdd = false;
                IsNewProduct = false;
                Product = new Product();
            }
            else if(addedSuccessfully == false)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to add product", "OK");
            }
        }

        [RelayCommand]

        private Task OnClickAddProduct()
        {
            IsClickedAdd = true;
            IsNewProduct = false;
            return Task.CompletedTask;
        }

        public void ResetProperties()
        {
            IsAddProductSuccess = false;
            IsAddStock = false;
            IsSoldStock = false;
            IsProduct = false;
            IsNewProduct = false;
            AddedStockQuantity = 0;
            StockModel = new StockModel();
            Barcode = string.Empty;
            Product = new Product();
        }
    }
}
