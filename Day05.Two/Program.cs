using System.Diagnostics;
using Shared;

var input = Input.Get("day05");

// Parse input.

var ranges = new List<(long Start, long End)>();
var reader = new StringReader(input);

while (reader.ReadLine() is { } line)
{
    if (string.IsNullOrWhiteSpace(line))
        break;

    line = line.Trim();

    var split = line.Split('-');
    ranges.Add((long.Parse(split[0]), long.Parse(split[1])));
}

// Merge ranges.
// This is a loop. Fuck you. I used goto.
{
    retry:
    ranges.Sort((x, y) => x.Start.CompareTo(y.Start));

    for (var i = 0; i < ranges.Count; i++)
    {
        var (start, end) = ranges[i];

        for (var j = i + 1; j < ranges.Count; j++)
        {
            var (otherStart, otherEnd) = ranges[j];
            if (otherStart > end)
                break;

            Debug.Assert(otherStart >= start && otherStart <= end);

            Console.WriteLine($"Merging range {start}-{end} and {otherStart}-{otherEnd}");

            ranges[i] = (start, Math.Max(otherEnd, end));
            ranges.RemoveAt(j);
            goto retry;
        }
    }
}

foreach (var (start, end) in ranges)
{
    Console.WriteLine($"{start}-{end}");
}

var result = ranges.Sum(r => r.End - r.Start + 1);
Console.WriteLine(result);
