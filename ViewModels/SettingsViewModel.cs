using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using inventory_mobile_app.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_mobile_app.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isAuthenticated;

        private readonly ClientService clientService;

        public SettingsViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            IsAuthenticated = false;
        }

        [RelayCommand]
        private async Task Logout()
        {
            SecureStorage.Default.Remove("Authentication");
            IsAuthenticated = false;
            await Shell.Current.GoToAsync("MainPage");
        }

    }
}
