#nullable enable

using System;

using NUnit.Framework;

using CSVTransformer.Codebase;

namespace CSVTransformer.Tests.Unit
{
    public class CellDataFactoryTest
    {
        [Test]
        public void Build_ShouldReturnStringCellData_ByDefault()
        {
            var factory = new CellDataFactory();
            var cell = factory.Build("aeaezz");

            Assert.That
            (
                cell,
                Is.InstanceOf<StringCellData>()
            );
        }

        [Test]
        public void Build_ShouldReturnNumberCellData_WhenFieldIsNumber()
        {
            var factory = new CellDataFactory();
            var cell = factory.Build("20.5");

            Assert.That
            (
                cell,
                Is.InstanceOf<NumberCellData>()
            );
        }

        [Test]
        public void Build_ShouldReturnDateCellData_WhenFieldIsDate()
        {
            var factory = new CellDataFactory();
            var cell1 = factory.Build("2022-05-11T01:00:00+00:00");
            var cell2 = factory.Build("2022-05-11");

            Assert.That
            (
                cell1,
                Is.InstanceOf<DateCellData>()
            );

            Assert.That
            (
                cell2,
                Is.InstanceOf<DateCellData>()
            );
        }
    }
}