
using CommunityToolkit.Mvvm.ComponentModel;
using inventory_mobile_app.Models;
using inventory_mobile_app.Services;
using System.Collections.ObjectModel;

namespace inventory_mobile_app.ViewModels
{
    public partial class CategoryViewModel : ObservableObject
    {
        private readonly ClientService clientService;

        public ObservableCollection<Category> Categories { get; set; } = [];
        public CategoryViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            LoadCategoriesData();
        }

        private async Task LoadCategoriesData()
        {
            var response = await clientService.GetCategoriesData();
            Categories?.Clear();
            if (response.Any())
            {
                foreach (var categories in response)
                {
                    Categories!.Add(categories);
                }
            }
        }
    }
}
