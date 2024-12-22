using AdventOfCode.Common.Extensions;
using AdventOfCode.Common.Models;
using AdventOfCode.Services;

namespace AdventOfCode;

/// <summary>
/// This project is for all of my attempts at the Advent of Code puzzles found here: https://adventofcode.com.
/// 
/// Author: Nicholas Evans
/// </summary>
public class Program
{
	public static string BaseUrl => "https://adventofcode.com";

	public static void Main(string[] args)
	{
		var yearValue = args.GetValueForArgument("--year");
		var year = string.IsNullOrEmpty(yearValue) ? (int?) null : int.Parse(yearValue);

		var dayValue = args.GetValueForArgument("--day");
		var day = string.IsNullOrEmpty(dayValue) ? (int?) null : int.Parse(dayValue);

		var partValue = args.GetValueForArgument("--part");
		var part = string.IsNullOrEmpty(partValue) ? (int?)null : int.Parse(partValue);

		var verbose = args.HasArgument("--verbose");
		var useTestInput = args.HasArgument("--test");

		var context = new SolverContext(year, day, part, verbose, useTestInput);
		var orchestrator = new SolvingOrchestrator(context);
		orchestrator.Run();
	}
}