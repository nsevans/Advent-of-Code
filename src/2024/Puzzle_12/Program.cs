using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year_2024.Puzzle_12;

/// <summary>
/// Link: https://adventofcode.com/2024/day/12
/// Input Format:
/// 	AAAA
///		BBCD
///		BBCC
///		EEEC
/// </summary>
public class Program
{
	private static List<(int dx, int dy)> _cardinalDirections = [
		(-1,  0),	// WEST
		( 0, -1),	// NORTH
		( 1,  0),	// EAST
		( 0,  1)	// SOUTH
	];
	private static List<(int dx, int dy)> _ordinalDirections = [
		(-1, -1),	// NORTH WEST
		( 1, -1),	// NORTH EAST
		( 1,  1),	// SOUTH EAST
		(-1,  1)	// SOUTH WEST
	];
	private static List<(int dx, int dy)> _allDirections = [
		(-1,  0),	// WEST
		(-1, -1),	// NORTH WEST
		( 0, -1),	// NORTH
		( 1, -1),	// NORTH EAST
		( 1,  0),	// EAST
		( 1,  1),	// SOUTH EAST
		( 0,  1),	// SOUTH
		(-1,  1)	// SOUTH WEST
	];

	public static void Main(string[] args)
	{
		Console.WriteLine("### 2024 - Puzzle 12 ###\n");
		var input = GetInput("./input.txt");

		var resultPart1 = CalculateSumOfFencePrices(input);
		Console.WriteLine($"[PART 1] Sum of fence prices using area and perimeter: {resultPart1}");

		var resultPart2 = CalculateSumOfDiscountedFencePrices(input);
		Console.WriteLine($"[PART 2] Sum of fence prices discounted using area and sides: {resultPart2}");
	}

	public static List<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName).ToList();
	}

	private static List<List<char>> ParseInput(IEnumerable<string> input)
	{
		// Pad top and bottom with padding character
		var horizontalPad = Enumerable.Repeat('_', input.Count() + 2).ToList();

		var newInput = new List<List<char>> { horizontalPad };

		newInput.AddRange(input
			.Select(x =>
			{
				// Pad start of line
				var line = new List<char>
                {
                    '_'
                };
				line.AddRange(x.ToCharArray());
				// Pad end of line
				line.Add('_');
				return line;
			}));

		newInput.Add(horizontalPad);
		return newInput;
	}

	#region PART 1
	private static int CalculateSumOfFencePrices(IEnumerable<string> input)
	{
		var map = ParseInput(input);

		var sum = 0;

		for (var y = 1; y < map.Count - 1; y++)
		{
			for (var x = 1; x < map[y].Count - 1; x++)
			{
				var plant = map[y][x];
				// Unique token per plant type A-Z that can be used to mark spots that have already been evaluated
				var evaluatedToken = (char)(plant - 32);

				// Already evaluated if not in range of A-Z
				if (plant < 65 || plant > 90)
					continue;

				(var area, var perimeter) = CalculateFencePriceRecursive(map, plant, evaluatedToken, x, y);
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
	#endregion

	#region PART 2
	private static int CalculateSumOfDiscountedFencePrices(IEnumerable<string> input)
	{
		var map = ParseInput(input);

		var sum = 0;

		for (var y = 1; y < map.Count - 1; y++)
		{
			for (var x = 1; x < map[y].Count - 1; x++)
			{
				var plant = map[y][x];
				// Unique token per plant type A-Z that can be used to mark spots that have already been evaluated
				var evaluatedToken = (char)(plant - 32);

				// Check if item is already evaluated
				if (plant < 65 || plant > 90)
					continue;

				(var area, var corners) = CalculateAreaOfGardenRecursive(map, plant, evaluatedToken, x, y);
				sum += area * corners;
			}
		}

		return sum;
	}

	private static (int area, int corners) CalculateAreaOfGardenRecursive(List<List<char>> map, char plant, char evaluatedToken, int x, int y)
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

	private static int GetOutsideCornerCount(List<List<char>> map, char plant, char evaluatedToken, int x, int y)
	{
		var plantValues = new List<char> { plant, evaluatedToken };
		var corners = 0;

		// Check left and top, top and right, right and bottom, and bottom and left corners
		foreach (var (dir1, index) in _cardinalDirections.Select((d, i) => (d, i)))
		{
			var dir2 = _cardinalDirections[(index + 1) % _cardinalDirections.Count];

			var point1 = map[y + dir1.dy][x + dir1.dx];
			var point2 = map[y + dir2.dy][x + dir2.dx];
			
			// If neither sides are matching, then this side must be a corner
			if (!plantValues.Contains(point1) && !plantValues.Contains(point2))
				corners++;
		}
		return corners;
	}

	private static int GetInsideCornerCount(List<List<char>> map, char plant, char evaluatedToken, int x, int y)
	{
		var plantValues = new List<char> { plant, evaluatedToken };
		var corners = 0;

		// Check (left, top left, and top), (top, top right, and right), 
		// (right, bottom right, and bottom), and (bottom, bottom left, and left)
		for (var i = 0; i < _allDirections.Count; i += 2)
		{
			var dir1 = _allDirections[i];
			var dir2 = _allDirections[(i + 1) % _allDirections.Count];
			var dir3 = _allDirections[(i + 2) % _allDirections.Count];

			var point1 = map[y + dir1.dy][x + dir1.dx];
			var point2 = map[y + dir2.dy][x + dir2.dx];
			var point3 = map[y + dir3.dy][x + dir3.dx];
			
			// If the two sides are matching but the diagonal side isn't, this must be an inside corner
			if (plantValues.Contains(point1) && !plantValues.Contains(point2) && plantValues.Contains(point3))
				corners++;
		}
		
		return corners;
	}
	#endregion
}