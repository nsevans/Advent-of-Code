using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Services.Solve.BaseSolvers;

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
public abstract class Day_12 : BaseCSharpSolver
{
	public override string Title => "Garden Groups";
	public override int Day => 12;
	public override int Year => 2024;

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