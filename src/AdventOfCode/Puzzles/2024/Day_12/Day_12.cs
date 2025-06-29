using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_12;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2024/day/12
///
/// Example/Test Input:
/// 	AAAA
///		BBCD
///		BBCC
///		EEEC
/// </summary>
public abstract class Day_12 : BaseSolver
{
	public override string Title => "Garden Groups";
	public override int Day => 12;
	public override int Year => 2024;

	protected readonly List<(int dx, int dy)> CardinalDirections = [
		(-1,  0),	// WEST
		( 0, -1),	// NORTH
		( 1,  0),	// EAST
		( 0,  1)	// SOUTH
	];

	protected readonly List<(int dx, int dy)> OrdinalDirections = [
		(-1, -1),	// NORTH WEST
		( 1, -1),	// NORTH EAST
		( 1,  1),	// SOUTH EAST
		(-1,  1)	// SOUTH WEST
	];

	protected readonly List<(int dx, int dy)> AllDirections = [
		(-1,  0),	// WEST
		(-1, -1),	// NORTH WEST
		( 0, -1),	// NORTH
		( 1, -1),	// NORTH EAST
		( 1,  0),	// EAST
		( 1,  1),	// SOUTH EAST
		( 0,  1),	// SOUTH
		(-1,  1)	// SOUTH WEST
	];

	protected static List<List<char>> ParsePaddedGardenMap(IEnumerable<string> input)
	{
		// Pad top and bottom with padding character
		var horizontalPad = Enumerable.Repeat('_', input.Count() + 2).ToList();

		var newInput = new List<List<char>> { horizontalPad };

		newInput.AddRange(input
			.Select(x =>
			{
				// Pad start of line
				var line = new List<char>
                {
                    '_'
                };
				line.AddRange(x.ToCharArray());
				// Pad end of line
				line.Add('_');
				return line;
			}));

		newInput.Add(horizontalPad);
		return newInput;
	}
}