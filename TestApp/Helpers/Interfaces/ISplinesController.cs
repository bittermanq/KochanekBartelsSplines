using System.Drawing;
using KochanekBartelsSplines.TestApp.Models;

namespace KochanekBartelsSplines.TestApp.Helpers.Interfaces
{
    public interface ISplinesController
    {
        AnchorPoint ActivePoint { get; set; }

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