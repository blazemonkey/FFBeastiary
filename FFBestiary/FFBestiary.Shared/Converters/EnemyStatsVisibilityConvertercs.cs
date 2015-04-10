using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FFBestiary.Converters
{
    public class EnemyStatsVisibilityConvertercs : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((value == null) || (value.GetType() != typeof(int)))
                return Visibility.Collapsed;

            var count = (int)value;

            return count > 1 ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
