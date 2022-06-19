using System.Collections.Generic;

using NUnit.Framework;

using CSVTransformer.Codebase;


namespace CSVTransformer.Tests.Unit
{
    public class SheetTest
    {
        [Test]
        public void ExtractColumns()
        {
            var row1 = new Row
            (
                new List<CellData>()
                {
                    new StringCellData("Column11"),
                    new StringCellData("Column12"),
                    new StringCellData("Column13")
                }
            );
            var row2 = new Row
            (
                new List<CellData>()
                {
                    new StringCellData("Column21"),
                    new StringCellData("Column22"),
                    new StringCellData("Column23")
                }
            );
            var sheet = new Sheet
            (
                new List<Row>() 
                { 
                    row1, 
                    row2 
                }
            );
            var new_sheet = sheet.ExtractColumns(new HashSet<byte>() { 1, 3 });

            Assert.That
            (
                new_sheet[0].CellCount,
                Is.EqualTo(2)
            );
            Assert.That
            (
                new_sheet[0][0].ToString(),
                Is.EqualTo("Column11")
            );
            Assert.That
            (
                new_sheet[0][1].ToString(),
                Is.EqualTo("Column13")
            );
            Assert.That
            (
                new_sheet[1][0].ToString(),
                Is.EqualTo("Column21")
            );
            Assert.That
            (
                new_sheet[1][1].ToString(),
                Is.EqualTo("Column23")
            );
        }

    }
}
