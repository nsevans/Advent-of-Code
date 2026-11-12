namespace AdventOfCode.Services.Solve;

public static class SolvingInputService
{
    public static bool TryGetInput(string filePath, out List<string> fileContents)
    {
        fileContents = [];
        if (!File.Exists(filePath))
        {
            // Create file for easier setup if one hasn't already been created
            File.Create(filePath);
            return false;
        }

        fileContents = File.ReadLines(filePath).ToList();

        if (fileContents.Count == 0 || string.IsNullOrWhiteSpace(string.Join("", fileContents)))
            return false;

        return true;
    }
}