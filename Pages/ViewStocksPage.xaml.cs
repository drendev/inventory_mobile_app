namespace inventory_mobile_app.Pages;
using inventory_mobile_app.ViewModels;
using static inventory_mobile_app.ViewModels.MainPageViewModel;

public partial class ViewStocksPage : ContentPage
{
	public ViewStocksPage()
	{
		InitializeComponent();
	}

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HistoryPage");

    }

}