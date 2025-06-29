using System.Collections.Generic;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode.Puzzles.Year_2024.Day_20;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
/// 
/// Link to Day https://adventofcode.com/2024/day/20
/// 
/// Example/Test Input: 
///		###############
///		#...#...#.....#
///		#.#.#.#.#.###.#
///		#S#...#.#.#...#
///		#######.#.#.###
///		#######.#.#...#
///		#######.#.###.#
///		###..E#...#...#
///		###.#######.###
///		#...###...#...#
///		#.#####.#.###.#
///		#.#...#.#.#...#
///		#.#.#.#.#.#.###
///		#...#...#...###
///		###############
/// </summary>
public abstract class Day_20 : BaseSolver
{
	public override string Title => "Race Condition";
	public override int Day => 20;
	public override int Year => 2024;

	protected readonly List<(int x, int y)> Directions = [
		(0,  -1),	// NORTH
		( 1,  0),	// EAST
		( 0,  1),	// SOUTH
		(-1,  0)	// WEST
	];

	protected static List<List<bool>> InitializeVisitedMap(List<List<char>> map)
	{
		var visited = new List<List<bool>>();
		for (var i = 0; i < map.Count; i++)
		{
			var visitedRow = new List<bool>();
			for (var j = 0; j < map[i].Count; j++)
			{
				visitedRow.Add(false);
			}
			visited.Add(visitedRow);
		}

		return visited;
	}

	protected static void TraverseRaceMap(List<List<char>> raceMap, List<List<bool>> visited, (int x, int y) startPosition, (int x, int y) endPosition, Dictionary<(int x, int y), int> positionTimeDictionary)
	{
		var currentTime = 0;
		var currentPosition = startPosition;
		
		while(currentPosition != endPosition)
		{
			visited.SetValueAtIndex(currentPosition, true);

			var leftPosition = (x: currentPosition.x - 1, y: currentPosition.y);
			if (raceMap.IsInBounds(leftPosition) && !visited.GetValueAtIndex(leftPosition) && raceMap.GetValueAtIndex(leftPosition) != '#')
			{
				positionTimeDictionary.Add(leftPosition, ++currentTime);
				currentPosition = leftPosition;
				continue;
			}

			var rightPosition = (x: currentPosition.x + 1, y: currentPosition.y);
			if (raceMap.IsInBounds(rightPosition) && !visited.GetValueAtIndex(rightPosition) && raceMap.GetValueAtIndex(rightPosition) != '#')
			{
				positionTimeDictionary.Add(rightPosition, ++currentTime);
				currentPosition = rightPosition;
				continue;
			}

			var topPosition = (x: currentPosition.x, y: currentPosition.y - 1);
			if (raceMap.IsInBounds(topPosition) && !visited.GetValueAtIndex(topPosition) && raceMap.GetValueAtIndex(topPosition) != '#')
			{
				positionTimeDictionary.Add(topPosition, ++currentTime);
				currentPosition = topPosition;
				continue;
			}

			var bottomPosition = (x: currentPosition.x, y: currentPosition.y + 1);
			if (raceMap.IsInBounds(bottomPosition) && !visited.GetValueAtIndex(bottomPosition) && raceMap.GetValueAtIndex(bottomPosition) != '#')
			{
				positionTimeDictionary.Add(bottomPosition, ++currentTime);
				currentPosition = bottomPosition;
				continue;
			}
		}
	}
}