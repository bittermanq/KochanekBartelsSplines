using System.Collections.Generic;
using KochanekBartelsSplines.Models;

namespace KochanekBartelsSplines.Helpers.Interfaces
{
    public interface IFileWriter
    {
        void Save(IEnumerable<AnchorLine> anchorLines, string fileName);
    }
}