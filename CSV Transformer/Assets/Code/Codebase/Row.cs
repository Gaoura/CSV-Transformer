#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CSVTransformer.Codebase
{
    public sealed class Row : IEnumerable
    {
        public byte CellCount
            => (byte)Cells.Count;

        private List<CellData> Cells { get; set; } = new();

        internal CellData this[CellPosition column_number]
            => Cells[column_number.AsArrayIndex];

        internal Row() { }

        public Row(List<CellData> cells)
        {
            Cells.AddRange(cells);
        }

        public static Row operator +(Row row1, Row row2)
        {
            var new_row = new Row();
            for (int i = 0; i < row1.CellCount; ++i)
            {
                new_row.Add(row1.Cells[i] + row2.Cells[i]);
            }

            return new_row;
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

        public int CompareTo(Row other, CellPosition column_number)
        {
            if (this[column_number] < other[column_number])
            {
                return -1;
            }
            else if (this[column_number] > other[column_number])
            {
                return 1;
            }
            else
            {
                return 0;
            }
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

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Cells).GetEnumerator();
        }

        internal Row ReplaceCell(CellPosition date_column_number, CellData new_cell)
        {
            var cells = new List<CellData>();

            for (ushort k = 1; k <= CellCount; ++k)
            {
                var current_cell = this[new CellPosition(k)];
                if (date_column_number.AsArrayIndex + 1 == k)
                {
                    current_cell = new_cell;
                }

                cells.Add(current_cell);
            }

            return new Row(cells);
        }

        internal List<Type> GetCellTypes()
        {
            var types = new List<Type>();
            foreach (var cell in Cells)
            {
                types.Add(cell.GetType());
            }
            return types;
        }
    }
}
