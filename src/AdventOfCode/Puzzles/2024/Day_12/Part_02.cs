using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_12;

public class Part_02 : Day_12
{
	public override int Part => 2;

	List<List<char>> _gardenMap;

	public override void PrepareData(List<string> input)
	{
		_gardenMap = ParsePaddedGardenMap(input);
	}

	public override void Solve()
	{
		var result = CalculateSumOfDiscountedFencePrices(_gardenMap);
		Console.WriteLine($"Sum of discounted fence prices using area and sides: {result}");
	}

	private int CalculateSumOfDiscountedFencePrices(List<List<char>> gardenMap)
	{
		var sum = 0;

		for (var y = 1; y < gardenMap.Count - 1; y++)
		{
			for (var x = 1; x < gardenMap[y].Count - 1; x++)
			{
				var plant = gardenMap[y][x];
				// Unique token per plant type A-Z that can be used to mark spots that have already been evaluated
				var evaluatedToken = (char)(plant - 32);

				// Check if item is already evaluated
				if (plant < 65 || plant > 90)
					continue;

				(var area, var corners) = CalculateAreaOfGardenRecursive(gardenMap, plant, evaluatedToken, x, y);
				sum += area * corners;
			}
		}

		return sum;
	}

	private (int area, int corners) CalculateAreaOfGardenRecursive(List<List<char>> map, char plant, char evaluatedToken, int x, int y)
	{
		// Mark the current spot as evaluated
		map[y][x] = evaluatedToken;
		
		var area = 1;
		// Calculate corners of current position
		var corners = GetOutsideCornerCount(map, plant, evaluatedToken, x, y) + GetInsideCornerCount(map, plant, evaluatedToken, x, y);

		// Check left
		if (map[y][x - 1] == plant)
		{
			var result = CalculateAreaOfGardenRecursive(map, plant, evaluatedToken, x - 1, y);
			area += result.area;
			corners += result.corners;
		}

		// Check right
		if (map[y][x + 1] == plant)
		{
			var result = CalculateAreaOfGardenRecursive(map, plant, evaluatedToken, x + 1, y);
			area += result.area;
			corners += result.corners;
		}

		// Check top
		if (map[y - 1][x] == plant)
		{
			var result = CalculateAreaOfGardenRecursive(map, plant, evaluatedToken, x, y - 1);
			area += result.area;
			corners += result.corners;
		}

		// Check bottom
		if (map[y + 1][x] == plant)
		{
			var result = CalculateAreaOfGardenRecursive(map, plant, evaluatedToken, x, y + 1);
			area += result.area;
			corners += result.corners;
		}

		return (area, corners);
	}

	private int GetOutsideCornerCount(List<List<char>> map, char plant, char evaluatedToken, int x, int y)
	{
		var plantValues = new List<char> { plant, evaluatedToken };
		var corners = 0;

		// Check left and top, top and right, right and bottom, and bottom and left corners
		foreach (var (dir1, index) in CardinalDirections.Select((d, i) => (d, i)))
		{
			var dir2 = CardinalDirections[(index + 1) % CardinalDirections.Count];

			var point1 = map[y + dir1.dy][x + dir1.dx];
			var point2 = map[y + dir2.dy][x + dir2.dx];
			
			// If neither sides are matching, then this side must be a corner
			if (!plantValues.Contains(point1) && !plantValues.Contains(point2))
				corners++;
		}
		return corners;
	}

	private int GetInsideCornerCount(List<List<char>> map, char plant, char evaluatedToken, int x, int y)
	{
		var plantValues = new List<char> { plant, evaluatedToken };
		var corners = 0;

		// Check (left, top left, and top), (top, top right, and right), 
		// (right, bottom right, and bottom), and (bottom, bottom left, and left)
		for (var i = 0; i < AllDirections.Count; i += 2)
		{
			var dir1 = AllDirections[i];
			var dir2 = AllDirections[(i + 1) % AllDirections.Count];
			var dir3 = AllDirections[(i + 2) % AllDirections.Count];

			var point1 = map[y + dir1.dy][x + dir1.dx];
			var point2 = map[y + dir2.dy][x + dir2.dx];
			var point3 = map[y + dir3.dy][x + dir3.dx];
			
			// If the two sides are matching but the diagonal side isn't, this must be an inside corner
			if (plantValues.Contains(point1) && !plantValues.Contains(point2) && plantValues.Contains(point3))
				corners++;
		}
		
		return corners;
	}
}