using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using KochanekBartelsSplines.TestApp.Drawing.Interfaces;
using KochanekBartelsSplines.TestApp.Models;
using KochanekBartelsSplines.TestApp.Models.Interfaces;
using Brushes = System.Drawing.Brushes;
using Pen = System.Drawing.Pen;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace KochanekBartelsSplines.TestApp.Drawing
{
    internal class BitmapDrawer : IBitmapDrawer
    {
        private readonly Pen _passivePointPen = new Pen(Brushes.Blue);
        private readonly Pen _activePointPen = new Pen(Brushes.Orange);
        private readonly Pen _activeLinePen = new Pen(Brushes.LimeGreen);

        private const int PassivePointRadius = 2;
        private const int ActivePointRadius = 5;


        public WriteableBitmap GetBitmap(IBitmapChannel bitmapChannel, IGraphicsGetter graphicsGetter)
        {
            const int dpi = 96;

            var writeableBitmap = new WriteableBitmap(800, 600, dpi, dpi, PixelFormats.Bgr32, null);

            writeableBitmap.Lock();

            var bitmap = new Bitmap(writeableBitmap.PixelWidth, writeableBitmap.PixelHeight,
                                    writeableBitmap.BackBufferStride, PixelFormat.Format32bppPArgb,
                                    writeableBitmap.BackBuffer);

            using (var graphics = graphicsGetter.FromImage(bitmap))
            {
                graphics.Clear(System.Drawing.Color.White);

                DrawCurves(bitmapChannel.Curves, graphics);
                DrawPoints(bitmapChannel.AnchorLines, graphics);
            }

            var dirtyRect = new Int32Rect(0, 0, (int) writeableBitmap.Width, (int) writeableBitmap.Height);

            writeableBitmap.AddDirtyRect(dirtyRect);
            writeableBitmap.Unlock();
            return writeableBitmap;
        }


        private void DrawCurves(IEnumerable<Curve> curves, Graphics graphics)
        {
            foreach (var curve in curves)
            {
                if (curve.Points.Count < 3)
                    continue;

                var pen = curve.IsActive ? _activeLinePen : new Pen(curve.Color);

                for (var i = 0; i < curve.Points.Count - 1; i++)
                {
                    graphics.DrawLine(pen, curve.Points[i], curve.Points[i + 1]);
                }
            }
        }

        private void DrawPoints(IEnumerable<AnchorLine> anchorLines, Graphics graphics)
        {
            foreach (var line in anchorLines)
            {
                if (!line.Points.Any())
                    continue;

                var radius = line.IsActive ? ActivePointRadius : PassivePointRadius;

                var firstPoint = line.Points.First();

                var pen = GetPen(firstPoint, line);

                graphics.DrawRectangle(pen, firstPoint.Position.X - radius, firstPoint.Position.Y - radius, 2* radius, 2* radius);

                for (var j = 1; j < line.Points.Count - 1; j++)
                {
                    var point = line.Points[j];

                    pen = GetPen(point, line);

                    graphics.DrawEllipse(pen, point.Position.X - radius, point.Position.Y - radius, 2* radius, 2* radius);
                }

                var lastPoint = line.Points.Last();

                pen = GetPen(lastPoint, line);

                graphics.DrawLine(pen, lastPoint.Position.X - radius, lastPoint.Position.Y - radius, lastPoint.Position.X + radius, lastPoint.Position.Y + radius);
                graphics.DrawLine(pen, lastPoint.Position.X + radius, lastPoint.Position.Y - radius, lastPoint.Position.X - radius, lastPoint.Position.Y + radius);
            }
        }

        private Pen GetPen(AnchorPoint point, AnchorLine line)
        {
            return line.IsActive 
                ? (point.IsActive ? _activePointPen : _passivePointPen)
                : new Pen(line.Color);
        }
    }
}