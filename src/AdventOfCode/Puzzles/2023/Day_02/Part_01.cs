using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2023.Day_02;

public class Part_01 : Day_02
{
	public override int Part => 1;

	public override string ResultMessage => "Sum of Game IDs that match criteria";

	private const int _redLimit = 12;
	private const int _greenLimit = 13;
	private const int _blueLimit = 14;
	private const int _overallLimit = 14;

	private List<string> _preparedInput;

    public override void PrepareData(List<string> input)
    {
		_preparedInput = input;
    }

    public override string GetResult()
    {
		return CalculateValidGames(_preparedInput).ToString();
	}

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
}