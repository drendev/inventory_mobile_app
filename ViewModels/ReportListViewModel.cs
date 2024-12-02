using CommunityToolkit.Mvvm.ComponentModel;
using inventory_mobile_app.Models;
using inventory_mobile_app.Services;
using System.Collections.ObjectModel;

namespace inventory_mobile_app.ViewModels
{
    public partial class ReportListViewModel: ObservableObject
    {
        private readonly ClientService clientService;

        public ObservableCollection<Report> Reports { get; set; } = [];

        public ReportListViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            Initialize();
        }

        private async void Initialize()
        {
            await LoadReportList();
        }

        public async Task LoadReportList()
        {
            try
            {
                // Fetch the product list from the service
                var response = await clientService.GetReportListAsync();

                // Clear existing products
                Reports.Clear();

                if (response != null && response.Any())
                {
                    var sortedReports = response
                        .OrderByDescending(report => report.Date.ToDateTime(report.Created))
                        .ToList();

                    // Add the products to the ObservableCollection
                    foreach (var report in sortedReports)
                    {
                        Reports.Add(report);
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
