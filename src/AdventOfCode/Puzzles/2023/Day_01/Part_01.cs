namespace AdventOfCode.Puzzles.Year_2023.Day_01;

public class Part_01 : Day_01
{
    public override int Part => 1;

	public override string ResultMessage => "Sum of combined first and last digit of each line";

    private List<string> _preparedInput;

    public override void PrepareData(List<string> input)
    {
		_preparedInput = input;
    }

    public override string GetResult()
    {
		return CalculateSumOfCalibrationDigits(_preparedInput).ToString();
    }

	private static int CalculateSumOfCalibrationDigits(IEnumerable<string> input)
	{
		var result = 0;
		foreach (var line in input)
		{
			result += ScanForDigits(line);
		}

		return result;
	}

	private static int ScanForDigits(string line)
	{
		var first = 0;
		// Iterate forward through the string until a number is found
		for (int i = 0; i < line.Length; i++)
		{
			// If it's a number, set it and continue
			if (int.TryParse(line[i].ToString(), out var firstItem))
			{
				first = firstItem;
				break;
			}
		}

		var last = 0;
		// Iterate backward through the string until a number is found
		for (int i = line.Length - 1; i >= 0; i--)
		{
			// If it's a number, set it and continue
			if (int.TryParse(line[i].ToString(), out var lastItem))
			{
				last = lastItem;
				break;
			}
		}

		return int.Parse($"{first}{last}");
	}
}