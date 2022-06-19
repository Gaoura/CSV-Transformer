#nullable enable

using System;
using System.Collections.Generic;

namespace CSVTransformer.Codebase
{
    public class Sheet
    {
        private List<Row> Rows { get; set; } = new();

        public Row this[byte index]
            => Rows[index];

        public Sheet() { }

        public Sheet ExtractColumns(HashSet<byte> columns_to_extract)
        {
            var new_sheet = new Sheet();

            foreach (var row in Rows)
            {
                var new_row = row.ExtractColumns(columns_to_extract);
                new_sheet.Add(new_row);
            }

            return new_sheet;
        }

        public Sheet(List<Row> rows)
        {
            Rows.AddRange(rows);
        }


        internal void ForEach(Action<Row> action)
        {
            foreach (var row in Rows)
            {
                action(row);
            }
        }

        internal void Add(Row row)
        {
            Rows.Add(row);
        }
    }
}