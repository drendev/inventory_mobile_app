using inventory_mobile_app.ViewModels;

namespace inventory_mobile_app.Pages.Auth;

public partial class SignupPage : ContentPage
{
	public SignupPage(SignupViewModel signUpViewModel)
	{
		InitializeComponent();
        BindingContext = signUpViewModel;
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void HandleNext(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync("//SetPassword");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}