using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2023.Day_03;

public class Part_02 : Day_03
{
	public override int Part => 2;

	private static char _specialSymbol => '*';

	private List<List<char>> _preparedInput;

    public override void PrepareData(List<string> input)
    {
        _preparedInput = input.Select(x => x.ToCharArray().ToList()).ToList();
    }

	public override string ResultMessage => "Sum of product of numbers adjacent to '*' when only 2 numbers are adjacent";

    public override string GetResult()
    {
		return SumProductOfNumbersLinkedToSpecialSymbol(_preparedInput).ToString();
	}

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
}
