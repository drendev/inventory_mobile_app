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

        private bool isHomeSelected;
        public bool IsHomeSelected
        {
            get => isHomeSelected;
            set
            {
                if (isHomeSelected != value)
                {
                    isHomeSelected = value;
                    OnPropertyChanged(nameof(IsHomeSelected));
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
                    OnPropertyChanged(nameof(IsInventorySelected));
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
                    OnPropertyChanged(nameof(IsScanSelected));
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
                    OnPropertyChanged(nameof(IsHistorySelected));
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
                    OnPropertyChanged(nameof(IsSettingsSelected));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}