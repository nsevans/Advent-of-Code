using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2024.Day_12;

public class Part_01 : Day_12
{
	public override int Part => 1;

	public override string ResultMessage => "Sum of fence prices using area and perimeter";

	List<List<char>> _gardenMap;

	public override void PrepareData(List<string> input)
	{
		_gardenMap = ParsePaddedGardenMap(input);
	}

	public override string GetResult()
	{
		return CalculateSumOfFencePrices(_gardenMap).ToString();
	}

	private static int CalculateSumOfFencePrices(List<List<char>> gardenMap)
	{
		var sum = 0;

		for (var y = 1; y < gardenMap.Count - 1; y++)
		{
			for (var x = 1; x < gardenMap[y].Count - 1; x++)
			{
				var plant = gardenMap[y][x];
				// Unique token per plant type A-Z that can be used to mark spots that have already been evaluated
				var evaluatedToken = (char)(plant - 32);

				// Already evaluated if not in range of A-Z
				if (plant < 65 || plant > 90)
					continue;

				(var area, var perimeter) = CalculateFencePriceRecursive(gardenMap, plant, evaluatedToken, x, y);
				sum += area * perimeter;
			}
		}

		return sum;
	}

	private static (int area, int perimeter) CalculateFencePriceRecursive(List<List<char>> map, char plant, char evaluatedToken, int x, int y)
	{
		// Mark the current spot as evaluated
		map[y][x] = evaluatedToken;

		var area = 1;
		// Current number of adjacent plants that are different than the current one
		var perimeter = 4;

		// Check left
		if (x - 1 >= 0)
		{
			var leftPlant = map[y][x - 1];
			// If 1 left is within the borders of the padding and the same plant type
			if (x - 1 >= 1 && leftPlant == plant)
			{
				var result = CalculateFencePriceRecursive(map, plant, evaluatedToken, x - 1, y);
				area += result.area;
				perimeter += result.perimeter;
				perimeter--;
			}
			// If there is an adjacent matching plant
			else if (leftPlant == evaluatedToken || leftPlant == plant)
				perimeter--;
		}

		// Check right
		if (x + 1 < map[y].Count)
		{
			var rightPlant = map[y][x + 1];
			// If 1 right is within the borders of the padding and the same plant type
			if (x + 1 < map[y].Count - 1 && rightPlant == plant)
			{
				var result = CalculateFencePriceRecursive(map, plant, evaluatedToken, x + 1, y);
				area += result.area;
				perimeter += result.perimeter;
				perimeter--;
			}
			// If there is an adjacent matching plant
			else if (rightPlant == evaluatedToken || rightPlant == plant)
				perimeter--;
		}

		// Check up
		if (y - 1 >= 0)
		{
			var topPlant = map[y - 1][x];
			// If 1 up is within the borders of the padding and the same plant type
			if (y - 1 >= 1 && topPlant == plant)
			{
				var result = CalculateFencePriceRecursive(map, plant, evaluatedToken, x, y - 1);
				area += result.area;
				perimeter += result.perimeter;
				perimeter--;
			}
			// If there is an adjacent matching plant
			else if (topPlant == evaluatedToken || topPlant == plant)
				perimeter--;
		}

		// Check down
		if (y + 1 < map.Count)
		{
			var bottomPlant = map[y + 1][x];
			// If 1 down is within the borders of the padding and the same plant type
			if (y + 1 < map.Count - 1 && bottomPlant == plant)
			{
				var result = CalculateFencePriceRecursive(map, plant, evaluatedToken, x, y + 1);
				area += result.area;
				perimeter += result.perimeter;
				perimeter--;
			}
			// If there is an adjacent matching plant
			else if (bottomPlant == evaluatedToken || bottomPlant == plant)
				perimeter--;
		}

		return (area, perimeter);
	}
}