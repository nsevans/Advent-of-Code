namespace AdventOfCode.Puzzles.Year_2024.Day_04;

public class Part_02 : Day_04
{
	public override int Part => 2;

	public override string ResultMessage => "Number of times an X of MAS appears in word search";

	private List<List<char>> _wordSearch;

	public override void PrepareData(List<string> input)
	{
		_wordSearch = input.To2DCharList();
	}

	public override string GetResult()
	{
		return CountNumberOfTimesWordShapeAppears(_wordSearch).ToString();
	}

	private static int CountNumberOfTimesWordShapeAppears(List<List<char>> wordSearch)
	{
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
}