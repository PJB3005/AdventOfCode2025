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

static long CalculateJoltage(string value)
{
    const int digits = 12;

    var span = value.AsSpan().Trim();

    var accum = 0L;

    for (var i = 0; i < digits; i++)
    {
        var keepChars = digits - i - 1;
        FindMaxInString(span[..^keepChars], out var digit, out var pos);
        accum *= 10;
        accum += digit;

        span = span[(pos+1)..];
    }

    Console.WriteLine($"Max in {value}: {accum}");
    return accum;
}

static void FindMaxInString(ReadOnlySpan<char> span, out int digit, out int pos)
{
    Debug.Assert(span.Length > 0);

    var highestPos = -1;
    var highest = '0';

    for (var i = 0; i < span.Length; i++)
    {
        var chr = span[i];
        Debug.Assert(chr is <= '9' and >= '1');
        if (chr > highest)
        {
            highest = chr;
            highestPos = i;
        }
    }

    Debug.Assert(highestPos >= 0);

    digit = highest - '0';
    pos = highestPos;
}
