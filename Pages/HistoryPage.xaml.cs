using inventory_mobile_app.ViewModels;
using static inventory_mobile_app.ViewModels.MainPageViewModel;
namespace inventory_mobile_app.Pages;

public partial class HistoryPage : ContentPage
{
	public HistoryPage()
	{
		InitializeComponent();
        BindingContext = App.MainViewModel;
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