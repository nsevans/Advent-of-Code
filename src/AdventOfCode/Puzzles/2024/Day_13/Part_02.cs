namespace AdventOfCode.Puzzles.Year_2024.Day_13;

public class Part_02 : Day_13
{
	public override int Part => 2;

	public override string ResultMessage => "Sum of fewest tokens used to win all possible games with a coordinate offset of 10,000,000,000,000";

	private List<Game> _games;

	public override void PrepareData(List<string> input)
	{
		_games = ParseGames(input);
	}

	public override string GetResult()
	{
		return CalculateSumOfTokensUsedForValidClawMachinesWithOffset(_games).ToString();
	}

	private static double CalculateSumOfTokensUsedForValidClawMachinesWithOffset(List<Game> games)
	{
		var sum = 0d;

		foreach (var game in games)
		{
			sum += CalculateButtonPressesWithOffset(game, 10_000_000_000_000ul);
		}

		return sum;
	}
}