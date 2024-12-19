using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2024.Day_06;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
/// 
/// Link to Day https://adventofcode.com/2024/day/6
/// 
/// Input Format: 
/// 	....#.....
///		.........#
///		..........
///		..#.......
///		.......#..
///		..........
///		.#..^.....
///		........#.
///		#.........
///		......#...
/// </summary>
public abstract class Day_06 : BaseSolver
{
	public override string Title => "Guard Gallivant";
	public override int Day => 6;
	public override int Year => 2024;

	protected static readonly List<(int x, int y)> Directions = [
		(0,  -1),	// NORTH
		( 1,  0),	// EAST
		( 0,  1),	// SOUTH
		(-1,  0)	// WEST
	];
}