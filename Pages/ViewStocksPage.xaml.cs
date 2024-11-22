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
    private void OnTabClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            string tab = button.CommandParameter as string;

            // Reset all tab borders
            AllItemsBorder.IsVisible = false;
            LowStockBorder.IsVisible = false;
            OutOfStockBorder.IsVisible = false;

            // Reset all content visibility
            AllItemsContent.IsVisible = false;
            LowStockContent.IsVisible = false;
            OutOfStockContent.IsVisible = false;

            // Show selected tab and content
            switch (tab)
            {
                case "All":
                    AllItemsBorder.IsVisible = true;
                    AllItemsContent.IsVisible = true;
                    break;
                case "LowStock":
                    LowStockBorder.IsVisible = true;
                    LowStockContent.IsVisible = true;
                    break;
                case "OutOfStock":
                    OutOfStockBorder.IsVisible = true;
                    OutOfStockContent.IsVisible = true;
                    break;
            }
        }
    }
}
