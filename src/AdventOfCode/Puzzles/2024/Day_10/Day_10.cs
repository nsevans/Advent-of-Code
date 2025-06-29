using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_10;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
/// 
/// Link to Day https://adventofcode.com/2024/day/10
/// 
/// Example/Test Input: 
/// 	0123
///		1234
///		8765
///		9876
/// </summary>
public abstract class Day_10 : BaseSolver
{
	public override string Title => "Hoof It";
	public override int Day => 10;
	public override int Year => 2024;

	protected static List<List<int>> ParseTrailMap(IEnumerable<string> input)
	{
		return input.Select(x => x.Select(x => int.Parse(x.ToString())).ToList()).ToList();
	}

	protected static List<(int x, int y)> ParseLocations(List<List<int>> input, int locationId)
	{
		var locations = new List<(int x, int y)>();

		foreach (var (line, y) in input.Select((l, i) => (l, i)))
		{
			foreach (var (value, x) in line.Select((v, i) => (v, i)))
			{
				if (value == locationId)
					locations.Add((x, y));
			}
		}

		return locations;
	}

	protected static List<(int x, int y)> TryFindEndOfTrailRecursiveDFS(List<List<int>> trailMap, (int x, int y) position)
    {
		var topsFound = new List<(int x, int y)>();

		if (trailMap[position.y][position.x] == 9)
			return [ (position.x, position.y) ];

		var currentPositionValue = trailMap[position.y][position.x];

		// Ensure left position is valid and check it if it is
		if (position.x - 1 >= 0 && trailMap[position.y][position.x - 1] == currentPositionValue + 1)
			topsFound.AddRange(TryFindEndOfTrailRecursiveDFS(trailMap, (position.x - 1, position.y)));

		// Ensure right position is valid and check it if it is 
		if (position.x + 1 < trailMap[position.y].Count && trailMap[position.y][position.x + 1] == currentPositionValue + 1)
			topsFound.AddRange(TryFindEndOfTrailRecursiveDFS(trailMap, (position.x + 1, position.y)));

		// Ensure top position is valid and check it if it is
		if (position.y - 1 >= 0 && trailMap[position.y - 1][position.x] == currentPositionValue + 1)
			topsFound.AddRange(TryFindEndOfTrailRecursiveDFS(trailMap, (position.x, position.y - 1)));

		// Ensure bottom position is valid and check it if it is
		if (position.y + 1 < trailMap.Count && trailMap[position.y + 1][position.x] == currentPositionValue + 1)
			topsFound.AddRange(TryFindEndOfTrailRecursiveDFS(trailMap, (position.x, position.y + 1)));

		return topsFound;
    }
}