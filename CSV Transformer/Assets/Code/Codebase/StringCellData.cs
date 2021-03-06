#nullable enable

using System;

namespace CSVTransformer.Codebase
{
    public class StringCellData : CellData
    {
        private string Data { get; set; }

        public StringCellData(string data)
        {
            Data = data;
        }

        public override string ToString()
        {
            return Data;
        }

        protected override bool IsGreaterThan(CellData other)
        {
            if (other is StringCellData string_cell)
            {
                return Data.CompareTo(string_cell.Data) > 0;
            }

            throw new NotSupportedException();
        }

        protected override bool IsLessThan(CellData other)
        {
            if (other is StringCellData string_cell)
            {
                return Data.CompareTo(string_cell.Data) < 0;
            }

            throw new NotSupportedException();
        }

        protected override CellData SumWith(CellData other)
        {
            if (other is StringCellData string_cell)
            {
                return new StringCellData(Data + string_cell.Data);
            }

            throw new NotSupportedException();
        }
    }
}
