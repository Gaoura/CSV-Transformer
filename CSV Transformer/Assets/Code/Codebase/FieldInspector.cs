#nullable enable

using System.Text.RegularExpressions;

namespace CSVTransformer.Codebase
{
    public class FieldInspector
    {
        public FieldType GetTypeOf(string field)
        {
            if (IsNumberRecognized(field))
            {
                return FieldType.Number;
            }
            else if (IsDateRecognized(field))
            {
                return FieldType.Date;
            }

            return FieldType.String;

            static bool IsNumberRecognized(string field)
            {
                Regex number_pattern = new(@"^\d+(\.\d+)?$");
                return number_pattern.IsMatch(field);
            }

            static bool IsDateRecognized(string field)
            {
                // language=regex
                string year_month_day_date_pattern = @"\d{4}-\d{2}-\d{2}";
                // language=regex
                string time_zone_pattern = @"[+-]\d{2}:\d{2}";
                // language=regex
                string time_pattern = @"\d{2}:\d{2}:\d{2}";
                // language=regex
                Regex date_pattern = new
                (
                    @"^" + 
                    year_month_day_date_pattern +
                    @"(T" + 
                    time_pattern + 
                    time_zone_pattern +
                    @")?$"
                );
                return date_pattern.IsMatch(field);
            }
        }
    }
}