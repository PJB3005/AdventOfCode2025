// 5137133207829 -> too low

using System.Diagnostics;
using Shared;

using var input = Input.GetReader("day07");

// Parse file.
var firstLine = input.ReadLine() ?? throw new Exception();

var startPos = firstLine.IndexOf('S');
var width = firstLine.Length;

var rows = new List<Tile[]>();

while (input.ReadLine() is { } line)
{
    var row = line.Select(chr => new Tile(chr == '^')).ToArray();
    rows.Add(row);
}

Debug.Assert(rows.All(row => row.Length == width));

// Propagate bottom to top.

for (var r = rows.Count - 1; r >= 0; r--)
{
    var row = rows[r];
    for (var c = 0; c < row.Length; c++)
    {
        var tile = row[c];
        if (tile.IsSplitter)
        {
            PropagateSplitterUp(tile, r, c);
        }
    }
}

var topSplitter = rows[1].Single(x => x.IsSplitter);

var timelineCount = topSplitter.TimelineCount + 1;

Console.WriteLine(timelineCount);

void PropagateSplitterUp(Tile splitter, int startRow, int column)
{
    for (var r = startRow - 1; r >= 0; r--)
    {
        var tile = rows[r][column];
        if (tile.IsSplitter)
            return;

        PropagateIfSplitter(splitter, r, column - 1);
        PropagateIfSplitter(splitter, r, column + 1);
    }
}

void PropagateIfSplitter(Tile splitter, int row, int column)
{
    ref var tile = ref rows[row][column];
    if (!tile.IsSplitter)
        return;

    tile.TimelineCount += splitter.TimelineCount;
}

[DebuggerDisplay("Splitter: {IsSplitter} w/ {TimelineCount}")]
internal struct Tile(bool isSplitter)
{
    public readonly bool IsSplitter = isSplitter;
    public long TimelineCount = isSplitter ? 1 : 0;
}
