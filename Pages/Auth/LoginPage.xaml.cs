 
using inventory_mobile_app.ViewModels;
using Microsoft.Maui.Platform;

namespace inventory_mobile_app.Pages.Auth;

public partial class LoginPage : ContentPage
{
    private readonly Color OriginalColor = Color.FromArgb("#FF6B2C");
    private readonly Color PressedColor = Color.FromArgb("#D9531A");

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
            await Shell.Current.GoToAsync("//SignupPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnLoginButtonTapped(object sender, TappedEventArgs e)
    {
        LoginButtonFrame.BackgroundColor = PressedColor;
        await LoginButtonFrame.ScaleTo(0.95, 50);
        await LoginButtonFrame.ScaleTo(1, 50);
        await Task.Delay(100);

        LoginButtonFrame.BackgroundColor = OriginalColor;
    }
}