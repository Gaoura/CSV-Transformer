#nullable enable

namespace CSVTransformer.Codebase
{
    public sealed class CellDataFactory
    {
        public CellData Build(string field)
        {
            CellData? cell = NumberCellData.Build(field);

            if (cell is null)
            {
                cell = DateCellData.Build(field);
            }

            return cell ?? new StringCellData(field);
        }
    }
}