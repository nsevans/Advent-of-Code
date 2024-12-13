using System;
using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2023.Day_02;

public class Part_02 : Day_02
{
	public override int Part => 2;

	private List<string> _preparedInput;
    
	public override void PrepareData(List<string> input)
    {
		_preparedInput = input;
    }

    public override void Solve()
    {
		var result = CalculateSumOfPowerOfEachGame(_preparedInput);
		Console.WriteLine($"Sum of the powers of the fewest cubes of each color per game: {result}");
    }

	private static long CalculateSumOfPowerOfEachGame(IEnumerable<string> input)
	{
		var sum = 0L;
		foreach (var line in input)
		{
			var minRed = int.MinValue;
			var minGreen = int.MinValue;
			var minBlue = int.MinValue;

			var parsedLine = line.Split(' ');
			for (int i = 2; i < parsedLine.Length; i += 2)
			{
				var number = int.Parse(parsedLine[i]);
				var color = parsedLine[i + 1];

				if (color[0] == 'r')
					minRed = number > minRed ? number : minRed;
				else if (color[0] == 'g')
					minGreen = number > minGreen ? number : minGreen;
				else if (color[0] == 'b')
					minBlue = number > minBlue ? number : minBlue;
			}

			sum += minRed * minGreen * minBlue;
		}

		return sum;
	}
}