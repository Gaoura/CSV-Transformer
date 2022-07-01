#nullable enable

using System;

namespace CSVTransformer.Codebase
{
    public class NumberCellData : CellData
    {
        private double Data { get; set; }

        public NumberCellData(double data)
        {
            Data = data;
        }

        protected override bool IsGreaterThan(CellData other)
        {
            if (other is NumberCellData number_cell)
            {
                return Data.CompareTo(number_cell.Data) > 0;
            }

            throw new NotSupportedException();
        }

        protected override bool IsLessThan(CellData other)
        {
            if (other is NumberCellData number_cell)
            {
                return Data.CompareTo(number_cell.Data) < 0;
            }

            throw new NotSupportedException();
        }
    }
}
