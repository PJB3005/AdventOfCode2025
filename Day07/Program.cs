using System.Diagnostics;
using Shared;

using var input = Input.GetReader("day07");

var firstLine = input.ReadLine() ?? throw new Exception();

var startPos = firstLine.IndexOf('S');
var width = firstLine.Length;

var rows = new List<Tile[]>();

while (input.ReadLine() is { } line)
{
    var row = line.Select(chr => chr == '^' ? Tile.Splitter : Tile.Empty).ToArray();
    rows.Add(row);
}

Debug.Assert(rows.All(row => row.Length == width));

// Make row below start a beam.

Debug.Assert(rows[0][startPos] == Tile.Empty);
rows[0][startPos] = Tile.Beam;

var totalSplittersHit = 0;

for (var i = 0; i < rows.Count - 1; i++)
{
    var curRow = rows[i];
    var nextRow = rows[i + 1];

    for (var x = 0; x < curRow.Length; x++)
    {
        var tile = curRow[x];
        if (tile != Tile.Beam)
            continue;

        var nextTile = nextRow[x];
        if (nextTile == Tile.Empty)
        {
            nextRow[x] = Tile.Beam;
        }
        else if (nextTile == Tile.Splitter)
        {
            totalSplittersHit += 1;
            nextRow[x - 1] = Tile.Beam;
            nextRow[x + 1] = Tile.Beam;
        }
    }
}

Console.WriteLine(totalSplittersHit);

internal enum Tile : byte
{
    Empty,
    Splitter,
    Beam
}
