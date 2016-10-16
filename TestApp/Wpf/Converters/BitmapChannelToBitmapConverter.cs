using System;
using System.Globalization;
using System.Windows.Data;
using KochanekBartelsSplines.TestApp.Drawing.Interfaces;
using KochanekBartelsSplines.TestApp.Models;

namespace KochanekBartelsSplines.TestApp.Wpf.Converters
{
    public class BitmapChannelToBitmapConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bitmapChannel = (BitmapChannel)value;
            if (bitmapChannel == null) return null;
            var bitmapDrawer = (IBitmapDrawer) parameter;
            if (bitmapDrawer == null) return null;

            var writeableBitmap = bitmapDrawer.GetBitmap(bitmapChannel);

            return writeableBitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}