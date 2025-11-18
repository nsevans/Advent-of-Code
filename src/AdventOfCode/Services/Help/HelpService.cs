using System;
using System.Linq;
using AdventOfCode.Common.Constants;

namespace AdventOfCode.Services.Help;

public static class HelpService
{
    public static void ShowHelp(string command = null)
    {
        if (HelpCommandConstants.Help.Options.Contains(command))
        {
            Console.WriteLine($"Usage: dotnet run -- {string.Join(" | ", HelpCommandConstants.Help.Options)}");
        }

        if (command == null || SolveCommandConstants.Solve.Options.Contains(command))
        {
            Console.WriteLine("=== Solve Command ===");
            Console.WriteLine($"Usage: dotnet run -- {string.Join(" | ", SolveCommandConstants.Solve.Options)}");
            Console.WriteLine(SolveCommandConstants.MainCommandHelp);
            Console.WriteLine(SolveCommandConstants.SubCommandHelp);
            Console.WriteLine();
        }

        if (command == null || GenerateCommandConstants.Generate.Options.Contains(command))
        {
            Console.WriteLine("=== Generate Command ===");
            Console.WriteLine($"Usage: dotnet run -- {string.Join(" | ", GenerateCommandConstants.Generate.Options)}");
            Console.WriteLine(GenerateCommandConstants.MainCommandHelp);
            Console.WriteLine(GenerateCommandConstants.SubCommandHelp);
            Console.WriteLine();
        }
    }
}