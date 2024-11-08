
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using inventory_mobile_app.Models;
using inventory_mobile_app.Pages.Auth;
using inventory_mobile_app.Services;
using System.Text.Json;

namespace inventory_mobile_app.ViewModels
{
    public partial class SignupViewModel : ObservableObject
    {
        [ObservableProperty]
        private SignupModel signupModel;

        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private bool isAuthenticated;

        private readonly ClientService clientService;

        public SignupViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            SignupModel = new();
            IsAuthenticated = false;
        }

        [RelayCommand]
        private async Task Signup()
        {
            bool signUpSuccessful = await clientService.Signup(SignupModel);

            if (!signUpSuccessful)
            {
                await Application.Current.MainPage.DisplayAlert("Signup Failed", "Signup failed. Please try again", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Signup Successful", "You have successfully signed up", "OK");
                await Shell.Current.GoToAsync(nameof(SignupPage));
            }
        }
    }
}
