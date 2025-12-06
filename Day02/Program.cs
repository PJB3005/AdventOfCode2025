using System.Globalization;
using Shared;

var input = Input.Get("day02").Trim();
var ranges = input.Split(',');

var sum = 0L;

foreach (var range in ranges)
{
    var split = range.Split('-');
    var start = long.Parse(split[0]);
    var end = long.Parse(split[1]);

    for (var i = start; i <= end; i++)
    {
        if (IsInvalidId(i))
        {
            Console.WriteLine($"Found ID: {i} in range {start}-{end}");
            sum += i;
        }
    }
}

Console.WriteLine(sum);

return;

static bool IsInvalidId(long value)
{
    Span<char> data = stackalloc char[64];
    value.TryFormat(data, out var written, provider: CultureInfo.InvariantCulture);
    var str = data[..written];

    if (str.Length % 2 == 1)
        return false;

    var half = str.Length / 2;

    return str[..half].SequenceEqual(str[half..]);
}
