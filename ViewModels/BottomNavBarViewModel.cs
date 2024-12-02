using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace inventory_mobile_app.ViewModels
{
    public class BottomNavBarViewModel : INotifyPropertyChanged
    {

        private string selectedTab;
        public string SelectedTab
        {
            get => selectedTab;
            set
            {
                if (selectedTab != value)
                {
                    selectedTab = value;
                    OnPropertyChanged(nameof(SelectedTab));
                    OnPropertyChanged(nameof(IsHomeSelected));
                    OnPropertyChanged(nameof(IsInventorySelected));
                    OnPropertyChanged(nameof(IsScanSelected));
                    OnPropertyChanged(nameof(IsHistorySelected));
                    OnPropertyChanged(nameof(IsSettingsSelected));
                }
            }
        }

        public bool IsHomeSelected => SelectedTab == "HomePage";
        public bool IsInventorySelected => SelectedTab == "InventoryPage";
        public bool IsScanSelected => SelectedTab == "ScanPage";
        public bool IsHistorySelected => SelectedTab == "HistoryPage";
        public bool IsSettingsSelected => SelectedTab == "SettingsPage";

        public BottomNavBarViewModel()
        {
            SelectedTab = "HomePage";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}