using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_05;

public class Part_02 : Day_05
{
	public override int Part => 2;

	public override string ResultMessage => "Sum of middle pages of re ordered updates";

	private Dictionary<int, List<int>> _pageOrderingRules;
	private List<List<int>> _pagesToProduce;

	public override void PrepareData(List<string> input)
	{
		_pageOrderingRules = ParsePageOrderingRules(input);
		_pagesToProduce = ParsePagesToProduce(input);
	}

	public override string GetResult()
	{
		return CalculateSumOfInvalidMiddlePageNumbers(_pageOrderingRules, _pagesToProduce).ToString();
	}

	private static int CalculateSumOfInvalidMiddlePageNumbers(Dictionary<int, List<int>> pageOrderingRules, List<List<int>> pagesToProduce)
	{
		var sum = 0;

		foreach (var update in pagesToProduce)
		{
			// Copy of the current update so it can be safely modified
			var currentUpdateCopy = new List<int>(update);
			var newUpdate = new List<int>();

			for (var i = 0; i < currentUpdateCopy.Count; i++)
			{
				var page = currentUpdateCopy[i];
				var pageOrderingRule = pageOrderingRules[page];

				// Scan preceding pages to see if all are valid within the current pages ordering rules
				if(!currentUpdateCopy[(i + 1)..].All(p => pageOrderingRule.Contains(p)))
					// If it's not valid add it to the end of the list so it can be re evaluated / evaluated
					// with the following pages
					currentUpdateCopy.Add(page);
				else
					newUpdate.Add(page);
			}

			if (!update.SequenceEqual(newUpdate))
				sum += newUpdate[newUpdate.Count / 2];
		}

		return sum;
	}
}