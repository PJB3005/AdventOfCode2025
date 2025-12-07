using Shared;

var input = Input.Get("day05");

var ranges = new List<(long Start, long End)>();
var validIds = new List<long>();

// Parse input.

var reader = new StringReader(input);
var readingRanges = true;
while (reader.ReadLine() is { } line)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        readingRanges = false;
        continue;
    }

    line = line.Trim();

    if (readingRanges)
    {
        var split = line.Split('-');
        ranges.Add((long.Parse(split[0]), long.Parse(split[1])));
    }
    else
    {
        validIds.Add(long.Parse(line));
    }
}

// Check

var result = validIds.Count(IsFreshIngredient);

Console.Write(result);

return;

bool IsFreshIngredient(long ingredientId)
{
    return ranges.Any(x => ingredientId >= x.Start && ingredientId <= x.End);
}
