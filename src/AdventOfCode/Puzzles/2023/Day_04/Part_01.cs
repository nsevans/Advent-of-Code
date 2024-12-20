using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2023.Day_04;

public class Part_01 : Day_04
{
	public override int Part => 1;

	private List<string> _preparedInput;
    
	public override void PrepareData(List<string> input)
    {
		_preparedInput = input;
    }

    public override void Solve()
    {
		var result = CalculatePointsForMatchingNumbers(_preparedInput);
		Console.WriteLine($"Sum of points for matching number: {result}");
    }

	private int CalculatePointsForMatchingNumbers(List<string> input)
	{
		var sum = 0;

		foreach(var line in input)
		{
			var split = line.Split('|');
			// Get left side of card, removing first two entries "Card" and "##:"
			var references = split[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[2..].Select(x => int.Parse(x));
			var values = split[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x));
			
			var cardPoints = 0;
			foreach(var value in values)
			{
				if (references.Contains(value))
				{
					// Set to 1 on first set, otherwise multiple result by 2
					cardPoints = cardPoints == 0 ? 1 : cardPoints * 2;
				}
			}
			sum += cardPoints;
		}

		return sum;
	}
}