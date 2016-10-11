using System.Drawing;
using System.Linq;
using System.Windows.Input;
using KochanekBartelsSplines.Helpers.Interfaces;
using KochanekBartelsSplines.Models;
using KochanekBartelsSplines.ViewModels.Interfaces;

namespace KochanekBartelsSplines.Helpers
{
    public class SplinesController : PropertyChangedImplementation, ISplinesController, ISplineSettingsContainer, IBitmapContainer
    {
        private readonly ISelectedAnchorPointGetter _selectedAnchorPointGetter;
        private readonly ILineInterpolator _lineInterpolator;

        private AnchorPoint _activePoint;
        private int _lastLineId;

        private AnchorLine _activeAnchorLine;
        private AnchorLine ActiveAnchorLine
        {
            get { return _activeAnchorLine; }
            set
            {
                _activeAnchorLine.IsActive = false;
                _activeAnchorLine = value;
                _activeAnchorLine.IsActive = true;
            }
        }

        public ISplineSettingsController SplineSettingsController { get; set; }

        public BitmapChannel BitmapChannel { get; set; }


        public SplinesController(ISelectedAnchorPointGetter selectedAnchorPointGetter, ISplineControllerFactory splineControllerFactory,
                                 ILineInterpolator lineInterpolator)
        {
            _selectedAnchorPointGetter = selectedAnchorPointGetter;
            _lineInterpolator = lineInterpolator;

            SplineSettingsController = splineControllerFactory.Get(UpdateBitmapChannel);

            BitmapChannel = new BitmapChannel();
            BitmapChannel.Curves = _lineInterpolator.GetCurves(BitmapChannel.AnchorLines, SplineSettingsController).ToList();

            ResetAnchorLines();
            ResetParameters();
        }
        
        
        public void DeleteActivePoint()
        {
            if (_activePoint != null)
            {
                ActiveAnchorLine.Points.Remove(_activePoint);
                UpdateBitmapChannel();
            }
        }

        public void AddOrSelectPoint(Point point)
        {
            var selectedPoint = _selectedAnchorPointGetter.Get(point, ActiveAnchorLine);

            if (selectedPoint == null) AddAnchorPoint(point);
            else
            {
                if (selectedPoint != _activePoint)
                {
                    if (_activePoint != null) _activePoint.IsActive = false;
                    selectedPoint.IsActive = true;
                    _activePoint = selectedPoint;
                }
            }

            UpdateBitmapChannel();
        }

        public void ClearLines()
        {
            ResetAnchorLines();
            UpdateBitmapChannel();
        }

        public void ResetParameters()
        {
            SplineSettingsController.Tension = 0;
            SplineSettingsController.Continuity = 0;
            SplineSettingsController.Bias = 0;
            SplineSettingsController.Segments = 10;

            RaisePropertyChanged(() => SplineSettingsController.Tension);
            RaisePropertyChanged(() => SplineSettingsController.Continuity);
            RaisePropertyChanged(() => SplineSettingsController.Bias);
            RaisePropertyChanged(() => SplineSettingsController.Segments);

            UpdateBitmapChannel();
        }

        public void DeleteAnchorLine()
        {
            if (BitmapChannel.AnchorLines.Count > 1)
            {
                BitmapChannel.AnchorLines.Remove(ActiveAnchorLine);
                ActiveAnchorLine = BitmapChannel.AnchorLines.First();
                UpdateBitmapChannel();
            }
        }

        public void AddAnchorLine()
        {
            ActiveAnchorLine = GetNewAnchorLine();

            if (_activePoint != null)
            {
                _activePoint.IsActive = false;
                _activePoint = null;
            }

            BitmapChannel.AnchorLines.Add(ActiveAnchorLine);
            UpdateBitmapChannel();
        }

        public void MakeCurveActive(int id)
        {
            var selectedCurve = (from curve in BitmapChannel.Curves
                where curve.Id == id
                select curve).First();

            for (var i = 0; i < BitmapChannel.Curves.Count; i++)
            {
                if (BitmapChannel.Curves[i] == selectedCurve)
                {
                    ActiveAnchorLine = BitmapChannel.AnchorLines[i];
                    break;
                }
            }

            UpdateBitmapChannel();
        }

        public void MoveActivePoint(Point point)
        {
            _activePoint.Position = point;
            UpdateBitmapChannel();
        }

        public void SetLineClosed(Point point)
        {
            var selectedPoint = _selectedAnchorPointGetter.Get(point, ActiveAnchorLine);

            if (selectedPoint == null)
                return;

            foreach (var line in BitmapChannel.AnchorLines.Where(line => line.Points.Any() && line.Points.First() == selectedPoint))
            {
                line.IsClosed = !line.IsClosed;
            }
        }


        public void MouseDown(Point point)
        {
            AddOrSelectPoint(point);
        }

        public void MouseMove(Point point)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                MoveActivePoint(point);
            }
        }

        public void MouseDoubleClick(Point point)
        {
            SetLineClosed(point);
        }

        public void KeyDown()
        {
            if (Keyboard.IsKeyDown(Key.Delete))
            {
                DeleteActivePoint();
            }
        }


        private void UpdateBitmapChannel()
        {
            BitmapChannel.Curves = _lineInterpolator.GetCurves(BitmapChannel.AnchorLines, SplineSettingsController).ToList();
            RaisePropertyChanged(() => BitmapChannel);
        }


        private void AddAnchorPoint(Point point)
        {
            if (_activePoint != null)
                _activePoint.IsActive = false;

            _activePoint = new AnchorPoint(point, true);

            ActiveAnchorLine.Points.Add(_activePoint);
        }

        private void ResetAnchorLines()
        {
            if (BitmapChannel.AnchorLines.Any()) BitmapChannel.AnchorLines.Clear();

            _lastLineId = 0;

            _activeAnchorLine = GetNewAnchorLine();
            _activeAnchorLine.IsActive = true;

            BitmapChannel.AnchorLines.Add(ActiveAnchorLine);
        }

        private AnchorLine GetNewAnchorLine()
        {
            var anchorLine = new AnchorLine { Id = _lastLineId };
            _lastLineId++;
            return anchorLine;
        }
    }
}