namespace Shared;

public static class Input
{
    public static string Get(string name)
    {
        return File.ReadAllText(GetPath(name));
    }

    public static StreamReader GetReader(string name)
    {
        return new StreamReader(GetPath(name));
    }

    private static string GetPath(string name)
    {
        return Path.Combine(GetSolutionDir(), "Inputs", $"{name}.txt");
    }

    private static string GetSolutionDir()
    {
        var dir = Environment.CurrentDirectory;
        while (true)
        {
            if (File.Exists(Path.Combine(dir, "AdventOfCode2025.sln")))
                return dir;

            var basePath = Path.GetDirectoryName(dir);
            dir = basePath ?? throw new InvalidCastException("Unable to find solution directory!");
        }
    }
}
