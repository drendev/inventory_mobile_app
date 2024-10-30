namespace inventory_mobile_app.Pages.Auth;

public partial class SignupPage : ContentPage
{
	public SignupPage()
	{
		InitializeComponent();
	}

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync(); // Go back to the previous page
    }

    private async void HandleNext(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new SetPassword());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}