using Shared;

var data = Input.Get("day01");

var pos = 50;
var countZero = 0;

var reader = new StringReader(data);
while (reader.ReadLine() is { } line)
{
    var trimmed = line.Trim();
    var type = trimmed[0];
    var count = int.Parse(trimmed[1..]);
    if (type == 'L')
        count = -count;

    pos += count;

    while (pos < 0)
    {
        pos += 100;
    }

    while (pos > 99)
    {
        pos -= 100;
    }

    if (pos == 0)
        countZero += 1;
}

Console.WriteLine(countZero);
