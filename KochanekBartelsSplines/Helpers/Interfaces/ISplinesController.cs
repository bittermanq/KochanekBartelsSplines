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
    }
}