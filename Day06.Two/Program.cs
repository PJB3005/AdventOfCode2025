using System.Buffers;
using System.Diagnostics;
using Shared;

var input = Input.GetReader("day06");

var unparsedRows = new List<string>();
var operations = new List<(char Operation, int StartPos, int EndPos)>();

var opValues = SearchValues.Create('+', '*');

while (input.ReadLine() is { } line)
{
    if (line.ContainsAny(opValues))
    {
        // The operations line.

        var curPos = 0;
        while (true)
        {
            var op = line[curPos];
            Debug.Assert(opValues.Contains(op));

            var searchStart = curPos + 1;
            var nextPos = line.AsSpan(searchStart).IndexOfAny(opValues);

            int endPos;
            if (nextPos < 0)
                endPos = line.Length;
            else
                endPos = searchStart + nextPos - 1;

            operations.Add((op, curPos, endPos));

            curPos = searchStart + nextPos;

            if (nextPos < 0)
                break;
        }

        break;
    }
    else
    {
        unparsedRows.Add(line);
    }
}

var total = 0L;
for (var c = 0; c < operations.Count; c++)
{
    var column = Column(c);
    var op = operations[c].Operation;

    long result;
    if (op == '+')
    {
        result = column.Sum();
    }
    else
    {
        Debug.Assert(op == '*');
        result = column.Aggregate(1L, (a, b) => a * b);
    }

    total += result;
}

Console.WriteLine(total);

return;

int[] Column(int column)
{
    var (_, start, end) = operations[column];

    var results = new int[end - start];
    var buf = new char[unparsedRows.Count];
    var insertIdx = 0;
    for (var i = end - 1; i >= start; i--)
    {
        for (var r = 0; r < unparsedRows.Count; r++)
        {
            buf[r] = unparsedRows[r][i];
        }

        var bufSpan = buf.AsSpan().Trim();
        results[insertIdx++] = int.Parse(bufSpan);
    }

    return results;
}
