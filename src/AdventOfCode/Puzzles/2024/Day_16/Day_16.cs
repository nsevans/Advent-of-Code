using System.Collections.Generic;
using System.Numerics;

namespace AdventOfCode.Puzzles.Year_2024.Day_16;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
/// 
/// Link to Day https://adventofcode.com/2024/day/16
/// 
/// Input Format: 
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

	protected static List<Complex> CardinalDirections = [
		new( 1,  0),	// EAST
		new( 0,  1),	// SOUTH
		new(-1,  0),	// WEST
		new( 0, -1),	// NORTH
	];
}