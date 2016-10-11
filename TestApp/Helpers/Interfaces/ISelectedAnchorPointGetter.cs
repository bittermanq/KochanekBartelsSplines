using System.Drawing;
using KochanekBartelsSplines.TestApp.Models;

namespace KochanekBartelsSplines.TestApp.Helpers.Interfaces
{
    public interface ISelectedAnchorPointGetter
    {
        AnchorPoint Get(Point point, AnchorLine anchorLine);
    }
}