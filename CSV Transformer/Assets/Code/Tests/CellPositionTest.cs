#nullable enable

using System;

using NUnit.Framework;

using CSVTransformer.Codebase;

namespace CSVTransformer.Tests.Unit
{
    public class CellPositionTest
    {
        [Test]
        public void Constructor_ShouldThrow_WhenParameterValueIsLessThanOne()
        {
            Assert.That
            (
                () => new CellPosition(0),
                Throws.TypeOf<ArgumentOutOfRangeException>()
            );
        }
    }
}