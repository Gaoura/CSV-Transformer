#nullable enable

using System;
using System.Globalization;

namespace CSVTransformer.Codebase
{
    public class CellDataFactory
    {
        public CellData Build(string data)
        {
            var inspector = new FieldInspector();
            var type = inspector.GetTypeOf(data); 

            CellData cell = type switch
            {
                FieldType.Number => new NumberCellData(Convert.ToDouble(data, CultureInfo.InvariantCulture.NumberFormat)),
                _ => new StringCellData(data),
            };

            return cell;
        }
    }
}