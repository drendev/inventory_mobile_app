using inventory_mobile_app.Pages.Auth;

namespace inventory_mobile_app
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        // Handle login button click event
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void HandleSignUp(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new SignupPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }

}
