namespace AdventOfCode.Puzzles.Year_2024.Day_16;

public class Part_02 : Day_16
{
	public override int Part => 2;

	public override string ResultMessage => "Number of tiles in all best paths";

	List<List<char>> _maze;

	public override void PrepareData(List<string> input)
	{
		_maze = input.To2DCharList();
	}

	public override string GetResult()
	{
		return CalculateMazeBestPathScore(_maze).ToString();
	}

	private static int CalculateMazeBestPathScore(List<List<char>> maze)
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

		var lowestCosts = new Dictionary<MazeState, int>
		{
			{ start, 0 }
		};

		var backtrack = new Dictionary<MazeState, HashSet<MazeState>>();
		var endStates = new HashSet<MazeState>();

		var bestCost = int.MaxValue;

		while (priorityQueue.Count > 0)
		{
			if (!priorityQueue.TryDequeue(out var state, out var cost))
				continue;

			if (cost > lowestCosts.GetValueOrDefinedDefault(state, int.MaxValue))
				continue;

			if ((state.X, state.Y) == endIndex)
			{
				if (cost > bestCost)
					break;

				bestCost = cost;
				endStates.Add(state);
			}

			var potentialNextMoves = new List<(MazeState, int c)>()
			{
				// Move one spot without changing direction
				(new(state.X + state.Dx, state.Y + state.Dy, state.Dx, state.Dy), cost + 1),
				// Rotate clockwise
				(new(state.X, state.Y, -state.Dy, state.Dx), cost + 1000),
				// Rotate counter clockwise
				(new(state.X, state.Y, state.Dy, -state.Dx), cost + 1000)
			};

			foreach (var (nextState, nextCost) in potentialNextMoves)
			{
				// Out of bounds or hitting a wall
				if (!maze.IsSafe(nextState.X, nextState.Y, ['#']))
					continue;

				var lowestCost = lowestCosts.GetValueOrDefinedDefault(nextState, int.MaxValue);
				if (nextCost > lowestCost)
					continue;

				if (nextCost < lowestCost)
				{
					backtrack[nextState] = [];
					lowestCosts[nextState] = nextCost;
				}
				backtrack[nextState].Add(state);

				priorityQueue.Enqueue(nextState, nextCost);
			}
		}

		var states = new List<MazeState>(endStates);
		var visited = new HashSet<MazeState>(endStates);

		while (states.Count > 0)
		{
			var key = states.First();
			states = states[1..];
			foreach (var lastState in backtrack.GetValueOrDefinedDefault(key, []))
			{
				if (visited.Contains(lastState))
					continue;
				visited.Add(lastState);
				states.Add(lastState);
			}
		}

		return visited.Select(x => (x.X, x.Y))
			.Distinct()
			.Count();
	}
}