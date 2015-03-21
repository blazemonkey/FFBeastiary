using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace FFBestiary.Converters
{
    public class IsBossConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((value == null) || (value.GetType() != typeof(bool)))
                return "";

            var isBoss = (bool)value;

            return isBoss ? "(BOSS)" : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
