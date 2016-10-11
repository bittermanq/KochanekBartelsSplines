using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using KochanekBartelsSplines.TestApp.Helpers.Interfaces;
using KochanekBartelsSplines.TestApp.Models;
using KochanekBartelsSplines.TestApp.ViewModels.Interfaces;
using KochanekBartelsSplines.TestApp.Wpf;

namespace KochanekBartelsSplines.TestApp.Helpers
{
    public class SplinesController : PropertyChangedImplementation, ISplinesController, ISplineSettingsContainer, IBitmapContainer
    {
        private readonly ISelectedAnchorPointGetter _selectedAnchorPointGetter;
        private readonly ILineInterpolator _lineInterpolator;

        private readonly List<AnchorLine> _anchorLines = new List<AnchorLine>();

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
        

        public SplinesController(ISelectedAnchorPointGetter selectedAnchorPointGetter, 
                                 ISplineControllerFactory splineControllerFactory,
                                 ILineInterpolator lineInterpolator)
        {
            _selectedAnchorPointGetter = selectedAnchorPointGetter;
            _lineInterpolator = lineInterpolator;

            SplineSettingsController = splineControllerFactory.Get(UpdateBitmapChannel);

            BitmapChannel = new BitmapChannel();

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
            if (_anchorLines.Count > 1)
            {
                _anchorLines.Remove(ActiveAnchorLine);
                ActiveAnchorLine = _anchorLines.First();
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

            _anchorLines.Add(ActiveAnchorLine);
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
                    ActiveAnchorLine = _anchorLines[i];
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

            foreach (var line in _anchorLines.Where(line => line.Points.Any() && line.Points.First() == selectedPoint))
            {
                line.IsClosed = !line.IsClosed;
            }
        }


        private void UpdateBitmapChannel()
        {
            BitmapChannel.Curves = _lineInterpolator.GetCurves(_anchorLines, SplineSettingsController).ToList();
            BitmapChannel.AnchorLines = _anchorLines;
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
            if (_anchorLines.Any())
                _anchorLines.Clear();

            _lastLineId = 0;

            _activeAnchorLine = GetNewAnchorLine();
            _activeAnchorLine.IsActive = true;

            _anchorLines.Add(ActiveAnchorLine);
        }

        private AnchorLine GetNewAnchorLine()
        {
            var anchorLine = new AnchorLine { Id = _lastLineId };
            _lastLineId++;
            return anchorLine;
        }
    }
}