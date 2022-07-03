#nullable enable

using System.IO;

namespace CSVTransformer.Codebase
{
    public class FileSaver
    {
        public void Save(Sheet sheet, StreamWriter file)
        {
            using (file)
            {
                file.Write(sheet.ToString());
                file.Flush();
            }
        }
    }
}