using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace KochanekBartelsSplines.Converters
{
    public class BoolToColorConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isActive = (bool) value;

            return isActive ? Brushes.AliceBlue : Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}