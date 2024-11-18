using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace inventory_mobile_app.Converters
{
    public class SelectedColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var selectedTab = value as string;
            var tab = parameter as string;

            if (selectedTab == tab)
                return Color.FromHex("#0050A2");  

            return Color.FromHex("#979797");  
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}