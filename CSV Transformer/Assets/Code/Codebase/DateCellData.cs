#nullable enable

using System;
using System.Text.RegularExpressions;

namespace CSVTransformer.Codebase
{
    public sealed class DateCellData : CellData
    {
        private static Regex DatePattern { get; set; }

        private string Date { get; set; }
        private string Time { get; set; }

        private DateCellData(string date, string time)
        {
            Date = date;
            Time = time;
        }

        static DateCellData()
        {
            // language=regex
            var year_month_day_date_pattern = @"\d{4}-\d{2}-\d{2}";
            // language=regex
            var time_zone_pattern = @"[+-]\d{2}:\d{2}";
            // language=regex
            var time_pattern = @"\d{2}:\d{2}:\d{2}";
            // language=regex
            DatePattern = new
            (
                @"^(" +
                year_month_day_date_pattern +
                @")(T(" +
                time_pattern +
                @")" +
                time_zone_pattern +
                @")?$"
            );
        }

        public static CellData? Build(string field)
        {
            if (IsDateRecognized(field))
            {
                var parts = field.Split("T");
                var date = parts[0];
                var time = parts.Length > 1 ? parts[1] : "";
                return new DateCellData(date, time);
            }

            return null;


            static bool IsDateRecognized(string field)
            {
                return DatePattern.IsMatch(field);
            }
        }

        protected override bool IsGreaterThan(CellData other)
        {
            if (other is DateCellData date_cell)
            {
                var date_compare = Date.CompareTo(date_cell.Date);
                if (date_compare != 0)
                {
                    return date_compare > 0;
                }

                return Time.CompareTo(date_cell.Time) > 0;
            }

            throw new NotSupportedException();
        }

        protected override bool IsLessThan(CellData other)
        {
            if (other is DateCellData date_cell)
            {
                var date_compare = Date.CompareTo(date_cell.Date);
                if (date_compare != 0)
                {
                    return date_compare < 0;
                }

                return Time.CompareTo(date_cell.Time) < 0;
            }

            throw new NotSupportedException();
        }

        protected override CellData SumWith(CellData other)
        {
            if (other is DateCellData)
            {
                return new DateCellData(Date, "");
            }

            throw new NotSupportedException();
        }

        internal bool HasSameDateAs(DateCellData other)
        {
            return Date == other.Date;
        }

        internal DateCellData GetCopyWithDateOnly()
        {
            return new DateCellData(Date, "");
        }

        public override string ToString()
        {
            var time = (Time.Length > 0) ? $"T{Time}" : "";
            return Date + time;
        }
    }
}