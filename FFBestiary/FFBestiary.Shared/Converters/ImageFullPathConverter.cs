using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace FFBestiary.Converters
{
    public class ImageFullPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || value.GetType() != typeof(string) || parameter == null)
                return null;

            var path = value.ToString();

            switch (parameter.ToString())
            {
                case "Game":
                    path = string.Format("{0}{1}", "ms-appx:///Images/Series/", path);
                    break;
                default:
                    break;
            }          

            return path;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
