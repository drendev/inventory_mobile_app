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

        public bool IsHomeSelected => SelectedTab == "Home";
        public bool IsInventorySelected => SelectedTab == "Inventory";
        public bool IsScanSelected => SelectedTab == "Scan";
        public bool IsHistorySelected => SelectedTab == "History";
        public bool IsSettingsSelected => SelectedTab == "Settings";

        public BottomNavBarViewModel()
        {
            SelectedTab = "Home";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}