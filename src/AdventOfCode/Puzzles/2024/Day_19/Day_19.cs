using AdventOfCode.Services.Solve.BaseSolvers;

namespace AdventOfCode.Puzzles.Year_2024.Day_19;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2024/day/19
///
/// Example/Test Input:
///		r, wr, b, g, bwu, rb, gb, br
///
///		brwrr
///		bggr
///		gbbr
///		rrbgbr
///		ubwu
///		bwurrg
///		brgr
///		bbrgwb
/// </summary>
public abstract class Day_19 : BaseCSharpSolver
{
	public override string Title => "Linen Layout";
	public override int Day => 19;
	public override int Year => 2024;
}