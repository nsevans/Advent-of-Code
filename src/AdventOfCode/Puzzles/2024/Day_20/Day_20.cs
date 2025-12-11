using System.Collections.Generic;
using AdventOfCode.Common.Extensions;
using AdventOfCode.Common.Models;
using AdventOfCode.Puzzles.Solvers;

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
public abstract class Day_20 : BaseCSharpSolver
{
	public override string Title => "Race Condition";
	public override int Day => 20;
	public override int Year => 2024;

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

	protected static void CalculateRaceMapTraversalTimes(List<List<char>> raceMap, Dictionary<Point2D, int> positionTimeDictionary)
    {
        var startPosition = new Point2D(raceMap.IndexOf('S'));
        var endPosition = new Point2D(raceMap.IndexOf('E'));
        var visited = InitializeVisitedMap(raceMap);

		var currentTime = 0;
        var currentPosition = startPosition;

		while(currentPosition != endPosition)
		{
			visited.SetValueAtIndex(currentPosition.X, currentPosition.Y, true);

			var leftPosition = new Point2D(currentPosition.X - 1, currentPosition.Y);
			if (raceMap.IsSafe(leftPosition, ['#']) && !visited.GetValueAtIndex(leftPosition))
			{
				positionTimeDictionary.Add(leftPosition, ++currentTime);
				currentPosition = leftPosition;
				continue;
			}

			var rightPosition = new Point2D(currentPosition.X + 1, currentPosition.Y);
			if (raceMap.IsSafe(rightPosition, ['#']) && !visited.GetValueAtIndex(rightPosition))
			{
				positionTimeDictionary.Add(rightPosition, ++currentTime);
				currentPosition = rightPosition;
				continue;
			}

			var topPosition = new Point2D(currentPosition.X, currentPosition.Y - 1);
			if (raceMap.IsSafe(topPosition, ['#']) && !visited.GetValueAtIndex(topPosition))
			{
				positionTimeDictionary.Add(topPosition, ++currentTime);
				currentPosition = topPosition;
				continue;
			}

			var bottomPosition = new Point2D(currentPosition.X, currentPosition.Y + 1);
			if (raceMap.IsSafe(bottomPosition, ['#']) && !visited.GetValueAtIndex(bottomPosition))
			{
				positionTimeDictionary.Add(bottomPosition, ++currentTime);
				currentPosition = bottomPosition;
				continue;
			}
		}
	}
}