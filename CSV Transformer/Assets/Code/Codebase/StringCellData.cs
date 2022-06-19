#nullable enable

namespace CSVTransformer.Codebase
{
    public class StringCellData : CellData
    {
        public string Data { get; private set; }

        public StringCellData(string data)
        {
            Data = data;
        }

        public override string ToString()
        {
            return Data;
        }
    }
}
