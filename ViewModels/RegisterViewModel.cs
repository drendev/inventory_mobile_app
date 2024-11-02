
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using inventory_mobile_app.Models;
using inventory_mobile_app.Pages.Auth;
using inventory_mobile_app.Services;
using System.Text.Json;

namespace inventory_mobile_app.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        [ObservableProperty]
        private RegisterModel registerModel;

        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private bool isAuthenticated;

        private readonly ClientService clientService;

        public RegisterViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            RegisterModel = new();
            IsAuthenticated = false;
        }

        [RelayCommand]
        private async Task Register()
        {
            await clientService.Register(RegisterModel);
        }

        private async void GetUserNameFromSecuredStorage()
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");

            if (serializedLoginResponseInStorage != null)
            {
                var loginResponse = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage);
                UserName = loginResponse?.UserName;
                IsAuthenticated = !string.IsNullOrEmpty(UserName);
            }
            else
            {
                IsAuthenticated = false;
            }
        }
    }
}
