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

        [Test]
        public void SumByDate_ShouldSumAllLinesWithSameDateIntoSameLine()
        {
            var sheet = CreateRows();
            var date_positions = sheet.GetAllDateCellColumnPositions();
            var summed_sheet = sheet.SumByDate(date_positions[0]);
            var expected_sheet = "2022-05-04,30.5\n" +
                                 "2022-05-07,9\n" +
                                 "2022-05-09,13";

            Assert.That
            (
                summed_sheet.ToString(),
                Is.EqualTo(expected_sheet)
            );

            static Sheet CreateRows()
            {
                var row1 = new Row
                (
                    new List<CellData>()
                    {
                        DateCellData.Build("2022-05-04T06:22:15+02:00"),
                        NumberCellData.Build("20.5")
                    }
                );
                var row2 = new Row
                (
                    new List<CellData>()
                    {
                        DateCellData.Build("2022-05-04T08:22:15+02:00"),
                        NumberCellData.Build("10")
                    }
                );
                var row3 = new Row
                (
                    new List<CellData>()
                    {
                        DateCellData.Build("2022-05-07T06:22:15+02:00"),
                        NumberCellData.Build("9")
                    }
                );
                var row4 = new Row
                (
                    new List<CellData>()
                    {
                        DateCellData.Build("2022-05-09T06:22:15+02:00"),
                        NumberCellData.Build("13")
                    }
                );


                var sheet = new Sheet
                (
                    new List<Row>()
                    {
                        row1,
                        row2,
                        row3,
                        row4
                    }
                );

                return sheet;
            }
        }

        [Test]
        public void SumByDate_ShouldReturnSheetWithNoModification_WhenItContainsOneLine()
        {
            var row1 = new Row
            (
                new List<CellData>()
                {
                    DateCellData.Build("2022-05-04T06:22:15+02:00"),
                    NumberCellData.Build("20.5")
                }
            );


            var sheet = new Sheet
            (
                new List<Row>()
                {
                        row1,
                }
            );

            var date_positions = sheet.GetAllDateCellColumnPositions();
            var summed_sheet = sheet.SumByDate(date_positions[0]);
            var expected_sheet = "2022-05-04T06:22:15+02:00,20.5";

            Assert.That
            (
                summed_sheet.ToString(),
                Is.EqualTo(expected_sheet)
            );
        }
    }
}
