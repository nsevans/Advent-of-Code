using AdventOfCode.Puzzles.Solvers;

namespace AdventOfCode.Puzzles.Year_2024.Day_04;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2024/day/4
///
/// Example/Test Input:
/// 	MMMSXXMASM
/// 	MSAMXMSMSA
/// 	AMXSXMAAMM
/// 	MSAMASMSMX
/// 	XMASAMXAMM
/// 	XXAMMXXAMA
/// 	SMSMSASXSS
/// 	SAXAMASAAA
/// 	MAMMMXMMMM
/// 	MXMXAXMASX
/// </summary>
public abstract class Day_04 : BaseCSharpSolver
{
	public override string Title => "Ceres Search";
	public override int Day => 4;
	public override int Year => 2024;
}