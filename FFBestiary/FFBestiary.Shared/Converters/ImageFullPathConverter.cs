using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FFBestiary.Converters
{
    public class ImageFullPathConverter : DependencyObject, IValueConverter
    {
        public string SubFolder
        {
            get { return (string)GetValue(SubFolderProperty); }
            set { SetValue(SubFolderProperty, value); }
        }

        public static readonly DependencyProperty SubFolderProperty =
       DependencyProperty.Register("SubFolder",
                                   typeof(string),
                                   typeof(ImageFullPathConverter),
                                   new PropertyMetadata(null));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || value.GetType() != typeof(string) || (SubFolder == null && parameter == null))
                return null;

            var path = value.ToString();

            if (parameter == null)
            {
                path = string.Format("{0}{1}{2}{3}", "ms-appx:///Images/", SubFolder, "/", path);
                return path;
            }

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
