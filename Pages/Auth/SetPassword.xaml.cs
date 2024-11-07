using inventory_mobile_app.ViewModels;

namespace inventory_mobile_app.Pages.Auth;

public partial class SetPassword : ContentPage
{
	public SetPassword(SignupViewModel signUpViewModel)
	{
		InitializeComponent();
        BindingContext = signUpViewModel;
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//SignupPage");
    }
}