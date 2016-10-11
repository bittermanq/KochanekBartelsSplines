using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace KochanekBartelsSplines.TestApp.Converters
{
    public class UserControlToMousePositionConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var element = (UserControl)value;
            var position = Mouse.GetPosition(element);

            if (position.X < 0) position.X = 0;
            if (position.X > element.Width) position.X = element.Width;
            if (position.Y < 0) position.Y = 0;
            if (position.Y > element.Width) position.Y = element.Width;
            
            return new Point((int) position.X, (int) position.Y);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}