using System.Drawing;

namespace KochanekBartelsSplines.Helpers.Interfaces
{
    public interface ISplinesController
    {
        void AddOrSelectPoint(Point point);
        void DeleteActivePoint();
        void ResetParameters();
        void ClearLines();
        void AddAnchorLine();
        void DeleteAnchorLine();
        void MakeCurveActive(int obj);
        void MoveActivePoint(Point point);
        void SetLineClosed(Point point);

        void MouseDown(Point obj);
        void MouseMove(Point obj);
        void MouseDoubleClick(Point obj);
        void KeyDown();
    }
}