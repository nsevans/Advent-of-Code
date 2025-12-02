namespace AdventOfCode.Puzzles.Year_2024.Day_25;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2024/day/25
///
/// Example/Test Input:
///		#####
///		.####
///		.####
///		.####
///		.#.#.
///		.#...
///		.....
///
///		#####
///		##.##
///		.#.##
///		...##
///		...#.
///		...#.
///		.....
///
///		.....
///		#....
///		#....
///		#...#
///		#.#.#
///		#.###
///		#####
///
///		.....
///		.....
///		#.#..
///		###..
///		###.#
///		###.#
///		#####
///
///		.....
///		.....
///		.....
///		#....
///		#.#..
///		#.#.#
///		#####
/// </summary>
public abstract class Day_25 : BaseCSharpSolver
{
	public override string Title => "Code Chronicle";
	public override int Day => 25;
	public override int Year => 2024;
}