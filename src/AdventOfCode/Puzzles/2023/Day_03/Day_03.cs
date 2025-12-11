using System.Collections.Generic;
using AdventOfCode.Puzzles.Solvers;

namespace AdventOfCode.Puzzles.Year_2023.Day_03;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2023/day/3
///
/// Example/Test Input:
///		..172..............................454..46.......507..........809
///		............*.........712........=.......*................515.*..
///		.........823.835........%.........710.....749........134..%......
/// </summary>
public abstract class Day_03 : BaseCSharpSolver
{
	public override string Title => "Gear Ratios";
    public override int Day => 3;
    public override int Year => 2023;

	protected static string ValidSymbols => "*=/%#&$-@+";

	protected static int ParseNumberFromString(List<char> line, int index)
	{
		var startingIndex = index;
		// Find starting index of number (loop backwards until first digit of group is found)
		for (int i = index; i > 0; i--)
		{
			if (line[i - 1] == '.' || ValidSymbols.Contains(line[i]))
				break;

			if (char.IsDigit(line[i - 1]))
				startingIndex = i - 1;
		}

		var numberAsString = "";
		// Compile number until end of list or first symbol is hit and  replace all found
		// digits with '.' so they aren't counted twice
		for (int i = startingIndex; i < line.Count; i++)
		{
			if (line[i] == '.' || ValidSymbols.Contains(line[i]))
				break;

			if (char.IsDigit(line[i]))
			{
				numberAsString += line[i];
				line[i] = '.';
			}
		}

		return int.Parse(numberAsString);
	}
}