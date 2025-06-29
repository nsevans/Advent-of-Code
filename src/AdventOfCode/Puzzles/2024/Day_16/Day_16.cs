using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2024.Day_16;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2024/day/16
///
/// Example/Test Input:
///		###############
///		#.......#....E#
///		#.#.###.#.###.#
///		#.....#.#...#.#
///		#.###.#####.#.#
///		#.#.#.......#.#
///		#.#.#####.###.#
///		#...........#.#
///		###.#.#####.#.#
///		#...#.....#.#.#
///		#.#.#.###.#.#.#
///		#.....#...#.#.#
///		#.###.#.#.#.#.#
///		#S..#.....#...#
///		###############
/// </summary>
public abstract class Day_16 : BaseSolver
{
	public override string Title => "Reindeer Maze";
	public override int Day => 16;
	public override int Year => 2024;

	protected List<(int dx, int dy)> CardinalDirections = [
		( 1,  0),	// EAST
		( 0,  1),	// SOUTH
		(-1,  0),	// WEST
		( 0, -1),	// NORTH
	];

	protected record MazeState(int X, int Y, int Dx, int Dy);
}