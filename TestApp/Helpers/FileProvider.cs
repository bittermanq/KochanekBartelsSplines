using System.Collections.Generic;
using System.IO;
using System.Linq;
using KochanekBartelsSplines.TestApp.Helpers.Interfaces;
using KochanekBartelsSplines.TestApp.Models;

namespace KochanekBartelsSplines.TestApp.Helpers
{
    public class FileProvider : IFileProvider
    {
        public void Save(IEnumerable<AnchorLine> anchorLines, string fileName)
        {
            var fileStream = new FileStream(fileName, FileMode.Truncate);

            using (var streamWriter = new StreamWriter(fileStream))
            {
                foreach (var anchorLine in anchorLines)
                {
                    streamWriter.Write(anchorLine.Id);
                    streamWriter.Write(':');
                    if (anchorLine.Points.Any()) streamWriter.Write('|');

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
            }
        }

        public void Read(string filename)
        {
            
        }
    }
}