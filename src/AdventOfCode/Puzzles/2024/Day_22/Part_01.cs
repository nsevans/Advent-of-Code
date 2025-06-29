using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2024.Day_22;

public class Part_01 : Day_22
{
    public override int Part => 1;

	private List<long> _secretNumbers;

	public override void PrepareData(List<string> input)
	{
		_secretNumbers = ParseSecretNumbers(input);
	}

	public override string ResultMessage => "Sum of the 2000th secret number generated for each buyer";

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

	private static long GenerateNextSecret(long secret)
	{
		var step1 = ((secret * 64) ^ secret) % 16777216;
		var step2 = ((step1 / 32) ^ step1) % 16777216;
		var step3 = ((step2 * 2048) ^ step2) % 16777216;
		return step3;
	}
}