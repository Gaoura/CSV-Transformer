#nullable enable

namespace CSVTransformer.Codebase
{
    public abstract class CellData
    {
        public static bool operator >(CellData cell1, CellData cell2)
            => cell1.IsGreaterThan(cell2);

        public static bool operator <(CellData cell1, CellData cell2)
            => cell1.IsLessThan(cell2);

        protected abstract bool IsGreaterThan(CellData other);

        protected abstract bool IsLessThan(CellData other);
}
}