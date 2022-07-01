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
        public void Build_ShouldReturnNumberCellData_WhenDataIsNumber()
        {
            var factory = new CellDataFactory();
            var cell = factory.Build("20.5");

            Assert.That
            (
                cell,
                Is.InstanceOf<NumberCellData>()
            );
        }
    }
}