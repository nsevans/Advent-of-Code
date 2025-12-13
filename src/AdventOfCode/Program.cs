using System;
using System.Linq;
using AdventOfCode.Common.Constants;
using AdventOfCode.Services.Generate;
using AdventOfCode.Services.Help;
using AdventOfCode.Services.Solve;

namespace AdventOfCode;

/// <summary>
/// This project is for all of my attempts at the Advent of Code puzzles found here: https://adventofcode.com.
/// Author: Nicholas Evans
/// </summary>
public class Program
{
	public static string AdventOfCodeUrl => "https://adventofcode.com";

	public static void Main(string[] args)
	{
		var firstArgument = args.FirstOrDefault();

        if (GenerateCommandConstants.Generate.Options.Contains(firstArgument))
        {
            var context = GenerationArgumentHandler.HandleInput(args[1..]);
            var generator = new GeneratingService(context);
            generator.Generate();
        }
        else if (SolveCommandConstants.Solve.Options.Contains(firstArgument))
        {
            var context = SolvingArgumentHandler.HandleInput(args[1..]);
            var orchestrator = new SolvingOrchestrator(context);
            orchestrator.Run();
        }
        else if (HelpCommandConstants.Help.Options.Contains(firstArgument))
        {
            var command = HelpArgumentHandler.HandleInput(args);
            HelpService.ShowHelp(command);
        }
        else
        {
            if (firstArgument == null)
                Console.WriteLine("No service specified");
            else
                Console.WriteLine($"Unknown option {firstArgument}");
            Console.WriteLine("Try `dotnet run -- --help` for more information.");
        }
	}
}