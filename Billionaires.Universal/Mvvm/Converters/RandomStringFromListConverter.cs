using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace Billionaires.Universal.Mvvm.Converters
{
    public class RandomStringFromListConverter : IValueConverter
    {
        private readonly Random _random = new Random();

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var enumrable = value as IEnumerable<string>;
            if (enumrable == null)
                return string.Empty;

            var list = enumrable.ToList();

            var index = _random.Next(0, list.Count);

            return list[index];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}