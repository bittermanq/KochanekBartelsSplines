using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using KochanekBartelsSplines.Helpers;
using KochanekBartelsSplines.Helpers.Interfaces;
using KochanekBartelsSplines.Models;
using KochanekBartelsSplines.ViewModels.Interfaces;


namespace KochanekBartelsSplines.ViewModels
{
    public class MainWindowViewModel : PropertyChangedImplementation, IMainWindowViewModel, ISplineController, IBitmapContainer
    {
        private readonly ILineInterpolator _lineInterpolator;

        private double _tension;
        public double Tension
        {
            get { return _tension; }
            set
            {
                _tension = value;
                UpdateBitmapChannel();
            }
        }

        private double _continuity;
        public double Continuity
        {
            get { return _continuity; }
            set
            {
                _continuity = value;
                UpdateBitmapChannel();
            }
        }

        private double _bias;
        public double Bias
        {
            get { return _bias; }
            set
            {
                _bias = value;
                UpdateBitmapChannel();
            }
        }
    
        private int _segments;
        public int Segments
        {
            get { return _segments; }
            set
            {
                _segments = value;
                UpdateBitmapChannel();
            }
        }

        public RelayCommand<Point> MouseDownCommand { get; private set; }
        public RelayCommand<Point> MouseMoveCommand { get; private set; }
        public RelayCommand<Point> MouseDoubleClickCommand { get; private set; }
        public RelayCommand KeyDownCommand { get; private set; }
        public RelayCommand ResetCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public RelayCommand OpenFileCommand { get; private set; }
        public RelayCommand SaveFileCommand { get; private set; }
        public RelayCommand SaveNewFileCommand { get; private set; }
        public RelayCommand AddCurveCommand { get; private set; }
        public RelayCommand DeleteCurveCommand { get; private set; }
        public RelayCommand<int> SelectCurveCommand { get; private set; }

        public BitmapChannel BitmapChannel { get; set; }


        private AnchorPoint _activePoint;
        
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

        private int _lastLineId;
        private String _fileName;


        public MainWindowViewModel(ILineInterpolator lineInterpolator)
        {
            _lineInterpolator = lineInterpolator;

            MouseDownCommand = new RelayCommand<Point>(MouseDown);
            MouseMoveCommand = new RelayCommand<Point>(MouseMove);
            MouseDoubleClickCommand = new RelayCommand<Point>(MouseDoubleClick);
            KeyDownCommand = new RelayCommand(KeyDown);
            ResetCommand = new RelayCommand(ResetParameters);
            ClearCommand = new RelayCommand(ClearLines);
            OpenFileCommand = new RelayCommand(OpenFile);
            SaveFileCommand = new RelayCommand(SaveFile);
            SaveNewFileCommand = new RelayCommand(SaveNewFile);
            AddCurveCommand = new RelayCommand(AddAnchorLine);
            DeleteCurveCommand = new RelayCommand(DeleteAnchorLine);
            SelectCurveCommand = new RelayCommand<int>(MakeCurveActive);

            BitmapChannel = new BitmapChannel();
            ResetAnchorLines();
            
            BitmapChannel.Curves = _lineInterpolator.GetCurves(BitmapChannel.AnchorLines, Tension, Continuity, Bias, Segments);

            ResetParameters();
        }

        
        private AnchorLine GetNewAnchorLine()
        {
            var anchorLine = new AnchorLine { Id = _lastLineId };
            _lastLineId++;
            return anchorLine;
        }

        private AnchorPoint GetSelectedPointOrDefault(Point point)
        {
            const int radius = 5;

            return ActiveAnchorLine.Points.FirstOrDefault(anchorPoint => Math.Sqrt(Math.Pow((point.X - anchorPoint.Position.X), 2) + Math.Pow((point.Y - anchorPoint.Position.Y), 2)) <= radius);
        }
        
        private void AddAnchorPoint(Point point)
        {
            if(_activePoint != null) _activePoint.IsActive = false;

            _activePoint = new AnchorPoint(point, true);

            ActiveAnchorLine.Points.Add(_activePoint);
        }

        private void DeleteActivePoint()
        {
            if (_activePoint != null)
            {
                ActiveAnchorLine.Points.Remove(_activePoint);
                UpdateBitmapChannel();
            }
        }

        private void AddOrSelectPoint(Point point)
        {
            var selectedPoint = GetSelectedPointOrDefault(point);

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

        private void MoveActivePoint(Point point)
        {
            _activePoint.Position = point;
            UpdateBitmapChannel();
        }

        private void SetLineClosed(Point point)
        {
            var selectedPoint = GetSelectedPointOrDefault(point);

            if (selectedPoint == null) return;

            foreach (var line in BitmapChannel.AnchorLines.Where(line => line.Points.Any() && line.Points.First() == selectedPoint))
            {
                line.IsClosed = !line.IsClosed;
            }
        }

        private void UpdateBitmapChannel()
        {
            BitmapChannel.Curves = _lineInterpolator.GetCurves(BitmapChannel.AnchorLines, Tension, Continuity, Bias, Segments);
            RaisePropertyChanged(() => BitmapChannel);
        }

        private void ClearLines()
        {
            ResetAnchorLines();
            UpdateBitmapChannel();
        }

        private void ResetAnchorLines()
        {
            if (BitmapChannel.AnchorLines.Any()) BitmapChannel.AnchorLines.Clear();

            _lastLineId = 0;

            _activeAnchorLine = GetNewAnchorLine();
            _activeAnchorLine.IsActive = true;

            BitmapChannel.AnchorLines.Add(ActiveAnchorLine);
        }

        private void ResetParameters()
        {
            Tension = 0;
            Continuity = 0;
            Bias = 0;
            Segments = 10;

            RaisePropertyChanged(() => Tension);
            RaisePropertyChanged(() => Continuity);
            RaisePropertyChanged(() => Bias);
            RaisePropertyChanged(() => Segments);

            UpdateBitmapChannel();
        }

        private void SaveNewFile()
        {
            var fileName = ShowSaveFileDialog();
            if (fileName != null) _fileName = fileName;
            else return;

            SavePointsToFile(BitmapChannel.AnchorLines);
        }

        private void SaveFile()
        {
            if (String.IsNullOrEmpty(_fileName))
            {
                SaveNewFile();
                return;
            }

            SavePointsToFile(BitmapChannel.AnchorLines);
            
        }

        private void OpenFile()
        {
            var fileName = ShowOpenFileDialog();

            if (fileName != null)
            {
                _fileName = fileName;
                ReadPointsFromFile(BitmapChannel.AnchorLines);

                if(!BitmapChannel.AnchorLines.Any()) ResetAnchorLines();
                ActiveAnchorLine = BitmapChannel.AnchorLines.First();
                UpdateBitmapChannel();
            }
        }

        private String ShowSaveFileDialog()
        {
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
            return null;
        }

        private String ShowOpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            return null;
        }

        private void DeleteAnchorLine()
        {
            if (BitmapChannel.AnchorLines.Count > 1)
            {
                BitmapChannel.AnchorLines.Remove(ActiveAnchorLine);
                ActiveAnchorLine = BitmapChannel.AnchorLines.First();
                UpdateBitmapChannel();
            }
        }

        private void AddAnchorLine()
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

        private void MakeCurveActive(int id)
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


        private void MouseDown(Point point)
        {
            AddOrSelectPoint(point);
        }
        
        private void MouseMove(Point point)
        {
            if(Mouse.LeftButton == MouseButtonState.Pressed)
            {
                MoveActivePoint(point);
            }
        }

        private void MouseDoubleClick(Point point)
        {
            SetLineClosed(point);
        }
        
        private void KeyDown()
        {
            if(Keyboard.IsKeyDown(Key.Delete))
            {
                DeleteActivePoint();
            }
        }












        private void SavePointsToFile(IEnumerable<AnchorLine> anchorLines)
        {
            var fileStream = new FileStream(_fileName, FileMode.Truncate);
            var streamWriter = new StreamWriter(fileStream);
            
            foreach (var anchorLine in anchorLines)
            {
                streamWriter.Write(anchorLine.Id);
                streamWriter.Write(':');
                if(anchorLine.Points.Any()) streamWriter.Write('|');

                foreach (var point in anchorLine.Points)
                {
                    streamWriter.Write(point.Position.X);
                    streamWriter.Write(';');   
                    streamWriter.Write(point.Position.Y);
                    streamWriter.Write('|');   
                }

                if (anchorLine.IsClosed) streamWriter.Write('c');   
                streamWriter.Write('\r');   
            }

            streamWriter.Dispose();
            fileStream.Dispose();
        }

        private void ReadPointsFromFile(List<AnchorLine> anchorLines)
        {
            var fileStream = new FileStream(_fileName, FileMode.Truncate);
            var streamReader = new StreamReader(fileStream);

            var readLine = streamReader.ReadLine();
        }
    }
}