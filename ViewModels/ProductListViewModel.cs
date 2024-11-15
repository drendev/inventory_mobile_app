using CommunityToolkit.Mvvm.ComponentModel;
using inventory_mobile_app.Models;
using inventory_mobile_app.Services;
using System.Collections.ObjectModel;

namespace inventory_mobile_app.ViewModels
{
    public partial class ProductListViewModel : ObservableObject
    {
        private readonly ClientService clientService;

        public ObservableCollection<ProductList> Products { get; set; } = new ObservableCollection<ProductList>();

        public ProductListViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            LoadProductList();
        }

        private async Task LoadProductList()
        {
            var response = await clientService.GetProductListsAsync();
            Products?.Clear();
            if (response != null && response.Any())
            {
                foreach (var product in response)
                {
                    Products.Add(product);
                }
            }
        }
    }
}
