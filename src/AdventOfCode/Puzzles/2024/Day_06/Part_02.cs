using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode.Puzzles.Year_2024.Day_06;

public class Part_02 : Day_06
{
	public override int Part => 2;

	private List<List<char>> _map;

	public override void PrepareData(List<string> input)
	{
		_map = input.To2DCharList();
	}

	public override string ResultMessage => "Number of positions causing guard infinite loop";

	public override string GetResult()
	{
		return CalcualtePositionsToCauseInfiniteLoops(_map).ToString();
	}

	private int CalcualtePositionsToCauseInfiniteLoops(List<List<char>> map)
	{
		var possibleGuardPositions = GetAllPossibleGuardPositions(map);
		var existingObstaclePositions = GetObstaclesPositions(map);

		var possibleObstacles = 0;

		// Iterate over each guard position, insert a new obstacle, then simulate the guard path to see if
		// it causes a loop
		foreach (var (guardPosition, index) in possibleGuardPositions.Select((p, i) => (p, i)))
		{
			var mapCopy = map.Select(x => x.ToList()).ToList();

			mapCopy[guardPosition.y][guardPosition.x] = '#';

			SimulateGuardPath(mapCopy, out var isLooping);

			if (isLooping)
				possibleObstacles++;
		}

		return possibleObstacles;
	}

	private List<(int x, int y)> GetAllPossibleGuardPositions(List<List<char>> map)
	{
		var guardPositions = new List<(int x, int y)>();

		var startingY = map.IndexOf(map.First(x => x.Contains('^')));
		var startingX = map[startingY].IndexOf('^');
		var startingDirection = Directions[0];

		var currentY = startingY;
		var currentX = startingX;
		var currentDirection = startingDirection;

		do
		{
			if (map[currentY][currentX] == '.')
			{
				guardPositions.Add((currentX, currentY));
			}

			var nextX = currentX + currentDirection.x;
			var nextY = currentY + currentDirection.y;

			if (nextY < 0 || nextX < 0 || nextY >= map.Count || nextX >= map[nextY].Count)
				break;

			if (map[nextY][nextX] == '#')
				// Update to try new direction (90 degree right turn)
				currentDirection = Directions[(Directions.IndexOf(currentDirection) + 1) % Directions.Count];
			else
			{
				currentX = nextX;
				currentY = nextY;
			}
		}
		// Account for if the current position returns to the starting position in the same direction to avoid an infinite loop
		while (!(currentX == startingX && currentY == startingY && currentDirection == startingDirection));

		return guardPositions.Distinct().ToList();
	}

	private static List<(int x, int y)> GetObstaclesPositions(List<List<char>> map)
	{
		var positions = new List<(int x, int y)>();

		foreach (var (row, rowIndex) in map.Select((r, i) => (r, i)))
		{
			foreach (var (position, positionIndex) in row.Select((p, i) => (p, i)))
			{
				if (position == '#')
					positions.Add((positionIndex, rowIndex));
			}
		}

		return positions;
	}

	private void SimulateGuardPath(List<List<char>> map, out bool isLooping)
	{
		isLooping = false;

		var obstacleCollisions = new Dictionary<(int x, int y), int>();

		var startingY = map.IndexOf(map.First(x => x.Contains('^')));
		var startingX = map[startingY].IndexOf('^');
		var startingDirection = Directions[0];

		var currentY = startingY;
		var currentX = startingX;
		var currentDirection = Directions[0];

		do
		{
			var nextX = currentX + currentDirection.x;
			var nextY = currentY + currentDirection.y;

			if (nextY < 0 || nextX < 0 || nextY >= map.Count || nextX >= map[nextY].Count)
				break;

			if (map[nextY][nextX] == '#')
			{
				// Update to try new direction (90 degree right turn)
				currentDirection = Directions[(Directions.IndexOf(currentDirection) + 1) % Directions.Count];

				if (obstacleCollisions.ContainsKey((nextX, nextY)))
					obstacleCollisions[(nextX, nextY)]++;
				else
					obstacleCollisions.Add((nextX, nextY), 1);

				// If the number of times the guard collides with a specific obstacle exceeds the thershold
				// then it is likely in a loop
				if (obstacleCollisions[(nextX, nextY)] < 4)
					continue;

				isLooping = true;
				break;
			}
			else
			{
				currentX = nextX;
				currentY = nextY;
			}
		}
		while (true);
	}
}