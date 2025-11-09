using System;
using System.Collections.Generic;
using AdventOfCode.Common.Constants;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode.Puzzles.Year_2024.Day_16;

public class Part_01 : Day_16
{
	public override int Part => 1;

	List<List<char>> _maze;

	public override void PrepareData(List<string> input)
	{
		_maze = input.To2DCharList();
	}

	public override string ResultMessage => "Lowest maze cost a reindeer could get";

	public override string GetResult()
	{
		return CalculateMazeBestPathScore(_maze).ToString();
	}

	private int CalculateMazeBestPathScore(List<List<char>> maze)
	{
		var startIndex = maze.IndexOf('S');
		var endIndex = maze.IndexOf('E');

		var score = FindShortestPathLength(maze, startIndex, endIndex);

		return score;
	}

	private static int FindShortestPathLength(List<List<char>> maze, (int x, int y) startIndex, (int x, int y) endIndex)
	{
		var (sx, sy) = startIndex;
		var (sdx, sdy) = Directions.Cardinal[0];
		var start = new MazeState(sx, sy, sdx, sdy);

		var priorityQueue = new PriorityQueue<MazeState, int>();
		priorityQueue.Enqueue(start, 0);

		var visited = new HashSet<MazeState> { start };

		while (priorityQueue.Count > 0)
		{
			if (!priorityQueue.TryDequeue(out var state, out var cost))
				continue;

			if ((state.X, state.Y) == endIndex)
				return cost;

			visited.Add(state);

			var potentialNextMoves = new List<(MazeState, int c)>()
			{
				// Move one spot without changing direction
				(new(state.X + state.Dx, state.Y + state.Dy, state.Dx, state.Dy), cost + 1),
				// Rotate clockwise
				(new(state.X, state.Y, -state.Dy, state.Dx), cost + 1000),
				// Rotate counter clockwise
				(new(state.X, state.Y, state.Dy, -state.Dx), cost + 1000)
			};

			foreach (var (nextMove, nextCost) in potentialNextMoves)
			{
				// Out of bounds or hitting a wall
				if (!maze.IsSafe(nextMove.X, nextMove.Y, ['#']))
					continue;

				// Already visited and found cheapest
				if (visited.Contains(nextMove))
					continue;

				priorityQueue.Enqueue(nextMove, nextCost);
			}
		}

		return 0;
	}
}