using inventory_mobile_app.ViewModels;

namespace inventory_mobile_app.Pages;

public partial class Inventory : ContentPage
{
	public Inventory(ProductListViewModel productListViewModel)
	{
		InitializeComponent();
        BindingContext = productListViewModel;
    }
}