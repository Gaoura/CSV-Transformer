#nullable enable

using System.Collections.Generic;

namespace CSVTransformer.Codebase
{
    public class Row
    {
        private List<CellData> Cells { get; set; } = new();
        public byte CellCount
            => (byte)Cells.Count;

        public CellData this[byte index]
            => Cells[index];


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
    }
}
