#nullable enable

using System;
using System.Collections.Generic;

using NUnit.Framework;

using CSVTransformer.Codebase;

namespace CSVTransformer.Tests.Unit
{
    public sealed class SheetFactoryTest
    {
        [Test]
        public void Build_ShouldReturnTheRightCountOfRowsInSheet_WhenCSVLinesAreProvided()
        {
            var factory = new SheetFactory();
            var csv_lines = new List<string>()
            {
                "field00,field01",
                "field10,field11",
                "field20,field21",
                "field30,field31"
            };

            var sheet = factory.Build(csv_lines);

            Assert.That
            (
                sheet.RowCount,
                Is.EqualTo(4)
            );
        }

        [Test]
        public void Build_ShouldThrow_WhenProvidedCSVLinesHaveVariableFieldCounts()
        {
            var factory = new SheetFactory();
            var csv_lines = new List<string>()
            {
                "field00,field01",
                "field10,field11,field12",
                "field20,field21",
                "field30,field31"
            };

            Assert.That
            (
                () => factory.Build(csv_lines),
                Throws.InstanceOf<ArgumentException>()
            );
        }

        [Test]
        public void Build_ShouldThrow_WhenProvidedCSVLinesHaveDifferentTypeCompositions()
        {
            var factory = new SheetFactory();
            var csv_lines = new List<string>()
            {
                "field00,field01",
                "20.5,field11",
                "field20,field21",
                "field30,field31"
            };

            Assert.That
            (
                () => factory.Build(csv_lines),
                Throws.InstanceOf<ArgumentException>()
            );
        }
    }
}