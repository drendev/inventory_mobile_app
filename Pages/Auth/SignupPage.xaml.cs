using inventory_mobile_app.ViewModels;
using Microsoft.Maui;

namespace inventory_mobile_app.Pages.Auth;

public partial class SignupPage : ContentPage
{
    private readonly Color OriginalColor = Color.FromArgb("#FF6B2C");
    private readonly Color PressedColor = Color.FromArgb("#D9531A");

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
        await Shell.Current.GoToAsync("//SetPassword");
    }

    private async void OnSignupButtonTapped(object sender, TappedEventArgs e)
    {
        SignupButtonFrame.BackgroundColor = PressedColor;
        await SignupButtonFrame.ScaleTo(0.95, 50);
        await SignupButtonFrame.ScaleTo(1, 50);
        await Task.Delay(100);

        SignupButtonFrame.BackgroundColor = OriginalColor;
    }
}
