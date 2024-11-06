
using inventory_mobile_app.ViewModels;
using Microsoft.Maui.Platform;

namespace inventory_mobile_app.Pages.Auth;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
        BindingContext = loginViewModel;
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        Email.Text = string.Empty;
        Password.Text = string.Empty;
    }

    private async void HandleSignUp(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync(nameof(SignupPage));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        //await (BindingContext as LoginViewModel).LoginCommand.ExecuteAsync(null);

        try
        {
            await Shell.Current.GoToAsync(nameof(HomePage));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}