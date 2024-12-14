using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Common.Extensions;

public static class ListExtensions
{
	/// <summary>
	/// Convert a list of values to a 2D list of chars using the ToString() method
	/// </summary>
	/// <param name="list"></param>
	/// <returns></returns>
	public static List<List<char>> To2DCharList<T>(this IList<T> list)
		=> list.Select(x => x.ToString().ToCharArray().ToList()).ToList();

	/// <summary>
	/// Check if the given coordinates are within the bounds of the given 2 dimensional list.
	/// </summary>
	/// <param name="map"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <returns></returns>
	public static bool IsInBounds(this IList<IList<object>> map, int x, int y)
	{
		if (x < 0 || y < 0)
			return false;

		if (y >= map.Count || x >= map[y].Count)
			return false;

		return true;
	}

	/// <summary>
	/// Swap the values at the given index
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="index1"></param>
	/// <param name="index2"></param>
	public static void SwapValues<T>(this IList<T> list, int index1, int index2)
	{
		(list[index1], list[index2]) = (list[index2], list[index1]);
	}
}