namespace AdventOfCode.Puzzles.Year_2024.Day_13;

public class Part_01 : Day_13
{
	public override int Part => 1;

	public override string ResultMessage => "Sum of fewest tokens used to win all possible games";

	private List<Game> _games;

	public override void PrepareData(List<string> input)
	{
		_games = ParseGames(input);
	}

	public override string GetResult()
	{
		return CalculateSumOfTokensUsedForClawMachines(_games).ToString();
	}

	private static double CalculateSumOfTokensUsedForClawMachines(List<Game> games)
	{
		var sum = 0d;

		foreach (var game in games)
		{
			sum += CalculateButtonPresses(game);
		}

		return sum;
	}
}