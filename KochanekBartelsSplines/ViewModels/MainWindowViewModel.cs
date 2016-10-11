using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GalaSoft.MvvmLight.Command;
using KochanekBartelsSplines.Helpers;
using KochanekBartelsSplines.Helpers.Interfaces;
using KochanekBartelsSplines.ViewModels.Interfaces;
using static System.String;


namespace KochanekBartelsSplines.ViewModels
{
    public class MainWindowViewModel : PropertyChangedImplementation, IMainWindowViewModel
    {
        private readonly IFileProvider _fileProvider;
        
        private string _fileName;
        
        public ISplinesController SplinesController { get; set; }


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

        
        public MainWindowViewModel(IFileProvider fileProvider, ISplinesController splinesController)
        {
            _fileProvider = fileProvider;

            SplinesController = splinesController;
            
            CreateCommands();
        }


        private void CreateCommands()
        {
            MouseDownCommand = new RelayCommand<Point>(SplinesController.MouseDown);
            MouseMoveCommand = new RelayCommand<Point>(SplinesController.MouseMove);
            MouseDoubleClickCommand = new RelayCommand<Point>(SplinesController.MouseDoubleClick);
            KeyDownCommand = new RelayCommand(SplinesController.KeyDown);
            ResetCommand = new RelayCommand(SplinesController.ResetParameters);
            ClearCommand = new RelayCommand(SplinesController.ClearLines);
            OpenFileCommand = new RelayCommand(OpenFile);
            SaveFileCommand = new RelayCommand(SaveFile);
            SaveNewFileCommand = new RelayCommand(SaveNewFile);
            AddCurveCommand = new RelayCommand(SplinesController.AddAnchorLine);
            DeleteCurveCommand = new RelayCommand(SplinesController.DeleteAnchorLine);
            SelectCurveCommand = new RelayCommand<int>(SplinesController.MakeCurveActive);
        }   

        
        private void SaveNewFile()
        {
            var fileName = ShowSaveFileDialog();
            if (fileName != null) _fileName = fileName;
            else return;

            SavePointsToFile();
        }

        private void SaveFile()
        {
            if (IsNullOrEmpty(_fileName))
            {
                SaveNewFile();
                return;
            }

            SavePointsToFile();
            
        }

        private void OpenFile()
        {
            var fileName = ShowOpenFileDialog();

            if (fileName != null)
            {
                _fileName = fileName;
                ReadPointsFromFile();
            }
        }

        private string ShowSaveFileDialog()
        {
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
            return null;
        }

        private string ShowOpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            return null;
        }

        private void SavePointsToFile()
        {
            //_fileProvider.Save(anchorLines, _fileName);
        }

        private void ReadPointsFromFile()
        {
            //_fileProvider.Read("FileName");
        }
    }
}