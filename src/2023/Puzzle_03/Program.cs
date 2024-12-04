using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year_2023.Puzzle_03;

/// <summary>
/// Link: https://adventofcode.com/2023/day/3
/// Input Format: 
///		..172..............................454..46.......507..........809
///		............*.........712........=.......*................515.*..
///		.........823.835........%.........710.....749........134..%......
/// </summary>

public class Program
{
	private static string _validSymbols => "*=/%#&$-@+";
	private static char _specialSymbol => '*';

	public static void Main(string[] args)
	{
		Console.WriteLine("### 2023 - Puzzle 03 ###\n");
		var input = GetInput("./input.txt");

		var inputMap = input.Select(x => x.ToCharArray().ToList()).ToList();
		var resultPart1 = SumNumbersLinkedToSymbols(inputMap);
		Console.WriteLine($"[PART 1] Sum of symbol adjacent numbers: {resultPart1}");

		// Recreate input map because methods for calculating result are destructive
		inputMap = input.Select(x => x.ToCharArray().ToList()).ToList();
		var resultPart2 = SumProductOfNumbersLinkedToSpecialSymbol(inputMap);
		Console.WriteLine($"[PART 2] Sum of product of numbers adjacent to '*' when only 2 numbers are adjacent: {resultPart2}");
	}

	public static IEnumerable<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName);
	}

	private static int ParseNumberFromString(List<char> line, int index)
	{
		var startingIndex = index;
		// Find starting index of number (loop backwards until first digit of group is found)
		for (int i = index; i > 0; i--)
		{
			if (line[i - 1] == '.' || _validSymbols.Contains(line[i]))
				break;

			if (char.IsDigit(line[i - 1]))
				startingIndex = i - 1;
		}

		var numberAsString = "";
		// Compile number until end of list or first symbol is hit and  replace all found 
		// digits with '.' so they aren't counted twice
		for (int i = startingIndex; i < line.Count; i++)
		{
			if (line[i] == '.' || _validSymbols.Contains(line[i]))
				break;

			if (char.IsDigit(line[i]))
			{
				numberAsString += line[i];
				line[i] = '.';
			}
		}

		return int.Parse(numberAsString);
	}

	#region PART 1
	private static int SumNumbersLinkedToSymbols(List<List<char>> inputMap)
	{
		var sum = 0;

		for (int i = 0; i < inputMap.Count; i++)
		{
			var lineLength = inputMap[i].Count;
			for (int j = 0; j < lineLength; j++)
			{
				if (char.IsDigit(inputMap[i][j]) || inputMap[i][j] == '.')
					continue;

				if (_validSymbols.Contains(inputMap[i][j]))
				{
					// Top Left
					if (i - 1 >= 0 && j - 1 >= 0 && char.IsDigit(inputMap[i - 1][j - 1]))
					{
						sum += ParseNumberFromString(inputMap[i - 1], j - 1);
					}

					// Top Center
					if (i - 1 >= 0 && char.IsDigit(inputMap[i - 1][j]))
					{
						sum += ParseNumberFromString(inputMap[i - 1], j);
					}

					// Top Right
					if (i - 1 >= 0 && j + 1 < lineLength && char.IsDigit(inputMap[i - 1][j + 1]))
					{
						sum += ParseNumberFromString(inputMap[i - 1], j + 1);
					}

					// Middle Left
					if (j - 1 >= 0 && char.IsDigit(inputMap[i][j - 1]))
					{
						sum += ParseNumberFromString(inputMap[i], j - 1);
					}

					// Middle Right
					if (j + 1 < lineLength && char.IsDigit(inputMap[i][j + 1]))
					{
						sum += ParseNumberFromString(inputMap[i], j + 1);
					}

					// Bottom Left
					if (i + 1 < lineLength && j - 1 >= 0 && char.IsDigit(inputMap[i + 1][j - 1]))
					{
						sum += ParseNumberFromString(inputMap[i + 1], j - 1);
					}

					// Bottom Center
					if (i + 1 < lineLength && char.IsDigit(inputMap[i + 1][j]))
					{
						sum += ParseNumberFromString(inputMap[i + 1], j);
					}

					// Bottom Right
					if (i + 1 < lineLength && j + 1 < lineLength && char.IsDigit(inputMap[i + 1][j + 1]))
					{
						sum += ParseNumberFromString(inputMap[i + 1], j + 1);
					}
				}
			}
		}

		return sum;
	}
	#endregion

	#region PART 2
	private static int SumProductOfNumbersLinkedToSpecialSymbol(List<List<char>> inputMap)
	{
		var sum = 0;
		for (int i = 0; i < inputMap.Count; i++)
		{
			var lineLength = inputMap[i].Count;
			for (int j = 0; j < lineLength; j++)
			{
				if (char.IsDigit(inputMap[i][j]) || inputMap[i][j] == '.')
					continue;

				if (_specialSymbol == inputMap[i][j])
				{
					var adjacents = GetListOfAdjacents(inputMap, i, j);

					if (adjacents.Count == 2)
						sum += adjacents[0] * adjacents[1];
				}
			}
		}
		return sum;
	}

	private static List<int> GetListOfAdjacents(List<List<char>> inputMap, int i, int j)
	{
		var adjacents = new List<int>();
		var lineLength = inputMap[i].Count;

		// Top Left
		if (i - 1 >= 0 && j - 1 >= 0 && char.IsDigit(inputMap[i - 1][j - 1]))
		{
			adjacents.Add(ParseNumberFromString(inputMap[i - 1], j - 1));
		}

		// Top Center
		if (i - 1 >= 0 && char.IsDigit(inputMap[i - 1][j]))
		{
			adjacents.Add(ParseNumberFromString(inputMap[i - 1], j));
		}

		// Top Right
		if (i - 1 >= 0 && j + 1 < lineLength && char.IsDigit(inputMap[i - 1][j + 1]))
		{
			adjacents.Add(ParseNumberFromString(inputMap[i - 1], j + 1));
		}

		// Middle Left
		if (j - 1 >= 0 && char.IsDigit(inputMap[i][j - 1]))
		{
			adjacents.Add(ParseNumberFromString(inputMap[i], j - 1));
		}

		// Middle Right
		if (j + 1 < lineLength && char.IsDigit(inputMap[i][j + 1]))
		{
			adjacents.Add(ParseNumberFromString(inputMap[i], j + 1));
		}

		// Bottom Left
		if (i + 1 < lineLength && j - 1 >= 0 && char.IsDigit(inputMap[i + 1][j - 1]))
		{
			adjacents.Add(ParseNumberFromString(inputMap[i + 1], j - 1));
		}

		// Bottom Center
		if (i + 1 < lineLength && char.IsDigit(inputMap[i + 1][j]))
		{
			adjacents.Add(ParseNumberFromString(inputMap[i + 1], j));
		}

		// Bottom Right
		if (i + 1 < lineLength && j + 1 < lineLength && char.IsDigit(inputMap[i + 1][j + 1]))
		{
			adjacents.Add(ParseNumberFromString(inputMap[i + 1], j + 1));
		}

		return adjacents;
	}

	#endregion
}

