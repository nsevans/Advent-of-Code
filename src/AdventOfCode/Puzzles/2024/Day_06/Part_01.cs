using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common.Constants;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode.Puzzles.Year_2024.Day_06;

public class Part_01 : Day_06
{
	public override int Part => 1;

	private List<List<char>> _map;

	public override void PrepareData(List<string> input)
	{
		_map = input.To2DCharList();
	}

	public override string ResultMessage => "Number of distinct guard positions";

	public override string GetResult()
	{
		return CalculateDistinctPositionsInPath(_map).ToString();
	}

	private static int CalculateDistinctPositionsInPath(List<List<char>> map)
	{
		var startingY = map.IndexOf(map.First(x => x.Contains('^')));
		var startingX = map[startingY].IndexOf('^');
		var startingDirection = Directions.Cardinal[0];

		var currentY = startingY;
		var currentX = startingX;
		var currentDirection = startingDirection;

		var distinctPositions = 0;
		do
		{
			// If on a valid position mark it and update count
			if (map[currentY][currentX] == '.' || map[currentY][currentX] == '^')
			{
				distinctPositions++;
				map[currentY][currentX] = 'X';
			}

			var nextX = currentX + currentDirection.x;
			var nextY = currentY + currentDirection.y;

			if (nextY < 0 || nextX < 0 || nextY >= map.Count || nextX >= map[nextY].Count)
				break;

			if (map[nextY][nextX] == '#')
				// Update to try new direction (90 degree right turn)
				currentDirection = Directions.Cardinal[(Directions.Cardinal.IndexOf(currentDirection) + 1) % Directions.Cardinal.Count];
			else
			{
				currentX = nextX;
				currentY = nextY;
			}
		}
		// Account for if the current position returns to the starting position in the same direction to avoid an infinite loop
		while (!(currentX == startingX && currentY == startingY && currentDirection == startingDirection));

		return distinctPositions;
	}
}