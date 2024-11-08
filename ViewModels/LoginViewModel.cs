using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using inventory_mobile_app.Models;
using inventory_mobile_app.Pages;
using inventory_mobile_app.Services;
using Microsoft.Maui.Controls;
using System.Text.Json;

namespace inventory_mobile_app.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private bool _isLoggingIn;
        public bool IsLoggingIn
        {
            get => _isLoggingIn;
            set
            {
                SetProperty(ref _isLoggingIn, value);
                LoginCommand.NotifyCanExecuteChanged();
            }
        }

        [ObservableProperty]
        private LoginModel loginModel;

        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private bool isAuthenticated;

        private readonly ClientService clientService;

        public LoginViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            LoginModel = new();
            IsAuthenticated = false;
        }

        [RelayCommand(CanExecute = nameof(CanExecuteLogin))]

        private async Task Login()
        {
            IsLoggingIn = true;

            // Call the login method and check if login was successful
            bool loginSuccessful = await clientService.Login(LoginModel);

            if (!loginSuccessful)
            {
                // Show alert if login failed
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Invalid username or password", "OK");
            }
            else
            {
                // Set authentication status if login is successful
                IsAuthenticated = true;
                await Shell.Current.GoToAsync(nameof(HomePage));
            }

            IsLoggingIn = false;
        }
        private bool CanExecuteLogin() => !IsLoggingIn;
    }
}