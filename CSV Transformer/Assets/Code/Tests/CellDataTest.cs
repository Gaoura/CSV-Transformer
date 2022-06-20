#nullable enable

using NUnit.Framework;

using CSVTransformer.Codebase;

namespace CSVTransformer.Tests.Unit
{
    public class CellDataTest
    {
        [Test]
        public void CompareTo_ShouldSuccessfullyCompareCells_WhenCellsShareSameType()
        {
            CellData cell1 = new StringCellData("2022-05-11");
            CellData cell2 = new StringCellData("2022-05-09");
            CellData cell3 = new StringCellData("2022-05-09");

            Assert.That
            (
                cell1 > cell2,
                Is.True
            );
            Assert.That
            (
                cell1 < cell2,
                Is.False
            );

            Assert.That
            (
                cell2 > cell1,
                Is.False
            );
            Assert.That
            (
                cell2 < cell1,
                Is.True
            );

            Assert.That
            (
                cell2 < cell3,
                Is.False
            );

            Assert.That
            (
                cell2 > cell3,
                Is.False
            );
        }
    }
}