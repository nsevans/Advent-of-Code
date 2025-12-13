using System.Collections.Generic;
using AdventOfCode.Services.Solve.BaseSolvers;

namespace AdventOfCode.Puzzles.Year_2023.Day_01;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2023/day/1
///
/// Example/Test Input:
///		mrjstg5onetwoeightgcczx8vgrgl
/// </summary>
public abstract class Day_01 : BaseCSharpSolver
{
	public override string Title => "Trebuchet?!";
    public override int Day => 1;
    public override int Year => 2023;

	protected static List<string> NumberStrings => ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
}