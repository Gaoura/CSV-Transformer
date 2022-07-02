#nullable enable

namespace CSVTransformer.Codebase
{
    public sealed class RowFactory
    {
        private FieldExtractor Extractor { get; set; } = new();

        public Row Build(string csv_line)
        {
            var row = new Row();
            var fields = Extractor.Extract(csv_line);

            var cell_factory = new CellDataFactory();
            foreach (var field in fields)
            {
                var cell = cell_factory.Build(field);
                row.Add(cell);
            }

            return row;
        }
    }
}