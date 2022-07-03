#nullable enable

using System.Collections.Generic;
using System.IO;

namespace CSVTransformer.Codebase
{
    public sealed class FileLoader
    {
        public List<string> Load(StreamReader file)
        {
            var loaded_lines = new List<string>();
            using (file)
            {
                string line = file.ReadLine();

                while (line != null)
                {
                    loaded_lines.Add(line);
                    line = file.ReadLine();
                }
            }

            return loaded_lines;
        }
    }
}