#nullable enable

using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

using CSVTransformer.Codebase;

public class RowTest
{
    [Test]
    public void ExtractColumns_OnlyKeepCellsThatAskedFor()
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
            new_row[0].ToString(),
            Is.EqualTo("Cell1")
        );
        Assert.That
        (
            new_row[1].ToString(),
            Is.EqualTo("Cell3")
        );
    }
}
