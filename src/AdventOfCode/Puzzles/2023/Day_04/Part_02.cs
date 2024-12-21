using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2023.Day_04;

public class Part_02 : Day_04
{
	public override int Part => 2;
	
	private Dictionary<int, Card> _cardDictionary;
	private List<int> _cardsToScratch;
	private int _lastCardId;

	private record Card(int Id, IList<int> ReferenceValues, IList<int> CardValues);

    public override void PrepareData(List<string> input)
    {
		// Reference dictionary of all available cards
		_cardDictionary = ConstructCardDictionary(input);
		// Maintained list of cards that can be scratched, new cards are added to the end of the list
		_cardsToScratch = _cardDictionary.Keys.ToList();
		// Id of last card id, used to ensure no card ids greater than it are added
		_lastCardId = _cardsToScratch.OrderBy(x => x).Last();
    }
	
	private Dictionary<int, Card> ConstructCardDictionary(IEnumerable<string> input)
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

	public override string ResultMessage => "Sum of total cards scratched";

    public override string GetResult()
    {
		return CalculateNumberOfInstancesOfWinningCards(_cardDictionary, _cardsToScratch, _lastCardId).ToString();
    }

	private int CalculateNumberOfInstancesOfWinningCards(Dictionary<int, Card> cardDictionary, List<int> cardsToScratch, int lastCardId)
	{	
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
}
