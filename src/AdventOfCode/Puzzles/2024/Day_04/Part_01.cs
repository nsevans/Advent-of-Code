using System;
using System.Collections.Generic;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode.Puzzles.Year_2024.Day_04;

public class Part_01 : Day_04
{
	public override int Part => 1;

	private List<List<char>> _wordSearch;

	public override void PrepareData(List<string> input)
	{
		_wordSearch = input.To2DCharList();
	}

	public override void Solve()
	{
		var result = CountNumberOfTimesWordAppears(_wordSearch);
		Console.WriteLine($"Number of times XMAS appears in word search: {result}");
	}

	private static int CountNumberOfTimesWordAppears(List<List<char>> wordSearch)
	{
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
}