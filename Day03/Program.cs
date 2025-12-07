using System.Diagnostics;
using Shared;

var result = Input.Get("day03")
    .Split("\n")
    .Select(x => x.Trim())
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .AsParallel()
    .Select(CalculateJoltage)
    .Sum();

Console.WriteLine(result);

static int CalculateJoltage(string value)
{
    value = value.Trim();

    var highestPos = -1;
    var highest = '0';

    for (var i = 0; i < value.Length - 1; i++)
    {
        var chr = value[i];
        Debug.Assert(chr is <= '9' and >= '1');
        if (chr > highest)
        {
            highest = chr;
            highestPos = i;
        }
    }

    var secondHighest = value[(highestPos + 1)..].Max();

    var max = int.Parse($"{highest}{secondHighest}");

    Console.WriteLine($"Max in {value}: {max}");

    return max;
}
