using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Puzzle_04;

/// <summary>
/// Link: https://adventofcode.com/2023/day/4
/// Input Format: 
///		Card   1: 82 41 56 54 18 62 29 55 34 20 | 37 14 10 80 58 11 65 96 90  8 59 32 53 21 98 83 17  9 87 25 71 77 70 73 24
/// </summary>

public class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("### 2023 - Puzzle 04 ###\n");
		var input = GetInput("./input.txt");

		var resultPart1 = CalculatePointsForMatchingNumbers(input);
		Console.WriteLine($"[PART 1] Sum of points for matching number: {resultPart1}");

		var resultPart2 = CalculateNumberOfInstancesOfWinningCards(input.ToList());
		Console.WriteLine($"[PART 2] Sum of points for matching number: {resultPart2}");
	}

	public static IEnumerable<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName);
	}

	#region PART 1
	private static int CalculatePointsForMatchingNumbers(IEnumerable<string> input)
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
	#endregion

	#region PART 2
	private record Card(int Id, IList<int> ReferenceValues, IList<int> CardValues);
	
	private static Dictionary<int, Card> ConstructCardDictionary(IEnumerable<string> input)
	{
		var dictionary = new Dictionary<int, Card>();
		foreach(var line in input)
		{
			var split = line.Split('|');
			// Parse card id from line, removing trailing ':'
			var cardId = int.Parse(split[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1][..^1]);
			// Get left side of card, removing first two entries "Card" and "##:"
			var references = split[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[2..]
				.Select(x => int.Parse(x))
				.ToList();
			var values = split[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)
				.Select(x => int.Parse(x))
				.ToList();

			var card = new Card(cardId, references, values);
			dictionary.Add(cardId, card);
		}

		return dictionary;
	}

	public static int CalculateNumberOfInstancesOfWinningCards(IEnumerable<string> input)
	{
		// Reference dictionary of all available cards
		var cardDictionary = ConstructCardDictionary(input);
		// Maintained list of cards that can be scratched, new cards are added to the end of the list
		var cardsToScratch = cardDictionary.Keys.ToList();
		// Id of last card id, used to ensure no card ids greater than it are added
		var lastCardId = cardsToScratch.OrderBy(x => x).Last();
		var numberOfCardsScratched = 0;

		for (int i = 0; i < cardsToScratch.Count; i++)
		{
			numberOfCardsScratched++;
			// Get current card from dictionary
			var scratchedCard = cardDictionary[cardsToScratch[i]];

			var numberOfMatches = 0;
			foreach(var value in scratchedCard.CardValues)
			{
				if (scratchedCard.ReferenceValues.Contains(value))
				{
					numberOfMatches++;
					var cardWonId = scratchedCard.Id + numberOfMatches;
					// Ensure card id being added isn't greater than the largest available card id
					if (cardWonId <= lastCardId)
					{
						cardsToScratch.Add(cardWonId);
					}
				}
			}
		}
		return numberOfCardsScratched;
	}
	#endregion
}