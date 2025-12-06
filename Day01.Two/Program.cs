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

    for (var i = 0; i < count; i++)
    {
        if (type == 'L')
        {
            pos -= 1;
            if (pos < 0)
                pos = 99;
        }
        else
        {
            pos += 1;
            if (pos > 99)
                pos = 0;
        }

        if (pos == 0)
            countZero += 1;
    }
}

Console.WriteLine(countZero);