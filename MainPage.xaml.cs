using inventory_mobile_app.Pages.Auth;
using inventory_mobile_app.ViewModels;

namespace inventory_mobile_app
{
    public partial class MainPage : ContentPage
    {
        private CancellationTokenSource loginCancellationTokenSource;

        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
        }

        // Handle login button click event
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
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
    }

}
