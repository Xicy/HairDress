using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HairDress.PL.Converter
{
    public class BackgroundViewPort : IValueConverter
    {
        public object Convert(dynamic value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Rect(new Size(value.Width, value.Height));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}