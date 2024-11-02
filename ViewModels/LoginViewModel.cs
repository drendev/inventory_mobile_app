using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using inventory_mobile_app.Models;
using inventory_mobile_app.Services;
using Microsoft.Maui.Controls;
using System.Text.Json;

namespace inventory_mobile_app.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
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

        [RelayCommand]

        private async Task Login()
        {
            await clientService.Login(LoginModel);
        }
    }
}