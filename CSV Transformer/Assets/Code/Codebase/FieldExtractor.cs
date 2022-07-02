#nullable enable

using System.Collections.Generic;

namespace CSVTransformer.Codebase
{
    public sealed class FieldExtractor
    {
        public List<string> Extract(string csv_line)
        {
            var fields = new List<string>(csv_line.Split(","));
            return fields;
        }
    }
}