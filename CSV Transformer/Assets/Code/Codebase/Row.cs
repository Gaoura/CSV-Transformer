#nullable enable

using System.Collections.Generic;
using System.Text;

namespace CSVTransformer.Codebase
{
    public class Row
    {
        private List<CellData> Cells { get; set; } = new();
        public byte CellCount
            => (byte)Cells.Count;


        internal Row() { }

        public Row(List<CellData> cells)
        {
            Cells.AddRange(cells);
        }

        public Row ExtractColumns(HashSet<byte> columns_to_extract)
        {
            var new_row = new Row();
            var cell_count = CellCount;

            for (byte i = 0; i < cell_count; ++i)
            {
                if (columns_to_extract.Contains((byte)(i + 1)))
                {
                    new_row.Add(Cells[i]);
                }
            }

            return new_row;
        }

        internal void Add(CellData cell)
        {
            Cells.Add(cell);
        }

        public override string ToString()
        {
            var string_builder = new StringBuilder();
            var cell_count = CellCount;

            if (cell_count > 0)
            {
                string_builder.Append(Cells[0].ToString());
            }

            for (var i = 1; i < cell_count; ++i)
            {
                string_builder.Append(",");
                string_builder.Append(Cells[i].ToString());
            }

            return string_builder.ToString();
        }
    }
}
