
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using inventory_mobile_app.Models;
using inventory_mobile_app.Pages.Auth;
using inventory_mobile_app.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace inventory_mobile_app.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private SignupModel registerModel;
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
            await clientService.Signup((SignupModel)RegisterModel);
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

        public class MainViewModel : INotifyPropertyChanged
        {
            private bool isHomeSelected;
            public bool IsHomeSelected
            {
                get => isHomeSelected;
                set
                {
                    if (isHomeSelected != value)
                    {
                        isHomeSelected = value;
                        OnPropertyChanged();
                    }
                }
            }

            private bool isInventorySelected;
            public bool IsInventorySelected
            {
                get => isInventorySelected;
                set
                {
                    if (isInventorySelected != value)
                    {
                        isInventorySelected = value;
                        OnPropertyChanged();
                    }
                }
            }

            private bool isScanSelected;
            public bool IsScanSelected
            {
                get => isScanSelected;
                set
                {
                    if (isScanSelected != value)
                    {
                        isScanSelected = value;
                        OnPropertyChanged();
                    }
                }
            }

            private bool isHistorySelected;
            public bool IsHistorySelected
            {
                get => isHistorySelected;
                set
                {
                    if (isHistorySelected != value)
                    {
                        isHistorySelected = value;
                        OnPropertyChanged();
                    }
                }
            }

        

            private bool isSettingsSelected;
            public bool IsSettingsSelected
            {
                get => isSettingsSelected;
                set
                {
                    if (isSettingsSelected != value)
                    {
                        isSettingsSelected = value;
                        OnPropertyChanged();
                    }
                }
            }

            private DateTime _expiryDate = DateTime.Today;

            public DateTime ExpiryDate
            {
                get => _expiryDate;
                set
                {
                    if (_expiryDate != value)
                    {
                        _expiryDate = value;
                        OnPropertyChanged(nameof(ExpiryDate)); 
                    }
                }
            }

            public MainViewModel()
            {
                IsHomeSelected = true; 
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
