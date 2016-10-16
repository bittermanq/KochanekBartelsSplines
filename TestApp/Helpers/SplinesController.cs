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

        private int _lastLineId;

        private AnchorPoint _activePoint;
        public AnchorPoint ActivePoint
        {
            get { return _activePoint; }
            set
            {
                _activePoint = value;

                RaisePropertyChanged(() => ActivePoint);
            }
        }

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
            if (ActivePoint != null)
            {
                ActiveAnchorLine.Points.Remove(ActivePoint);
                UpdateBitmapChannel();
            }
        }

        public void AddOrSelectPoint(Point point)
        {
            var selectedPoint = _selectedAnchorPointGetter.Get(point, ActiveAnchorLine);

            if (selectedPoint == null) AddAnchorPoint(point);
            else
            {
                if (selectedPoint != ActivePoint)
                {
                    if (ActivePoint != null)
                        ActivePoint.IsActive = false;

                    selectedPoint.IsActive = true;
                    ActivePoint = selectedPoint;
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
            if (ActivePoint != null)
            {
                ActivePoint.Tension = 0;
                ActivePoint.Continuity = 0;
                ActivePoint.Bias = 0;

                RaisePropertyChanged(() => ActivePoint.Tension);
                RaisePropertyChanged(() => ActivePoint.Continuity);
                RaisePropertyChanged(() => ActivePoint.Bias);
            }

            SplineSettingsController.Segments = 10;
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

            if (ActivePoint != null)
            {
                ActivePoint.IsActive = false;
                ActivePoint = null;
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
            if (ActivePoint != null)
            {
                ActivePoint.Position = point;
                UpdateBitmapChannel();
            }
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

        private void AddAnchorPoint(Point point)
        {
            if (ActivePoint != null)
                ActivePoint.IsActive = false;

            ActivePoint = new AnchorPoint(point, true, UpdateBitmapChannel);

            ActiveAnchorLine.Points.Add(ActivePoint);
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
            var anchorLine = new AnchorLine(new ColorContainer(Color.Black, UpdateBitmapChannel)) { Id = _lastLineId };
            _lastLineId++;
            return anchorLine;
        }


        private void UpdateBitmapChannel()
        {
            BitmapChannel.Curves = _lineInterpolator.GetCurves(_anchorLines, SplineSettingsController).ToList();
            BitmapChannel.AnchorLines = _anchorLines;
            RaisePropertyChanged(() => BitmapChannel);
        }
    }
}