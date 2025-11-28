using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_15;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2024/day/15
///
/// Input Format:
/// 	########
///		#..O.O.#
///		##@.O..#
///		#...O..#
///		#.#.O..#
///		#...O..#
///		#......#
///		########
///
///		<^^>>>vv<v>>v<<
/// </summary>
public abstract class Day_15 : BaseCsharpSolver
{
	public override string Title => "Warehouse Woes";
	public override int Day => 15;
	public override int Year => 2024;

	protected Dictionary<char, (int x, int y)> CardinalDirections = new() {
		{ '<', (x: -1, y:  0) }, // WEST
		{ '^', (x:  0, y: -1) }, // NORTH
		{ '>', (x:  1, y:  0) }, // EAST
		{ 'v', (x:  0, y:  1) }, // SOUTH
	};

	protected static List<char> ParseMovements(List<string> input)
	{
		var movements = new List<char>();

		var startIndex = 0;

		// Skip the map section of the input
		while(!string.IsNullOrWhiteSpace(input[startIndex]))
		{
			startIndex++;
		}

		for (var i = startIndex; i < input.Count; i++)
		{
			movements.AddRange(input[i].ToCharArray().ToList());
		}

		return movements;
	}
}