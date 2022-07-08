#nullable enable

using System.Collections.Generic;
using System.Text;
using System;

using UnityEngine.Assertions;

namespace CSVTransformer.Codebase
{
    public class Sheet
    {
        private List<Row> Rows { get; set; } = new();
        private List<Type> ColumnTypes { get; set; } = new();

        public byte RowCount
            => (byte)Rows.Count;

        public Row this[CellPosition row_number]
            => Rows[row_number.AsArrayIndex];

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
            foreach (var row in rows)
            {
                Add(row);
            }
        }

        internal void Add(Row row)
        {
            if (ColumnTypes.Count == 0)
            {
                Assert.IsTrue(Rows.Count == 0);
                ColumnTypes.AddRange(row.GetCellTypes());
            }
            else if (!RowMatchesColumTypes(ColumnTypes, row))
            {
                throw new ArgumentException();
            }

            Rows.Add(row);

            static bool RowMatchesColumTypes(List<Type> column_types, Row row)
            {
                if (column_types.Count != row.CellCount)
                {
                    return false;
                }

                int i = 0;
                foreach (var cell in row)
                {
                    if (cell.GetType() != column_types[i])
                    {
                        return false;
                    }
                    ++i;
                }

                return true;
            }
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

        public Sheet Sort(CellPosition column_number)
        {
            var sorted_rows = new List<Row>(Rows);
            sorted_rows.Sort( (x, y) => x.CompareTo(y, column_number) );
            return new Sheet(sorted_rows);
        }

        public Sheet SumByDate(DateCellColumnPosition date_cell_column_position)
        {
            if (Rows.Count < 2)
            {
                return this;
            }

            var date_cell_position = date_cell_column_position.Position;

            var sheet = new Sheet();

            for (int i = 0; i < RowCount; ++i)
            {
                var last_same_date_row_index = GetLastIndexOfRowsWithSameDate(i, Rows, date_cell_position);
                var rows_with_same_date = Rows.GetRange(i, last_same_date_row_index - i + 1);
                var new_row = SumAll(rows_with_same_date);
                if (rows_with_same_date.Count == 1)
                {
                    var date_cell = KeepOnlyDatePartInDateCell(new_row, date_cell_position);
                    new_row = new_row.ReplaceCell(date_cell_position, date_cell);
                }

                sheet.Add(new_row);

                i = last_same_date_row_index;
            }

            return sheet;


            static int GetLastIndexOfRowsWithSameDate
            (
                int reference_row_index, 
                List<Row> rows, 
                CellPosition date_column_number
            )
            {
                var reference_row = rows[reference_row_index];
                int last_same_date_row_index = reference_row_index;

                for (int i = reference_row_index + 1; i < rows.Count; ++i)
                {
                    var row = rows[i];
                    var date_cell1 = (DateCellData)reference_row[date_column_number];
                    var date_cell2 = (DateCellData)row[date_column_number];

                    if (!date_cell1.HasSameDateAs(date_cell2))
                    {
                        last_same_date_row_index = i - 1;
                        break;
                    }
                }

                return last_same_date_row_index;
            }

            static Row SumAll(List<Row> rows)
            {
                var new_row = rows[0];

                for (int i = 1; i < rows.Count; ++i)
                {
                    new_row += rows[i];
                }

                return new_row;
            }


            static DateCellData KeepOnlyDatePartInDateCell(Row row, CellPosition date_column_number)
            {
                var date_cell = (DateCellData)row[date_column_number];
                return date_cell.GetCopyWithDateOnly();
            }
        }
        

        public DateCellColumnPositionCollection GetAllDateCellColumnPositions()
        {
            var positions = new List<DateCellColumnPosition>();
            for (int i = 0; i < ColumnTypes.Count; ++i)
            {
                if (ColumnTypes[i] == typeof(DateCellData))
                {
                    positions.Add(new DateCellColumnPositionInitializer(new CellPosition((ushort)(i + 1))));
                }
            }

            return new DateCellColumnPositionCollectionInitializer(positions);
        }

        public class DateCellColumnPositionCollection
        {
            private List<DateCellColumnPosition> Positions { get; set; }
            
            public DateCellColumnPosition this[int i] => Positions[i];

            protected DateCellColumnPositionCollection(List<DateCellColumnPosition> positions)
            {
                Positions = positions;
            }
        }
        private class DateCellColumnPositionCollectionInitializer : DateCellColumnPositionCollection
        {
            public DateCellColumnPositionCollectionInitializer(List<DateCellColumnPosition> positions)
                : base(positions)
            { }
        }

        public class DateCellColumnPosition
        {
            public CellPosition Position { get; private set; }

            protected DateCellColumnPosition(CellPosition position) 
            {
                Position = position;
            }
        }

        private class DateCellColumnPositionInitializer : DateCellColumnPosition
        {
            public DateCellColumnPositionInitializer(CellPosition position)
                : base (position)
            { }
        }

    }
}