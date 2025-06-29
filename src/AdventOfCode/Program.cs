using System;
using System.Linq;
using AdventOfCode.Services.Generating;
using AdventOfCode.Services.Solving;

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

		if (firstArgument == "--generate" || firstArgument == "-g")
		{
			var context = GenerationArgumentHandler.HandleInput(args[1..]);
			var generator = new GeneratingService(context);
			generator.Generate();
		}
		else if (firstArgument == "--solve" || firstArgument == "-s")
		{
			var context = SolvingArgumentHandler.HandleInput(args[1..]);
			var orchestrator = new SolvingOrchestrator(context);
			orchestrator.Run();
		}
		else
		{
			if (firstArgument == null)
				Console.Write("No service specified.");
			else
				Console.Write($"Invalid service '{firstArgument}'.");
			Console.WriteLine(" Either use '--solve' to run the solving service or '--generate' to run the generation service.");
		}
	}
}