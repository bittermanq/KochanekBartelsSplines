using System.Collections.Generic;
using KochanekBartelsSplines.TestApp.Models;

namespace KochanekBartelsSplines.TestApp.Helpers.Interfaces
{
    public interface IFileWriter
    {
        void Save(IEnumerable<AnchorLine> anchorLines, string fileName);
    }
}