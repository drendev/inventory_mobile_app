
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
        private bool _isSignup;
        public bool IsSignup
        {
            get => _isSignup;
            set
            {
                SetProperty(ref _isSignup, value);
                SignupCommand.NotifyCanExecuteChanged();
            }
        }

        [ObservableProperty]
        private SignupModel signupModel;

        [ObservableProperty]
        private List<string> availablePositions;

        private readonly ClientService clientService;

        public SignupViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            SignupModel = new();
            AvailablePositions = new List<string> { "Manager", "Staff", "Supervisor", "Inventory Clerk" };
        }

        [RelayCommand(CanExecute = nameof(CanExecuteSignup))]
        private async Task Signup()
        {
            IsSignup = true;
            bool signUpSuccessful = await clientService.Signup(SignupModel);

            if (!signUpSuccessful)
            {
                await Application.Current.MainPage.DisplayAlert("Signup Failed", "Signup failed. Please try again", "OK");
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }

            IsSignup = false;
        }
        private bool CanExecuteSignup() => !IsSignup;
    }
}
