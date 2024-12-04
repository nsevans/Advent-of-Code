using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year_2024.Puzzle_04;

/// <summary>
/// Link: https://adventofcode.com/2024/day/4
/// Input Format:
/// 	MMMSXXMASM
/// 	MSAMXMSMSA
/// 	AMXSXMAAMM
/// 	MSAMASMSMX
/// 	XMASAMXAMM
/// 	XXAMMXXAMA
/// 	SMSMSASXSS
/// 	SAXAMASAAA
/// 	MAMMMXMMMM
/// 	MXMXAXMASX
/// </summary>
public class Program
{
	private static List<(int y, int x)> Directions => [
		(0,  -1),	// WEST
		(-1, -1),	// SOUTH WEST 
		(-1,  0),	// SOUTH
		(-1,  1),	// SOUTH EAST
		( 0,  1),	// EAST
		( 1,  1),	// NORTH EAST
		( 1,  0),	// NORTH
		( 1, -1)	// NORTH WEST
	];

	public static void Main(string[] args)
	{
		Console.WriteLine("### 2024 - Puzzle 04 ###\n");
		var input = GetInput("./input.txt");

		var resultPart1 = CountNumberOfTimesWordAppears(input);
		Console.WriteLine($"[PART 1] Number of times XMAS appears in input: {resultPart1}");

		var resultPart2 = CountNumberOfTimesWordShapeAppears(input);
		Console.WriteLine($"[PART 2] Number of times an X of MAS appears in input: {resultPart2}");
	}

	public static List<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName).ToList();
	}

	#region PART 1
	private static int CountNumberOfTimesWordAppears(IEnumerable<string> input)
	{
		var wordSearch = input.Select(x => x.ToCharArray().ToList()).ToList();
		var wordCount = 0;

		for (int i = 0; i < wordSearch.Count; i++)
		{
			for (int j = 0; j < wordSearch[i].Count; j++)
			{
				var currentLetter = wordSearch[i][j];
				var lineLength = wordSearch[i].Count;

				if (currentLetter == 'X')
					wordCount += SearchForWord(wordSearch, "XMAS", j, i);
			}
		}

		return wordCount;
	}

	private static int SearchForWord(List<List<char>> wordSearch, string word, int x, int y)
	{
		var wordCount = 0;
		for (var direction = 0; direction < 8; direction++)
		{
			if (SearchForWordRecursive(wordSearch, word, x, y, direction))
				wordCount++;
		}
		return wordCount;
	}

	private static bool SearchForWordRecursive(List<List<char>> wordSearch, string word, int x, int y, int direction)
	{
		if (word.Length == 0)
			return true;

		if (x < 0 || y < 0 || y >= wordSearch.Count || x >= wordSearch[y].Count)
			return false;

		if (wordSearch[y][x] != word[0])
			return false;

		var newX = x + Directions[direction].x;
		var newY = y + Directions[direction].y;
		return SearchForWordRecursive(wordSearch, word[1..], newX, newY, direction);
	}
	#endregion

	#region PART 2
	private static int CountNumberOfTimesWordShapeAppears(IEnumerable<string> input)
	{
		var wordSearch = input.Select(x => x.ToCharArray().ToList()).ToList();

		var shapeCount = 0;

		// Start at 1 and end before length - 1 as top and bottom outer layer don't need to be accounted for
		for (int i = 1; i < wordSearch.Count - 1; i++)
		{
			// Start at 1 and end before length - 1 as left and right outer layer don't need to be accounted for
			for (int j = 1; j < wordSearch[i].Count - 1; j++)
			{
				if (wordSearch[i][j] == 'A')
				{
					var sectionOneIsValid = false;
					// Check if TOP LEFT to BOTTOM RIGHT or BOTTOM RIGHT to TOP LEFT is valid
					if (wordSearch[i - 1][j - 1] == 'M' && wordSearch[i + 1][j + 1] == 'S' || wordSearch[i - 1][j - 1] == 'S' && wordSearch[i + 1][j + 1] == 'M')
						sectionOneIsValid = true;

					var sectionTwoIsValid = false;
					// Check if TOP RIGHT to BOTTOM LEFT or BOTTOM LEFT to TOP RIGHT is valid
					if ((wordSearch[i - 1][j + 1] == 'M' && wordSearch[i + 1][j - 1] == 'S') || (wordSearch[i - 1][j + 1] == 'S' && wordSearch[i + 1][j - 1] == 'M'))
						sectionTwoIsValid = true;

					if (sectionOneIsValid && sectionTwoIsValid)
						shapeCount++;
				}	
			}
		}
		return shapeCount;
	}
	#endregion
}
