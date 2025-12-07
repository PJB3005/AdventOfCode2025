using System.Diagnostics;
using Shared;

var input = Input.GetReader("day06");

var rows = new List<int[]>();
List<char>? operations = null;

while (input.ReadLine() is { } line)
{
    if (line.Contains('+') || line.Contains('*'))
    {
        operations = line
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(x => x[0])
            .ToList();
        break;
    }
    else
    {
        rows.Add(line
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(int.Parse)
            .ToArray());
    }
}

Debug.Assert(operations != null);
Debug.Assert(rows.All(x => x.Length == operations.Count));

var total = 0L;
for (var c = 0; c < operations.Count; c++)
{
    var column = Column(c);

    long result;
    if (operations[c] == '+')
    {
        result = column.Sum();
    }
    else
    {
        Debug.Assert(operations[c] == '*');
        result = column.Aggregate(1L, (a, b) => a * b);
    }

    total += result;
}

Console.WriteLine(total);

return;

IEnumerable<int> Column(int c)
{
    foreach (var row in rows)
    {
        yield return row[c];
    }
}
