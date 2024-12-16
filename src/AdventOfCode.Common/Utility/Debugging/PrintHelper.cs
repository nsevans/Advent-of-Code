using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Common.Utility.Debugging;

public static class PrintHelper
{
	public static void PrintMap<T>(List<List<T>> map, List<T> highlightValues = null, List<(int x, int y)> highlightIndexes = null, char highlighter = '_', bool padItems = true)
	{
		highlightValues ??= [];
		highlightIndexes ??= [];

		foreach (var (row, yIndex) in map.Select((r, y) => (r, y)))
		{
			foreach (var (item, xIndex) in row.Select((i, x) => (i, x)))
			{
				if (highlightIndexes.Contains((xIndex, yIndex)) || highlightValues.Contains(item))
				{
					if (padItems)
						Console.Write($" {highlighter}{item}{highlighter} ");
					else
						Console.Write($"{highlighter}{item}{highlighter}");
				}
				else
				{
					if (padItems)
						Console.Write($"  {item}  ");
					else
						Console.Write($"{item}");
				}
			}
			Console.WriteLine();
		}
	}

	public static void PrintMaskedMap<T>(List<List<T>> map, List<List<bool>> mask, char maskValue = 'O', List<T> ignore = null)
	{
		if (map.Count != mask.Count || map[0].Count != mask[0].Count)
			throw new ArgumentException($"The size of {nameof(mask)} must be the same size as {nameof(map)}");

		foreach (var (row, yIndex) in map.Select((r, y) => (r, y)))
		{
			foreach (var (item, xIndex) in row.Select((i, x) => (i, x)))
			{
				var doMaskIndex = mask[yIndex][xIndex];
				if (doMaskIndex && !ignore.Contains(item))
				{
					Console.Write($" {maskValue} ");
				}
				else
				{
					Console.Write($" {item} ");
				}
			}
			Console.WriteLine();
		}
	}
}