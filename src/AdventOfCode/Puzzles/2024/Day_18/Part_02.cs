namespace AdventOfCode.Puzzles.Year_2024.Day_18;

public class Part_02 : Day_18
{
	public override int Part => 2;

	public override string ResultMessage => "The coordinate of the first byte that completely blocks the path";

	List<(int x, int y)> _incomingBytes;

	public override void PrepareData(List<string> input)
	{
		_incomingBytes = ParseIncomingBytes(input);
	}

	public override string GetResult()
	{
		return GetFirstBlockingByteCoordinate(_incomingBytes).ToString();
	}

	private static string GetFirstBlockingByteCoordinate(List<(int x, int y)> incomingBytes)
	{
		// Set to 1024 because the first puzzle shows that there is still a path after the first 1024
		// bytes have fallen
		var minBlockingBytes = 1024;
		var maxBlockingBytes = incomingBytes.Count - 1;

		while (minBlockingBytes < maxBlockingBytes)
		{
			var midPoint = (minBlockingBytes + maxBlockingBytes) / 2;
			if (TryTraverseMap(incomingBytes, midPoint + 1))
				minBlockingBytes = midPoint + 1;
			else
				maxBlockingBytes = midPoint;
		}

		var (x, y) = incomingBytes[minBlockingBytes];
		return $"{x},{y}";
	}

	private static bool TryTraverseMap(List<(int x, int y)> incomingBytes, int bytesFallen)
	{
		var map = GenerateMap();
		for (var i = 0; i < bytesFallen; i++)
		{
			var (x, y) = incomingBytes[i];
			map[y][x] = '#';
		}

		var queue = new Queue<(int x, int y)>();
		queue.Enqueue(StartPosition);
		var visited = new HashSet<(int x, int y)> { StartPosition };

		while (queue.Count > 0)
		{
			if (!queue.TryDequeue(out var position))
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
					return true;

				visited.Add((x, y));
				queue.Enqueue((x, y));
			}
		}

		return false;
	}
}