using System.Collections.Generic;

using NUnit.Framework;

using CSVTransformer.Codebase;


namespace CSVTransformer.Tests.Unit
{
    public class SheetTest
    {
        [Test]
        public void ExtractColumns_ShouldKeepTheSameNumberOfRows()
        {
            var row1 = new Row
            (
                new List<CellData>()
                {
                    new StringCellData("Cell11"),
                    new StringCellData("Cell12"),
                    new StringCellData("Cell13")
                }
            );
            var row2 = new Row
            (
                new List<CellData>()
                {
                    new StringCellData("Cell21"),
                    new StringCellData("Cell22"),
                    new StringCellData("Cell23")
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
                new_sheet.RowCount,
                Is.EqualTo(2)
            );
        }

        [Test]
        public void ToString_ShouldDisplayEachRowOnItsOwnLine()
        {
            var row1 = new Row
            (
                new List<CellData>()
                {
                    new StringCellData("Cell11"),
                    new StringCellData("Cell12")
                }
            );
            var row2 = new Row
            (
                new List<CellData>()
                {
                    new StringCellData("Cell21"),
                    new StringCellData("Cell22")
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

            Assert.That
            (
                sheet.ToString(),
                Is.EqualTo
                (
                    row1.ToString() + "\n" +
                    row2.ToString()
                )
            );
        }


        [Test]
        public void SortByDate_ShouldKeepTheSameNumberOfRows()
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
            var sheet = new Sheet
            (
                new List<Row>()
                {
                    row1,
                    row2
                }
            );

            var new_sheet = sheet.Sort(new CellPosition(1));

            Assert.That
            (
                new_sheet.RowCount,
                Is.EqualTo(2)
            );
        }


        [Test]
        public void SortByDate_ShouldSortRowsByChronologicalOrderOnOneColumn()
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
            var sheet = new Sheet
            (
                new List<Row>()
                {
                    row1,
                    row2
                }
            );

            var new_sheet = sheet.Sort(new CellPosition(1));

            Assert.That
            (
                new_sheet[new CellPosition(1)],
                Is.EqualTo(row2)
            );
            Assert.That
            (
                new_sheet[new CellPosition(2)],
                Is.EqualTo(row1)
            );
        }
    }
}
