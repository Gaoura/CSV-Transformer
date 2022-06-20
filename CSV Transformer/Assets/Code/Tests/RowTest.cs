#nullable enable

using System.Collections.Generic;

using NUnit.Framework;

using CSVTransformer.Codebase;

namespace CSVTransformer.Tests.Unit
{
    public class RowTest
    {
        [Test]
        public void ExtractColumns_ShouldOnlyKeepCellsThatAskedFor()
        {
            var row = new Row
            (
                new List<CellData>()
                {
                new StringCellData("Cell1"),
                new StringCellData("Cell2"),
                new StringCellData("Cell3")
                }
            );

            var new_row = row.ExtractColumns(new HashSet<byte>() { 1, 3 });

            Assert.That
            (
                new_row.CellCount,
                Is.EqualTo(2)
            );
            Assert.That
            (
                new_row.ToString(),
                Is.EqualTo("Cell1,Cell3")
            );
        }

        [Test]
        public void ToString_ShouldDisplayAllCellsInCSVFormat()
        {
            var row = new Row
            (
                new List<CellData>()
                {
                new StringCellData("Cell1"),
                new StringCellData("Cell2"),
                new StringCellData("Cell3")
                }
            );

            Assert.That
            (
                row.ToString(),
                Is.EqualTo("Cell1,Cell2,Cell3")
            );
        }

        [Test]
        public void CompareTo()
        {
            var row1 = new Row
            (
                new List<CellData>()
                {
                    new StringCellData("2022-05-11"),
                    new StringCellData("Cell12")
                }
            );
            var row2 = new Row
            (
                new List<CellData>()
                {
                    new StringCellData("2022-05-09"),
                    new StringCellData("Cell22")
                }
            );

            Assert.That
            (
                row1.CompareTo(row2, 1),
                Is.EqualTo(1)
            );
            Assert.That
            (
                row1.CompareTo(row1, 1),
                Is.EqualTo(0)
            );
            Assert.That
            (
                row2.CompareTo(row1, 1),
                Is.EqualTo(-1)
            );
        }
    }
}