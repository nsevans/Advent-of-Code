using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode.Puzzles.Year_2024.Day_15;

public class Part_01 : Day_15
{
	public override int Part => 1;

	private List<List<char>> _warehouseMap;
	private List<char> _movements;

	public override void PrepareData(List<string> input)
	{
		_warehouseMap = ParseWarehouseMap(input);
		_movements = ParseMovements(input);
	}

	private List<List<char>> ParseWarehouseMap(List<string> input)
	{
		var map = new List<List<char>>();

		foreach(var line in input)
		{
			if (string.IsNullOrWhiteSpace(line))
				break;

			map.Add(line.ToCharArray().ToList());
		}

		return map;
	}

	public override string ResultMessage => "Sum of boxes' GPS coordinats";

	public override string GetResult()
	{
		return CalculateSumOfBoxGpsCoordinates(_warehouseMap, _movements).ToString();
	}

	private long CalculateSumOfBoxGpsCoordinates(List<List<char>> warehouseMap, List<char> movements)
	{
		var currentPosition = warehouseMap.IndexOf('@');

		foreach (var movement in movements)
		{
			var direction = _cardinalDirections[movement];
			var nextPosition = (x: currentPosition.x + direction.x, y: currentPosition.y + direction.y);

			var itemAtNextPosition = warehouseMap.GetValueAtIndex(nextPosition);

			// Wall, can't move
			if (itemAtNextPosition == '#')
				continue;

			// Free Space, move freely
			if (itemAtNextPosition == '.')
			{
				currentPosition = MoveToFreeSpace(warehouseMap, currentPosition, nextPosition);
				continue;
			}

			// Box, push if space
			if (itemAtNextPosition == 'O')
			{
				if (MoveBoxes(warehouseMap, direction, currentPosition))
					currentPosition = nextPosition;
				continue;
			}
		}


		var indexes = warehouseMap.IndexesOf('O');
		var sum = indexes.Select(i => (long)((100 * i.y) + i.x)).Sum();

		return sum;
	}

	private bool MoveBoxes(List<List<char>> warehouseMap, (int x, int y) direction, (int x, int y) currentPosition)
	{
		if (!warehouseMap.IsInBounds(currentPosition.x, currentPosition.y))
			return false;

		var itemAtCurrentPosition = warehouseMap.GetValueAtIndex(currentPosition);

		var nextPosition = (x: currentPosition.x + direction.x, y: currentPosition.y + direction.y);
		var itemAtNextPosition = warehouseMap.GetValueAtIndex(nextPosition);

		if (itemAtNextPosition == 'O')
		{
			var isValidMove = MoveBoxes(warehouseMap, direction, nextPosition);
			
			if (isValidMove)
			{
				warehouseMap.SetValueAtIndex(currentPosition, '.');
				warehouseMap.SetValueAtIndex(nextPosition, itemAtCurrentPosition);
				return true;
			}

			return false;
		}

		if (itemAtNextPosition == '.')
		{
			warehouseMap.SetValueAtIndex(nextPosition, itemAtCurrentPosition);
			warehouseMap.SetValueAtIndex(currentPosition, '.');
			return true;
		}

		return false;
	}
}