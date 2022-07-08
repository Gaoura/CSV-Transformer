#nullable enable

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CSVTransformer.Codebase
{
    public class NumberCellData : CellData
    {
        private double Data { get; set; }

        private NumberCellData(double data)
        {
            Data = data;
        }

        public static CellData? Build(string field)
        {
            if (IsNumberRecognized(field))
            {
                return new NumberCellData(Convert.ToDouble(field, CultureInfo.InvariantCulture.NumberFormat));
            }

            return null;


            static bool IsNumberRecognized(string field)
            {
                Regex number_pattern = new(@"^\d+(\.\d+)?$");
                return number_pattern.IsMatch(field);
            }
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

        protected override CellData SumWith(CellData other)
        {
            if (other is NumberCellData number_cell)
            {
                return new NumberCellData(Data + number_cell.Data);
            }

            throw new NotSupportedException();
        }

        public override string ToString()
        {
            return Data.ToString().Replace(',', '.');
        }
    }
}
