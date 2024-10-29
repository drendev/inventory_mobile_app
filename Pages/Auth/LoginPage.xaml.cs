
namespace inventory_mobile_app.Pages.Auth;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync(); // Go back to the previous page
    }
}