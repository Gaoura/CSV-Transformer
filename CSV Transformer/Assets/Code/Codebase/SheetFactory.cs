#nullable enable

using System.Collections.Generic;

namespace CSVTransformer.Codebase
{
    public sealed class SheetFactory
    {
        private RowFactory RowFactory { get; set; } = new();

        public Sheet Build(List<string> csv_lines)
        {
            var sheet = new Sheet();
            foreach (var csv_line in csv_lines)
            {
                var row = RowFactory.Build(csv_line);
                sheet.Add(row);
            }

            return sheet;
        }
    }
}