using System;
using System.Globalization;
using System.Windows.Data;

namespace KochanekBartelsSplines.TestApp.Wpf.Converters
{
    public class MediaColorToDrawingColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var drawingColor = (System.Drawing.Color) value;

            return System.Windows.Media.Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var mediaColor = (System.Windows.Media.Color)value;

            return System.Drawing.Color.FromArgb(mediaColor.A, mediaColor.R, mediaColor.G, mediaColor.B);
        }
    }
}