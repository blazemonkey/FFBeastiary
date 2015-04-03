using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace FFBestiary.Converters
{
    public class BooleanToSymbolFavouritesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((value == null) || (value.GetType() != typeof(bool)))
                return "";

            var isFavourite = (bool)value;

            return isFavourite ? Symbol.Remove : Symbol.Add;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
