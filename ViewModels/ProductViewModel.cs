using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using inventory_mobile_app.Models;
using inventory_mobile_app.Services;

namespace inventory_mobile_app.ViewModels
{
    [QueryProperty(nameof(Product), "Product")]
    public partial class ProductViewModel : ObservableObject
    {
        private ProductList _product;
        private readonly ClientService clientService;

        [ObservableProperty]
        private UpdateProduct _updateProduct;

        [ObservableProperty]
        private ProductDelete _productDelete;

        private bool _isUpdatedProduct;
        public bool IsUpdatedProduct
        {
            get => _isUpdatedProduct;
            set
            {
                SetProperty(ref _isUpdatedProduct, value);
            }
        }

        private bool _isClickUpdate;
        public bool IsClickUpdate
        {
            get => _isClickUpdate;
            set
            {
                SetProperty(ref _isClickUpdate, value);
            }
        }

        private bool _isProductDeleted;
        public bool IsProductDeleted
        {
            get => _isProductDeleted;
            set
            {
                SetProperty(ref _isProductDeleted, value);
            }
        }

        private bool _isClosed;
        public bool IsClosed
        {
            get => _isClosed;
            set
            {
                SetProperty(ref _isClosed, value);
            }
        }

        public ProductList Product
        {
            get => _product;
            set
            {
                SetProperty(ref _product, value);
                if (_product != null)
                {
                    // Populate UpdateProduct when Product is set
                    UpdateProduct = new UpdateProduct
                    {
                        Barcode = _product.Barcode,
                        ProductName = _product.ProductName,
                        Description = _product.Description,
                        BasePrice = _product.BasePrice,
                        SalePrice = _product.SalePrice,
                        Stock = _product.Stock,
                        ImageUrl = _product.ImageUrl,
                        ExpiryDate = _product.ExpiryDate
                    };
                }
            }
        }

        public ProductViewModel(ClientService clientService)
        {
            this.clientService = clientService;
        }

        [RelayCommand]
        private async Task EditProductAsync()
        {
            // Call API to update the product
            bool isUpdated = await clientService.UpdateProductAsync(UpdateProduct);

            if (isUpdated)
            {
                // Update the existing Product object
                Product.Barcode = UpdateProduct.Barcode;
                Product.ProductName = UpdateProduct.ProductName;
                Product.Description = UpdateProduct.Description;
                Product.BasePrice = UpdateProduct.BasePrice;
                Product.SalePrice = UpdateProduct.SalePrice;
                Product.Stock = UpdateProduct.Stock;
                Product.ImageUrl = UpdateProduct.ImageUrl;
                Product.ExpiryDate = UpdateProduct.ExpiryDate;

                // Trigger PropertyChanged for Product
                OnPropertyChanged(nameof(Product));

                IsUpdatedProduct = true;
                IsClickUpdate = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to update product", "OK");
            }
        }

        [RelayCommand]
        private async Task DeleteProductAsync()
        {
            // Call API to delete the product
            bool isDeleted = await clientService.DeleteProductAsync(Product.ProductId);

            if (isDeleted)
            {
                // Trigger PropertyChanged for IsDeletedProduct
                IsProductDeleted = true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to delete product", "OK");
            }
        }

        [RelayCommand]
        private Task OnClickUpdateProduct()
        {
            IsClickUpdate = true;
            return Task.CompletedTask;
        }

        [RelayCommand]
        private async Task OnClosedProduct()
        {
            IsProductDeleted = false;
            await Shell.Current.GoToAsync("//InventoryPage");
        }
    }
}
