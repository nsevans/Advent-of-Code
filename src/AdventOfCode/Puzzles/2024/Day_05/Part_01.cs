using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_05;

public class Part_01 : Day_05
{
	public override int Part => 1;

	public override string ResultMessage => "Sum of middle pages of valid ordered updates";

	private Dictionary<int, List<int>> _pageOrderingRules;
	private List<List<int>> _pagesToProduce;

	public override void PrepareData(List<string> input)
	{
		_pageOrderingRules = ParsePageOrderingRules(input);
		_pagesToProduce = ParsePagesToProduce(input);
	}

	public override string GetResult()
	{
		return CalculateSumOfValidMiddlePageNumbers(_pageOrderingRules, _pagesToProduce).ToString();
	}

	private static int CalculateSumOfValidMiddlePageNumbers(Dictionary<int, List<int>> pageOrderingRules, List<List<int>> pagesToProduce)
	{
		var sum = 0;

		foreach (var update in pagesToProduce)
		{
			var isValid = true;
			foreach (var (page, index) in update.Select((p, i) => (p, i)))
			{
				// Scan preceding pages to see if all are valid within the current pages ordering rules
				var nextPages = update[(index + 1)..];
				if (!nextPages.All(n => pageOrderingRules[page].Contains(n)))
				{
					isValid = false;
					break;
				}
			}

			if (isValid)
				sum += update[update.Count / 2];
		}

		return sum;
	}
}