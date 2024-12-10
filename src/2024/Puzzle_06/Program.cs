using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year_2024.Puzzle_06;

/// <summary>
/// Link: https://adventofcode.com/2024/day/6
/// Input Format:
/// 	....#.....
///		.........#
///		..........
///		..#.......
///		.......#..
///		..........
///		.#..^.....
///		........#.
///		#.........
///		......#...
/// </summary>
public class Program
{
	private static readonly List<(int x, int y)> _directions = [
		(0,  -1),	// NORTH
		( 1,  0),	// EAST
		( 0,  1),	// SOUTH
		(-1,  0)	// WEST
	];

	public static void Main(string[] args)
	{
		Console.WriteLine("### 2024 - Puzzle 06 ###\n");
		var input = GetInput("./input.txt");

		var resultPart1 = CalculateDistinctPositionsInPath(input);
		Console.WriteLine($"[PART 1] Number of distinct guard positions: {resultPart1}");

		var resultPart2 = CalcualtePositionsToCauseInfiniteLoops(input);
		Console.WriteLine($"[PART 2] Number of positions causing guard loop: {resultPart2}");
	}

	public static List<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName).ToList();
	}

	#region PART 1
	private static int CalculateDistinctPositionsInPath(IEnumerable<string> input)
	{
		var map = input.Select(x => x.ToCharArray().ToList()).ToList();

		var startingY = map.IndexOf(map.First(x => x.Contains('^')));
		var startingX = map[startingY].IndexOf('^');
		var startingDirection = _directions[0];
		
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
				currentDirection = _directions[(_directions.IndexOf(currentDirection) + 1) % _directions.Count];
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
	#endregion

	#region PART 2
	private static int CalcualtePositionsToCauseInfiniteLoops(IEnumerable<string> input)
	{
		var map = input.Select(x => x.ToCharArray().ToList()).ToList();

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

	private static List<(int x, int y)> GetAllPossibleGuardPositions(List<List<char>> map)
	{
		var guardPositions = new List<(int x, int y)>();

		var startingY = map.IndexOf(map.First(x => x.Contains('^')));
		var startingX = map[startingY].IndexOf('^');
		var startingDirection = _directions[0];
		
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
				currentDirection = _directions[(_directions.IndexOf(currentDirection) + 1) % _directions.Count];
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

	private static void SimulateGuardPath(List<List<char>> map, out bool isLooping)
	{
		isLooping = false;

		var obstacleCollisions = new Dictionary<(int x, int y), int>();

		var startingY = map.IndexOf(map.First(x => x.Contains('^')));
		var startingX = map[startingY].IndexOf('^');
		var startingDirection = _directions[0];

		var currentY = startingY;
		var currentX = startingX;
		var currentDirection = _directions[0];

		do
		{
			var nextX = currentX + currentDirection.x;
			var nextY = currentY + currentDirection.y;

			if (nextY < 0 || nextX < 0 || nextY >= map.Count || nextX >= map[nextY].Count)
				break;
			
			if (map[nextY][nextX] == '#')
			{
				// Update to try new direction (90 degree right turn)
				currentDirection = _directions[(_directions.IndexOf(currentDirection) + 1) % _directions.Count];

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
	#endregion
}