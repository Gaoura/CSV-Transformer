#nullable enable
using System.Collections.Generic;

namespace CSVTransformer.Codebase
{
    public class Transformer
    {
        public List<List<CellData>> ExtractColumns(List<List<CellData>> sheet, HashSet<byte> columns_to_extract)
        {
            var new_sheet = new List<List<CellData>>();
            foreach (var row in sheet)
            {
                var new_row = new List<CellData>();
                for (byte i = 0; i < row.Count; ++i)
                {
                    if (columns_to_extract.Contains((byte)(i + 1)))
                    {
                        new_row.Add(row[i]);
                    }
                }

                new_sheet.Add(new_row);
            }

            return new_sheet;
        }
    }
}