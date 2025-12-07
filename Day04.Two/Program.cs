using System.Diagnostics;
using Shared;

var input = ParseInput(Input.Get("day04"));

var xLength = input[0].Length;

var totalCountRemoved = 0;
while (true)
{
    var countRemoved = 0;

    for (var x = 0; x < xLength; x++)
    {
        for (var y = 0; y < input.Length; y++)
        {
            var c = input[y][x];
            if (!c)
                continue;

            if (IsRemoveable(y, x))
            {
                input[y][x] = false;
                countRemoved += 1;
            }
        }
    }

    if (countRemoved == 0)
        break;

    totalCountRemoved += countRemoved;
}

Console.Write(totalCountRemoved);

return;

bool IsRemoveable(int y, int x)
{
    var localCount = 0;
    if (Test(y - 1, x)) // N
        localCount += 1;
    if (Test(y - 1, x - 1)) // NW
        localCount += 1;
    if (Test(y, x - 1)) // W
        localCount += 1;
    if (Test(y + 1, x - 1)) // SW
        localCount += 1;
    if (Test(y + 1, x)) // S
        localCount += 1;
    if (Test(y + 1, x + 1)) // SE
        localCount += 1;
    if (Test(y, x + 1)) // E
        localCount += 1;
    if (Test(y - 1, x + 1)) // NE
        localCount += 1;

    return localCount < 4;
}

bool Test(int y, int x)
{
    if (y < 0 || y >= input.Length)
        return false;

    if (x < 0 || x >= xLength)
        return false;

    return input[y][x];
}

static bool[][] ParseInput(string input)
{
    var reader = new StringReader(input);

    var lines = new List<bool[]>();
    while (reader.ReadLine() is { } line)
    {
        if (string.IsNullOrWhiteSpace(line))
            continue;

        line = line.Trim();

        lines.Add(line.Select(c => c == '@').ToArray());
    }

    Debug.Assert(lines.All(l => l.Length == lines[0].Length));

    return lines.ToArray();
}
