using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Billionaires.Universal.Mvvm.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
            => value is bool && (bool)value ? Visibility.Visible: Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}