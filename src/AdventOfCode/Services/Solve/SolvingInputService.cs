using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Puzzles.Solvers;

namespace AdventOfCode.Services.Solve;

public static class SolvingInputService
{
    public static bool TryGetInput(BaseSolver solver, out List<string> fileContents)
    {
        fileContents = [];

        if (!File.Exists(solver.InputFilePath))
        {
            // Create file for easier setup if one hasn't already been created
            File.Create(solver.InputFilePath);
            return false;
        }

        fileContents = File.ReadLines(solver.InputFilePath).ToList();

        if (fileContents.Count == 0 || string.IsNullOrWhiteSpace(string.Join("", fileContents)))
            return false;

        return true;
    }
}