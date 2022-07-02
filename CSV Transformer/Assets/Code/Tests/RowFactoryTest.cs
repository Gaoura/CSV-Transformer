#nullable enable

using System;
using System.Collections.Generic;

using NUnit.Framework;

using CSVTransformer.Codebase;

namespace CSVTransformer.Tests.Unit
{
    public sealed class RowFactoryTest
    {
        [Test]
        public void Build_ShouldReturnTheRightCountOfCellsInRow_WhenCSVLineIsProvided()
        {
            var factory = new RowFactory();

            var row = factory.Build("2022-05-04T06:22:15+02:00,RE2UVzrFyxD,2d168cb9041660b8f59e,RVN,10.76624999,0.00000000");

            Assert.That
            (
                row.CellCount,
                Is.EqualTo(6)
            );
        }

        [Test]
        public void Build_ShouldReturnTheRightTypestOfCellsInRow_WhenCSVLineIsProvided()
        {
            var factory = new RowFactory();

            var row = factory.Build("2022-05-04T06:22:15+02:00,RE2UVzrFyxD,2d168cb9041660b8f59e,RVN,10.76624999,0.00000000");

            var expected_row_cell_types = new List<Type>()
            {
                typeof(DateCellData),
                typeof(StringCellData),
                typeof(StringCellData),
                typeof(StringCellData),
                typeof(NumberCellData),
                typeof(NumberCellData)
            };
            var actual_row_cell_types = new List<Type>();

            foreach (var cell in row)
            {
                actual_row_cell_types.Add(cell.GetType());
            }

            for (int i = 0; i < actual_row_cell_types.Count; ++i)
            {
                Assert.That
                (
                    actual_row_cell_types[i],
                    Is.EqualTo(expected_row_cell_types[i])
                );
            }
        }
    }
}