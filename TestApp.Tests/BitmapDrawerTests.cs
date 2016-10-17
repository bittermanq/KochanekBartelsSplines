using System.Collections.Generic;
using System.Drawing;
using KochanekBartelsSplines.TestApp.Drawing;
using KochanekBartelsSplines.TestApp.Drawing.Interfaces;
using KochanekBartelsSplines.TestApp.Models;
using KochanekBartelsSplines.TestApp.Models.Interfaces;
using Rhino.Mocks;
using Xunit;

namespace TestApp.Tests
{
    public class BitmapDrawerTests
    {
        private readonly BitmapDrawer _target;

        private readonly IGraphicsGetter _graphicsGetter;
        private readonly IBitmapChannel _bitmapChannel;
        private readonly Graphics _graphics;


        public BitmapDrawerTests()
        {
            _bitmapChannel = MockRepository.GenerateStub<IBitmapChannel>();
            _graphicsGetter = MockRepository.GenerateStub<IGraphicsGetter>();
            _graphics = MockRepository.GenerateStub<Graphics>();
            _graphicsGetter.Stub(x => x.FromImage(null)).IgnoreArguments().Return(_graphics);

            _target = new BitmapDrawer();
        }


        [Fact]
        public void GetBitmap_EmptyBitmapChannel_NoDrawing()
        {
            _bitmapChannel.AnchorLines = new List<AnchorLine>();
            _bitmapChannel.Curves = new List<Curve>();

            _target.GetBitmap(_bitmapChannel, _graphicsGetter);

            _graphics.AssertWasNotCalled(x => x.DrawLine(new Pen(new Color()), 0, 0, 0, 0), o => o.IgnoreArguments());
            _graphics.AssertWasNotCalled(x => x.DrawEllipse(new Pen(new Color()), 0, 0, 0, 0), o => o.IgnoreArguments());
            _graphics.AssertWasNotCalled(x => x.DrawRectangle(new Pen(new Color()), 0, 0, 0, 0), o => o.IgnoreArguments());
        }

        [Fact]
        public void GetBitmap_OneEmptyAnchorLine_NoDrawing()
        {
            _bitmapChannel.AnchorLines = new List<AnchorLine>
            {
                new AnchorLine(new ColorContainer(new Color(), null))
            };
            _bitmapChannel.Curves = new List<Curve>();

            _target.GetBitmap(_bitmapChannel, _graphicsGetter);

            _graphics.AssertWasNotCalled(x => x.DrawLine(new Pen(new Color()), 0, 0, 0, 0), o => o.IgnoreArguments());
            _graphics.AssertWasNotCalled(x => x.DrawEllipse(new Pen(new Color()), 0, 0, 0, 0), o => o.IgnoreArguments());
            _graphics.AssertWasNotCalled(x => x.DrawRectangle(new Pen(new Color()), 0, 0, 0, 0), o => o.IgnoreArguments());
        }

        [Fact]
        public void GetBitmap_OneEmptyCurve_NoDrawing()
        {
            _bitmapChannel.AnchorLines = new List<AnchorLine>();
            _bitmapChannel.Curves = new List<Curve>
            {
                new Curve(new ColorContainer(new Color(), null))
            };

            _target.GetBitmap(_bitmapChannel, _graphicsGetter);

            _graphics.AssertWasNotCalled(x => x.DrawLine(new Pen(new Color()), 0, 0, 0, 0), o => o.IgnoreArguments());
            _graphics.AssertWasNotCalled(x => x.DrawEllipse(new Pen(new Color()), 0, 0, 0, 0), o => o.IgnoreArguments());
            _graphics.AssertWasNotCalled(x => x.DrawRectangle(new Pen(new Color()), 0, 0, 0, 0), o => o.IgnoreArguments());
        }

        [Fact]
        public void GetBitmap_TwoCurves_ThreePointsInEachCurve_TwoLinesWithTwoSegmentsAreDrawn()
        {
            _bitmapChannel.AnchorLines = new List<AnchorLine>();
            var curve1 = new Curve(new ColorContainer(new Color(), null));
            curve1.Points.AddRange(new List<Point> {new Point(), new Point(), new Point()});
            var curve2 = new Curve(new ColorContainer(new Color(), null));
            curve2.Points.AddRange(new List<Point> { new Point(), new Point(), new Point() });
            _bitmapChannel.Curves = new List<Curve> { curve1, curve2 };
            
            _target.GetBitmap(_bitmapChannel, _graphicsGetter);

            _graphics.AssertWasCalled(x => x.DrawLine(new Pen(new Color()), new Point(), new Point()), o => o.IgnoreArguments().Repeat.Times(4));
        }

        [Fact]
        public void GetBitmap_OneAnchorLine_ThreePoints_RectangleAndEllipseAndCrossAreDrawn()
        {
            var line = new AnchorLine(new ColorContainer(new Color(), null));
            line.Points.AddRange(new List<AnchorPoint>
            {
                new AnchorPoint(new Point(), true, null),
                new AnchorPoint(new Point(), false, null),
                new AnchorPoint(new Point(), false, null)
            });
            _bitmapChannel.AnchorLines = new List<AnchorLine> { line };
            _bitmapChannel.Curves = new List<Curve>();

            _target.GetBitmap(_bitmapChannel, _graphicsGetter);

            _graphics.AssertWasCalled(x => x.DrawRectangle(new Pen(new Color()), 0, 0, 0, 0), o => o.IgnoreArguments());
            _graphics.AssertWasCalled(x => x.DrawEllipse(new Pen(new Color()), 0, 0, 0, 0), o => o.IgnoreArguments());
            _graphics.AssertWasCalled(x => x.DrawLine(new Pen(new Color()), 0, 0, 0, 0), o => o.IgnoreArguments());
            _graphics.AssertWasCalled(x => x.DrawLine(new Pen(new Color()), 0, 0, 0, 0), o => o.IgnoreArguments());
        }
    }
}
