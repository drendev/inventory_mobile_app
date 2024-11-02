using inventory_mobile_app.ViewModels;

namespace inventory_mobile_app.Pages.Auth;

public partial class SignupPage : ContentPage
{
	public SignupPage(RegisterViewModel registerViewModel)
	{
		InitializeComponent();
        BindingContext = registerViewModel;
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