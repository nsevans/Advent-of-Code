using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Year_2023.Puzzle_02;

/// <summary>
/// Link: https://adventofcode.com/2023/day/2
/// Input Format: Game 1: 2 green, 6 blue, 7 red; 12 green, 6 blue, 3 red; 5 red, 18 green, 4 blue
/// </summary>
/// 
public class Program
{
	private const int _redLimit = 12;
	private const int _greenLimit = 13;
	private const int _blueLimit = 14;
	private const int _overallLimit = 14;

	public static void Main(string[] args)
	{
		Console.WriteLine("### 2023 - Puzzle 02 ###\n");
		var input = GetInput("input.txt");

		var resultPart1 = CalculateValidGames(input);
		Console.WriteLine($"[PART 1] Sum of Game IDs that match criteria: {resultPart1}");

		var resultPart2 = CalculateSumOfPowerOfEachGame(input);
		Console.WriteLine($"[PART 2] Sum of the powers of the fewest cubes of each color per game: {resultPart2}");
	}

	public static IEnumerable<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName);
	}

	#region PART 1
	private static int CalculateValidGames(IEnumerable<string> input)
	{
		var sum = 0;
		foreach (var line in input)
		{
			var parsedLine = line.Split(' ');

			// Each line starts with "Game ##:", so we can safely assume that
			// the id is at index 1 after splitting and removing ':' from the end
			var gameId = int.Parse(parsedLine[1][..^1]);

			var isValidGame = true;
			for (int i = 2; i < parsedLine.Length; i += 2)
			{
				var number = int.Parse(parsedLine[i]);
				var color = parsedLine[i + 1];

				isValidGame =
					// If any of the inputs exceed the overall limit we can save at least
					// a calculation since it doesn't matter what colour it's for
					number <= _overallLimit
					// Validate the first letter of each color since they're unique
					&& (
						(color[0] == 'r' && number <= _redLimit)
						|| (color[0] == 'g' && number <= _greenLimit)
						|| (color[0] == 'b' && number <= _blueLimit)
					);

				// As soon as it's not a valid game stop iterating
				if (!isValidGame)
					break;
			}

			if (isValidGame)
				sum += gameId;
		}

		return sum;
	}
	#endregion

	#region PART 2
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
	#endregion
}