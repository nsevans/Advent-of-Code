using AdventOfCode.Extensions;

namespace AdventOfCode;

/// <summary>
/// Author: Nicholas Evans
/// 
/// This project is for all of my attempts at the Advent of Code puzzles found here: https://adventofcode.com.
/// 
/// Usage:
/// 	dotnet run -- --year <int> --puzzle <int> --part <int> --verbose
/// All parameters are optional. The ones used will filter down the number of puzzles performed.
/// Ex:
/// 	`dotnet run -- --year 2024` will execute all puzzles for the year 2024
/// 	`dotnet run -- --day 1` will execute puzzles for day #1 for all years
/// 	`dotnet run -- --part 1` will execute the first part of all days for all years
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

		var orchestrator = new PuzzleSolvingService(year, day, part, verbose);
		orchestrator.SolvePuzzles();
	}
}