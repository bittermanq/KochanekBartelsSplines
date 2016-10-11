using System.Drawing;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using KochanekBartelsSplines.Helpers.Interfaces;

namespace KochanekBartelsSplines.ViewModels
{
    public class SplinesControllerViewModel : ISplinesControllerViewModel
    {
        public ISplinesController SplinesController { get; set; }

        public RelayCommand<Point> MouseDownCommand { get; private set; }
        public RelayCommand<Point> MouseMoveCommand { get; private set; }
        public RelayCommand<Point> MouseDoubleClickCommand { get; private set; }
        public RelayCommand KeyDownCommand { get; private set; }
        public RelayCommand ResetCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public RelayCommand AddCurveCommand { get; private set; }
        public RelayCommand DeleteCurveCommand { get; private set; }
        public RelayCommand<int> SelectCurveCommand { get; private set; }


        public SplinesControllerViewModel(ISplinesController splinesController)
        {
            SplinesController = splinesController;
            
            CreateCommands();
        }


        private void CreateCommands()
        {
            MouseDownCommand = new RelayCommand<Point>(MouseDown);
            MouseMoveCommand = new RelayCommand<Point>(MouseMove);
            MouseDoubleClickCommand = new RelayCommand<Point>(MouseDoubleClick);
            KeyDownCommand = new RelayCommand(KeyDown);
            ResetCommand = new RelayCommand(SplinesController.ResetParameters);
            ClearCommand = new RelayCommand(SplinesController.ClearLines);
            AddCurveCommand = new RelayCommand(SplinesController.AddAnchorLine);
            DeleteCurveCommand = new RelayCommand(SplinesController.DeleteAnchorLine);
            SelectCurveCommand = new RelayCommand<int>(SplinesController.MakeCurveActive);
        }


        private void MouseDown(Point point)
        {
            SplinesController.AddOrSelectPoint(point);
        }

        private void MouseMove(Point point)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                SplinesController.MoveActivePoint(point);
            }
        }

        private void MouseDoubleClick(Point point)
        {
            SplinesController.SetLineClosed(point);
        }

        private void KeyDown()
        {
            if (Keyboard.IsKeyDown(Key.Delete))
            {
                SplinesController.DeleteActivePoint();
            }
        }
    }
}