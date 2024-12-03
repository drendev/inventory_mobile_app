using CommunityToolkit.Mvvm.ComponentModel;
using inventory_mobile_app.Models;
using inventory_mobile_app.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace inventory_mobile_app.ViewModels
{
    public partial class ReportListViewModel : ObservableObject
    {
        private readonly ClientService clientService;
        private readonly DashboardViewModel dashboardViewModel;

        [ObservableProperty]
        private string searchText; // Bind this to the SearchBar Text

        [ObservableProperty]
        private Dashboard dashboard;

        public ObservableCollection<Report> Reports { get; set; } = new();
        public ObservableCollection<Report> FilteredReports { get; set; } = new();



        public ReportListViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            Dashboard = new();
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
                // Fetch the report list from the service
                var response = await clientService.GetReportListAsync();
                var response1 = await clientService.GetDashboardAsync();

                if (response1 != null)
                {
                    Dashboard = response1;
                }
                else
                {
                    // Provide a detailed message for debugging
                    string errorMessage = response == null ? "The response is null." : "The response contains no dashboard.";
                    await Shell.Current.DisplayAlert("Error", $"An error occurred while fetching the dashboard: {errorMessage}", "OK");
                }

                // Clear existing reports
                Reports.Clear();
                FilteredReports.Clear();

                if (response != null && response.Any())
                {
                    var sortedReports = response
                        .OrderByDescending(report => report.Date.ToDateTime(report.Created))
                        .ToList();

                    // Add reports to the ObservableCollection
                    foreach (var report in sortedReports)
                    {
                        Reports.Add(report);
                    }

                    ApplySearch(); // Apply search after loading reports
                }
                else
                {
                    string errorMessage = response == null ? "The response is null." : "The response contains no reports.";
                    await Shell.Current.DisplayAlert("Error", $"An error occurred while fetching reports: {errorMessage}", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            }
        }

        private void ApplySearch()
        {
            // Filter the Reports collection based on SearchText and update FilteredReports
            var filteredReports = string.IsNullOrWhiteSpace(SearchText)
                ? Reports
                : new ObservableCollection<Report>(
                    Reports.Where(r => r.ProductName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                       r.Username.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));

            FilteredReports.Clear();
            foreach (var report in filteredReports)
            {
                FilteredReports.Add(report);
            }
        }

        // This method is triggered when the search text changes
        partial void OnSearchTextChanged(string value)
        {
            ApplySearch(); // Update the filtered reports when the search text changes
        }
    }
}
