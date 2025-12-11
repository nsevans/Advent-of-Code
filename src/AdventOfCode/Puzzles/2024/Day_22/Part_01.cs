using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2024.Day_22;

public class Part_01 : Day_22
{
    public override int Part => 1;

	public override string ResultMessage => "Sum of the 2000th secret number generated for each buyer";

	private List<long> _secretNumbers;

	public override void PrepareData(List<string> input)
	{
		_secretNumbers = ParseSecretNumbers(input);
	}

	public override string GetResult()
	{
		return CalculateSumOf2000thSecretNumber(_secretNumbers).ToString();
	}

	private static long CalculateSumOf2000thSecretNumber(List<long> secretNumbers)
	{
		var sum = 0L;
		foreach (var secret in secretNumbers)
		{
			var result = SimulateSecretGeneration(secret, 2000);
			sum += result;
		}
		return sum;
	}

	private static long SimulateSecretGeneration(long secret, int cycles)
	{
		var currentSecret = secret;
		for (var i = 0; i < cycles; i++)
		{
			currentSecret = GenerateNextSecret(currentSecret);
		}
		return currentSecret;
	}
}