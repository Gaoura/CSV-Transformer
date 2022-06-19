#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace CSVTransformer.Codebase
{
    public class Sheet
    {
        private List<Row> Rows { get; set; } = new();

        public byte RowCount
            => (byte)Rows.Count;

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

        internal void Add(Row row)
        {
            Rows.Add(row);
        }

        public override string ToString()
        {
            var string_builder = new StringBuilder();
            var row_count = RowCount;

            if (row_count > 0)
            {
                string_builder.Append(Rows[0].ToString());
            }

            for (var i = 1; i < row_count; ++i)
            {
                string_builder.Append("\n");
                string_builder.Append(Rows[i].ToString());
            }

            return string_builder.ToString();
        }
    }
}