#nullable enable

using System;

using NUnit.Framework;

using CSVTransformer.Codebase;

namespace CSVTransformer.Tests.Unit
{
    public class CellDataTest
    {
        [Test]
        public void CompareTo_ShouldSuccessfullyCompareCells_WhenCellsAreStrings()
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

        [Test]
        public void CompareTo_ShouldSuccessfullyCompareCells_WhenCellsAreNumbers()
        {
            CellData cell1 = NumberCellData.Build("10.5") ?? throw new NullReferenceException();
            CellData cell2 = NumberCellData.Build("10") ?? throw new NullReferenceException();
            CellData cell3 = NumberCellData.Build("10") ?? throw new NullReferenceException();

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

        [Test]
        public void CompareTo_ShouldSuccessfullyCompareCells_WhenCellsAreDatesAndDataIsOnlyDate()
        {
            CellData cell1 = DateCellData.Build("2022-05-12") ?? throw new NullReferenceException();
            CellData cell2 = DateCellData.Build("2022-05-11") ?? throw new NullReferenceException();
            CellData cell3 = DateCellData.Build("2022-05-11") ?? throw new NullReferenceException();

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

        [Test]
        public void CompareTo_ShouldSuccessfullyCompareCells_WhenCellsAreDatesAndDataIsDateAndTime()
        {
            CellData cell1 = DateCellData.Build("2022-05-11T02:00:00+00:00") ?? throw new NullReferenceException();
            CellData cell2 = DateCellData.Build("2022-05-11T01:00:00+00:00") ?? throw new NullReferenceException();
            CellData cell3 = DateCellData.Build("2022-05-11T01:00:00+00:00") ?? throw new NullReferenceException();

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

        [Test]
        public void CompareTo_ShouldThrow_WhenCellsDoesNotShareSameType()
        {
            CellData cell1 = new StringCellData("2022-05-11");
            CellData cell2 = NumberCellData.Build("10.5") ?? throw new NullReferenceException();
            CellData cell3 = DateCellData.Build("2022-05-11") ?? throw new NullReferenceException();

            Assert.That
            (
                () => cell1 > cell2,
                Throws.InstanceOf<NotSupportedException>()
            );
            Assert.That
            (
                () => cell1 < cell2,
                Throws.InstanceOf<NotSupportedException>()
            );

            Assert.That
            (
                () => cell2 > cell1,
                Throws.InstanceOf<NotSupportedException>()
            );
            Assert.That
            (
                () => cell2 < cell1,
                Throws.InstanceOf<NotSupportedException>()
            );

            Assert.That
            (
                () => cell3 > cell1,
                Throws.InstanceOf<NotSupportedException>()
            );
            Assert.That
            (
                () => cell3 < cell1,
                Throws.InstanceOf<NotSupportedException>()
            );
        }

        [Test]
        public void DateCellDataToString_ShouldDisplayDateAndTime_WhenProvidedDateAndTime()
        {
            var cell = DateCellData.Build("2022-05-11T02:00:00+00:00") ?? throw new NullReferenceException();

            Assert.That
            (
                cell.ToString(),
                Is.EqualTo("2022-05-11T02:00:00+00:00")
            );
        }

        [Test]
        public void DateCellDataToString_ShouldDisplayDate_WhenProvidedDateOnly()
        {
            var cell = DateCellData.Build("2022-05-11") ?? throw new NullReferenceException();

            Assert.That
            (
                cell.ToString(),
                Is.EqualTo("2022-05-11")
            );
        }

        [Test]
        public void NumberCellDataToString_ShouldDisplayNumberAsItWasProvidedInCSV()
        {
            var cell = NumberCellData.Build("20.5") ?? throw new NullReferenceException();

            Assert.That
            (
                cell.ToString(),
                Is.EqualTo("20.5")
            );
        }

        [Test]
        public void Sum_ShouldConcatenateData_WhenCellsAreStrings()
        {
            CellData cell1 = new StringCellData("Part1");
            CellData cell2 = new StringCellData("Part2");

            CellData sum = cell1 + cell2;

            Assert.That
            (
                sum.ToString(),
                Is.EqualTo("Part1Part2")
            );
        }

        [Test]
        public void Sum_ShouldConcatenateData_WhenCellsAreNumbers()
        {
            CellData cell1 = DateCellData.Build("2022-05-11") ?? throw new NullReferenceException();
            CellData cell2 = DateCellData.Build("2022-05-12") ?? throw new NullReferenceException();

            CellData sum = cell1 + cell2;

            Assert.That
            (
                sum.ToString(),
                Is.EqualTo("2022-05-11")
            );
        }

        [Test]
        public void Sum_ShouldConcatenateData_WhenCellsAreDates()
        {
            CellData cell1 = NumberCellData.Build("10.5") ?? throw new NullReferenceException();
            CellData cell2 = NumberCellData.Build("12.5") ?? throw new NullReferenceException();

            CellData sum = cell1 + cell2;

            Assert.That
            (
                sum.ToString(),
                Is.EqualTo("23")
            );
        }

        [Test]
        public void Sum_ShouldThrow_WhenCellsDoesNotShareSameType()
        {
            CellData cell1 = new StringCellData("2022-05-11");
            CellData cell2 = NumberCellData.Build("10.5") ?? throw new NullReferenceException();
            CellData cell3 = DateCellData.Build("2022-05-11") ?? throw new NullReferenceException();

            Assert.That
            (
                () => cell1 + cell2,
                Throws.InstanceOf<NotSupportedException>()
            );

            Assert.That
            (
                () => cell2 + cell1,
                Throws.InstanceOf<NotSupportedException>()
            );

            Assert.That
            (
                () => cell3 + cell1,
                Throws.InstanceOf<NotSupportedException>()
            );
        }
    }
}