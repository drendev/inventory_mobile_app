using inventory_mobile_app.ViewModels;
using static inventory_mobile_app.ViewModels.MainPageViewModel;
namespace inventory_mobile_app.Pages;

public partial class HistoryPage : ContentPage
{
	public HistoryPage(ReportListViewModel reportListViewModel)
	{
		InitializeComponent();
        BindingContext = reportListViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Refresh the report list each time the page appears
        if (BindingContext is ReportListViewModel viewModel)
        {
            viewModel.LoadReportList();  
        }
    }

    private async void OnViewStocksClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync("//ViewStocksPage");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }
}