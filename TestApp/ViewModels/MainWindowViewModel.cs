using System;
using System.Windows.Forms;
using GalaSoft.MvvmLight.Command;
using KochanekBartelsSplines.TestApp.Helpers.Interfaces;
using KochanekBartelsSplines.TestApp.ViewModels.Interfaces;
using KochanekBartelsSplines.TestApp.Wpf;

namespace KochanekBartelsSplines.TestApp.ViewModels
{
    public class MainWindowViewModel : PropertyChangedImplementation, IMainWindowViewModel, ISplinesControllerVMContainer
    {
        private readonly IFileProvider _fileProvider;
        
        private string _fileName;
        
        public ISplinesControllerViewModel SplinesControllerViewModel { get; set; }

        public RelayCommand OpenFileCommand { get; private set; }
        public RelayCommand SaveFileCommand { get; private set; }
        public RelayCommand SaveNewFileCommand { get; private set; }
        
        
        public MainWindowViewModel(IFileProvider fileProvider, ISplinesControllerViewModel splinesControllerViewModel)
        {
            _fileProvider = fileProvider;

            SplinesControllerViewModel = splinesControllerViewModel;
            
            CreateCommands();
        }


        private void CreateCommands()
        {
            OpenFileCommand = new RelayCommand(OpenFile);
            SaveFileCommand = new RelayCommand(SaveFile);
            SaveNewFileCommand = new RelayCommand(SaveNewFile);
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
            if (String.IsNullOrEmpty(_fileName))
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