using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common.Models;

namespace AdventOfCode.Common.Utility.Debugging;

public static class PrintHelper
{
    /// <summary>
    /// Print a formatted 2D map from the given 2D list.<br/>
    /// Includes highlighting specific indexes or values, adding spaces between each item, and specifying how the given values can be highlighted
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="map"></param>
    /// <param name="highlightValues"></param>
    /// <param name="highlightIndeces"></param>
    /// <param name="highlighter"></param>
    /// <param name="padItems"></param>
	public static void PrintMap<T>(List<List<T>> map, T[] highlightValues = null, Point2D[] highlightIndeces = null, char highlighter = '_', bool padItems = true)
    {
        highlightValues ??= [];
        highlightIndeces ??= [];

        foreach (var (row, yIndex) in map.Select((r, y) => (r, y)))
        {
            foreach (var (item, xIndex) in row.Select((i, x) => (i, x)))
            {
                if (highlightIndeces.Contains(new(xIndex, yIndex)) || highlightValues.Contains(item))
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

    /// <summary>
    /// Print a formatted 2D map from the given 2D list and apply a map over top to update specific indexex.<br/>
    /// Includes specifying the value used to display a masked item and a list of values to ignore when masking.<br/>
    /// Example:<br/>
    /// 1 = true, 0 = false <br/>
    /// <code>
    /// # # # # # #     0 0 0 0 0 0     # # # # # #
    /// # - # # # #     0 1 0 0 0 0     # O # # # #
    /// # - - - - #  +  0 1 1 1 1 0  =  # O O O O #
    /// # # # - # #     0 0 0 1 0 0     # # # O # #
    /// # - - - # #     0 0 0 1 0 0     # - - O # #
    /// # - # # # #     0 0 0 0 0 0     # - # # # #
    /// # # # # # #     0 0 0 0 0 0     # # # # # #
    /// </code>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="map"></param>
    /// <param name="mask"></param>
    /// <param name="maskValue"></param>
    /// <param name="ignore"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void PrintMaskedMap<T>(List<List<T>> map, List<List<bool>> mask, char maskValue = 'O', List<T> ignore = null)
    {
        if (map.Count != mask.Count || map[0].Count != mask[0].Count)
            throw new ArgumentException($"The size of {nameof(mask)} must be the same size as {nameof(map)}");

        foreach (var (row, yIndex) in map.Select((r, y) => (r, y)))
        {
            foreach (var (item, xIndex) in row.Select((i, x) => (i, x)))
            {
                var doMaskIndex = mask[yIndex][xIndex];
                if (doMaskIndex && ignore?.Contains(item) != true)
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