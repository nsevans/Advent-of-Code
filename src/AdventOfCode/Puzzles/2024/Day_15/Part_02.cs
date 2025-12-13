namespace AdventOfCode.Puzzles.Year_2024.Day_15;

public class Part_02 : Day_15
{
	public override int Part => 2;

	public override string ResultMessage => "Sum of large boxes' GPS coordinates";

	private List<List<char>> _warehouseMap;
	private List<char> _movements;

	public override void PrepareData(List<string> input)
	{
		_warehouseMap = ParseWarehouseMap(input);
		_movements = ParseMovements(input);
	}

	private static List<List<char>> ParseWarehouseMap(List<string> input)
	{
		var map = new List<List<char>>();

		foreach (var line in input)
		{
			if (string.IsNullOrWhiteSpace(line))
				break;

			var mapRow = new List<char>();

			foreach (var item in line)
			{
				if (item == 'O')
					mapRow.AddRange(['[', ']']);
				else if (item == '@')
					mapRow.AddRange(['@', '.']);
				else
					mapRow.AddRange(Enumerable.Repeat(item, 2));
			}

			map.Add(mapRow);
		}

		return map;
	}

	public override string GetResult()
	{
		return CalculateSumOfLargeBoxGpsCoordinates(_warehouseMap, _movements).ToString();
	}

	private long CalculateSumOfLargeBoxGpsCoordinates(List<List<char>> warehouseMap, List<char> movements)
	{
		var currentRobotPosition = warehouseMap.IndexOf('@');

		foreach (var movement in movements)
		{
			var direction = CardinalDirections[movement];
			var nextMoves = new List<(int x, int y)>
			{
				currentRobotPosition
			};
			var canMove = true;

			for (var i = 0; i < nextMoves.Count; i++)
			{
				var nextPosition = (x: nextMoves[i].x + direction.x, y: nextMoves[i].y + direction.y);
				var itemAtNextPosition = warehouseMap.GetValueAtIndex(nextPosition);

				// Already in the list of items to check, so can skip any subsequent times
				if (nextMoves.Contains(nextPosition))
					continue;

				// Wall
				if (itemAtNextPosition == '#')
				{
					canMove = false;
					break;
				}
				else if (itemAtNextPosition == '[')
				{
					// Add '['
					nextMoves.Add(nextPosition);
					// Add ']'
					nextMoves.Add((x: nextPosition.x + 1, y: nextPosition.y));
				}
				else if (itemAtNextPosition == ']')
				{
					// Add ']'
					nextMoves.Add(nextPosition);
					// Add '['
					nextMoves.Add((x: nextPosition.x - 1, y: nextPosition.y));
				}
			}

			if (!canMove)
				continue;

			// Create copy as modification is happening to grid and original positions are needed
			var warehouseCopy = warehouseMap.Copy();


			// Set the current positions of the boxes to be free spaces
			foreach (var nextMovePosition in nextMoves[1..])
				warehouseMap.SetValueAtIndex(nextMovePosition, '.');

			// Set the next positions of the boxes to be the box value
			foreach (var nextMovePosition in nextMoves[1..])
				warehouseMap.SetValueAtIndex((x: nextMovePosition.x + direction.x, y: nextMovePosition.y + direction.y), warehouseCopy.GetValueAtIndex(nextMovePosition));

			var nextRobotPosition = (x: nextMoves[0].x + direction.x, y: nextMoves[0].y + direction.y);
			warehouseMap.SetValueAtIndex(nextMoves[0], '.');
			warehouseMap.SetValueAtIndex(nextRobotPosition, '@');
			currentRobotPosition = nextRobotPosition;
		}


		var indexes = warehouseMap.IndexesOf('[');
		var sum = indexes.Select(i => (long)((100 * i.y) + i.x)).Sum();
		return sum;
	}
}