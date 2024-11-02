
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using inventory_mobile_app.Models;
using inventory_mobile_app.Pages.Auth;
using inventory_mobile_app.Services;
using System.Text.Json;

namespace inventory_mobile_app.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private RegisterModel registerModel;
        [ObservableProperty]
        private LoginModel loginModel;

        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private bool isAuthenticated;

        private readonly ClientService clientService;

        public MainPageViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            RegisterModel = new();
            LoginModel = new();
            isAuthenticated = false;
            GetUserNameFromSecuredStorage();
        }

        [RelayCommand]

        private async Task Register()
        {
            await clientService.Register(RegisterModel);
        }

        [RelayCommand]

        private async Task Login()
        {
            await clientService.Login(LoginModel);
            GetUserNameFromSecuredStorage();
        }

        private async Task Logout()
        {
            SecureStorage.Default.Remove("Authentication");
            isAuthenticated = false;
            await Shell.Current.GoToAsync(nameof(MainPage));
        }

        private async void GetUserNameFromSecuredStorage()
        {
            if (!string.IsNullOrEmpty(UserName))
            {
                isAuthenticated = true;
                return;
            }
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage != null)
            {
                UserName = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.UserName;
                isAuthenticated = true;
                return;
            }
            isAuthenticated = false;
        }

    }
}
