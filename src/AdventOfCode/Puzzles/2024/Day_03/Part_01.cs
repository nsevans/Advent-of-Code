namespace AdventOfCode.Puzzles.Year_2024.Day_03;

public class Part_01 : Day_03
{
	public override int Part => 1;

    public override string ResultMessage => "Sum of products of mul commands";

	private List<string> _preparedInput;

    public override void PrepareData(List<string> input)
    {
		_preparedInput = input;
    }

	public override string GetResult()
    {
		return CalculateSumOfProducts(_preparedInput).ToString();
    }

	private static long CalculateSumOfProducts(IEnumerable<string> input)
	{
		var sum = 0L;
		// Pattern to find occurences of "mul(xxx,xxx)"
		var pattern = @"mul\((\d{1,3},\d{1,3})\)";

		foreach (var line in input)
		{
			var matches = Regex.Matches(line, pattern);

			foreach (var match in matches.Select(x => x.Value))
				sum += GetProductOfValuesFromMatch(match);
		}

		return sum;
	}
}
