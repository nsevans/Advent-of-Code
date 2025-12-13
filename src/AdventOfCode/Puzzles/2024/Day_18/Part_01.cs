namespace AdventOfCode.Puzzles.Year_2024.Day_18;

public class Part_01 : Day_18
{
	public override int Part => 1;

	public override string ResultMessage => "The minimum number of steps needed to reach the exit";

	List<(int x, int y)> _incomingBytes;
	List<List<char>> _map;

	private static readonly int _bytesFallen = 1024;

	public override void PrepareData(List<string> input)
	{
		_incomingBytes = ParseIncomingBytes(input);
		_map = GenerateMap();
	}

	public override string GetResult()
	{
		return TraverseMemoryMap(_map, _incomingBytes).ToString();
	}

	private static int TraverseMemoryMap(List<List<char>> map, List<(int x, int y)> incomingBytes)
	{
		for (var i = 0; i < _bytesFallen; i++)
		{
			var (x, y) = incomingBytes[i];
			map[y][x] = '#';
		}

		return GetMinimumStepsToExit(map);
	}

	private static int GetMinimumStepsToExit(List<List<char>> map)
	{
		var priorityQueue = new PriorityQueue<(int x, int y), int>();
		priorityQueue.Enqueue(StartPosition, 0);
		var visited = new HashSet<(int x, int y)> { StartPosition };

		while (priorityQueue.Count > 0)
		{
			if (!priorityQueue.TryDequeue(out var position, out var steps))
				continue;

			var potentialNextMoves = new List<(int x, int y)>()
			{
				// Up
				(position.x, position.y - 1),
				// Down
				(position.x, position.y + 1),
				// Left
				(position.x - 1, position.y),
				// Right
				(position.x + 1, position.y)
			};

			foreach (var (x, y) in potentialNextMoves)
			{
				if (!map.IsSafe(x, y, ['#']))
					continue;

				if (visited.Contains((x, y)))
					continue;

				if ((x, y) == EndPosition)
					return steps + 1;

				visited.Add((x, y));
				priorityQueue.Enqueue((x, y), steps + 1);
			}
		}

		return 0;
	}
}