using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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
	/// <typeparam name="T"></typeparam>
	/// <param name="map"></param>
	/// <param name="index"></param>
	/// <returns></returns>
	public static bool IsInBounds<T>(this List<List<T>> map, (int x, int y) index) => map.IsInBounds(index.x, index.y);

	/// <summary>
	/// Check if the given coordinates are within the bounds of the given 2 dimensional list.
	/// </summary>
	/// <param name="map"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <returns></returns>
	public static bool IsInBounds<T>(this List<List<T>> map, int x, int y)
	{
		if (x < 0 || y < 0)
			return false;

		if (y >= map.Count || x >= map[y].Count)
			return false;

		return true;
	}

	/// <summary>
	/// Check if the given coordinates are safe (i.e.: in bounds of the 2D list and not an unsafe value)
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="map"></param>
	/// <param name="index"></param>
	/// <param name="unsafes"></param>
	/// <returns></returns>
	public static bool IsSafe<T>(this List<List<T>> map, (int x, int y) index, List<T> unsafes = null) => map.IsSafe(index.x, index.y, unsafes);

	/// <summary>
	/// Check if the given coordinates are safe (i.e.: in bounds of the 2D list and not an unsafe value)
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="map"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="unsafes"></param>
	/// <returns></returns>
	public static bool IsSafe<T>(this List<List<T>> map, int x, int y, List<T> unsafes = null)
	{
		if (!map.IsInBounds(x, y))
			return false;

		if (unsafes.Contains(map[y][x]))
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

	/// <summary>
	/// Searches the given value and returns the index of the first occurrence within the 2 dimensional list.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="value"></param>
	/// <returns>Returns the index of the first occurrence of the item if found, otherwise (-1, -1)</returns>
	public static (int x, int y) IndexOf<T>(this List<List<T>> list, T value)
	{
		if (list.Where(x => x.Contains(value)).Count() == 0)
			return (-1, -1);

		var row = list.Where(x => x.Contains(value)).First();
		var y = list.IndexOf(row);
		var x = row.IndexOf(value);

		return (x, y);
	}

	/// <summary>
	/// Searches the given value and returns all indexes within the 2 dimensional list.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="value"></param>
	/// <returns>Returns a list of indexes of the item if found, otherwise an empt list</returns>
	public static List<(int x, int y)> IndexesOf<T>(this List<List<T>> list, T value)
	{
		var indexes = new List<(int x, int y)>();

		foreach (var (row, y) in list.Select((r, i) => (r, i)))
		{
			foreach (var (item, x) in row.Select((i, j) => (i, j)))
			{
				if (item.Equals(value))
					indexes.Add((x, y));
			}
		}

		return indexes;
	}

	/// <summary>
	/// Get a value from a specified index of a 2D list at a given position, as long as the position is valid
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="position"></param>
	/// <returns>The value at the given index, default if the index is invalid</returns>
	public static T GetValueAtIndex<T>(this List<List<T>> list, (int x, int y) position) => list.GetValueAtIndex(position.x, position.y);

	/// <summary>
	/// Get a value from a specified index of a 2D list at a given position, as long as the position is valid
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <returns>The value at the given index, default if the index is invalid</returns>
	public static T GetValueAtIndex<T>(this List<List<T>> list, int x, int y)
	{
		if (!list.IsInBounds(x, y))
			return default;

		return list[y][x]; 
	}

	/// <summary>
	/// Set a given value in a 2D list at a given position, as long as the position is valid 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="position"></param>
	/// <param name="value"></param>
	/// <returns>True if set successfully, false otherwise</returns>
	public static bool SetValueAtIndex<T>(this List<List<T>> list, (int x, int y) position, T value) => list.SetValueAtIndex(position.x, position.y, value);

	/// <summary>
	/// Set a given value in a 2D list at a given position, as long as the position is valid 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="value"></param>
	/// <returns>True if set successfully, false otherwise</returns>
	public static bool SetValueAtIndex<T>(this List<List<T>> list, int x, int y, T value)
	{
		if (!list.IsInBounds(x, y))
			return false;

		list[y][x] = value;
		return true;
	}
}