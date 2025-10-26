using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Puzzles.Year_2024.Day_08;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2024/day/8
///
/// Example/Test Input:
/// 	............
///		........0...
///		.....0......
///		.......0....
///		....0.......
///		......A.....
///		............
///		............
///		........A...
///		.........A..
///		............
///		............
/// </summary>
public abstract class Day_08 : BaseDotnetSolver
{
	public override string Title => "Resonant Collinearity";
	public override int Day => 8;
	public override int Year => 2024;

	protected static Dictionary<char, List<Vector2>> ParseFrequenceyDictionary(IEnumerable<string> input)
	{
		// Convert input to dictionary of Lists of Vectors to more easily identify matching antenna frequencies
		var dictionary = new Dictionary<char, List<Vector2>>();

		foreach (var (line, yCoord) in input.Select((l, y) => (l, y)))
		{
			foreach (var (point, xCoord) in line.Select((p, x) => (p, x)))
			{
				if (point == '.')
					continue;

				if (dictionary.TryGetValue(point, out var coords))
					coords.Add(new Vector2(xCoord, yCoord));
				else
					dictionary.Add(point, [ new Vector2(xCoord, yCoord) ]);
			}
		}

		return dictionary;
	}
}