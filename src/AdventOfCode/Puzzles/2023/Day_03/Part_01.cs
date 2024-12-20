using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2023.Day_03;

public class Part_01 : Day_03
{
	public override int Part => 1;

	private List<List<char>> _preparedInput;
    
	public override void PrepareData(List<string> input)
    {
        _preparedInput = input.Select(x => x.ToCharArray().ToList()).ToList();
    }

    public override void Solve()
    {
		var result = SumNumbersLinkedToSymbols(_preparedInput);
		Console.WriteLine($"Sum of symbol adjacent numbers: {result}");
    }

	private int SumNumbersLinkedToSymbols(List<List<char>> inputMap)
	{
		var sum = 0;

		for (int i = 0; i < inputMap.Count; i++)
		{
			var lineLength = inputMap[i].Count;
			for (int j = 0; j < lineLength; j++)
			{
				if (char.IsDigit(inputMap[i][j]) || inputMap[i][j] == '.')
					continue;

				if (ValidSymbols.Contains(inputMap[i][j]))
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
}