using CommunityToolkit.Mvvm.ComponentModel;
using inventory_mobile_app.Models;
using inventory_mobile_app.Services;

namespace inventory_mobile_app.ViewModels
{
    public partial class DashboardViewModel: ObservableObject
    {
        private readonly ClientService clientService;

        [ObservableProperty]
        private Dashboard dashboard;

        public DashboardViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            Dashboard = new();
            Initialize();
        }

        private async void Initialize()
        {
            await LoadDashboard();
        }

        public async Task LoadDashboard()
        {
            try
            {
                // Fetch the dashboard from the service
                var response = await clientService.GetDashboardAsync();

                if (response != null)
                {
                    Dashboard = response;
                }
                else
                {
                    // Provide a detailed message for debugging
                    string errorMessage = response == null ? "The response is null." : "The response contains no dashboard.";
                    await Shell.Current.DisplayAlert("Error", $"An error occurred while fetching the dashboard: {errorMessage}", "OK");
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
