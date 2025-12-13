using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common.Models;
using AdventOfCode.Services.Solve.BaseSolvers;

namespace AdventOfCode.Puzzles.Year_2024.Day_21;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2024/day/21
///
/// Example/Test Input:
///		029A
///		980A
///		179A
///		456A
///		379A
/// </summary>
public abstract class Day_21 : BaseCSharpSolver
{
	public override string Title => "Keypad Conundrum";
	public override int Day => 21;
	public override int Year => 2024;

	/// <summary>
	/// Max number of layers to recurse
	/// In terms of puzzle: root direction pad (1) + number robots controlling direction pads + robot controlling num pad (1)
	/// </summary>
	protected abstract int MaxLayers { get; }

	/// <summary>
	///	+---+---+---+
	///	| 7 | 8 | 9 |
	///	+---+---+---+
	///	| 4 | 5 | 6 |
	///	+---+---+---+
	///	| 1 | 2 | 3 |
	///	+---+---+---+
	///	    | 0 | A |
	///	    +---+---+
	/// </summary>
	protected Dictionary<char, Point2D> NumpadDictionary = new()
	{
		{ 'A', new(2, 3) },
		{ '0', new(1, 3) },
		{ '1', new(0, 2) },
		{ '2', new(1, 2) },
		{ '3', new(2, 2) },
		{ '4', new(0, 1) },
		{ '5', new(1, 1) },
		{ '6', new(2, 1) },
		{ '7', new(0, 0) },
		{ '8', new(1, 0) },
		{ '9', new(2, 0) }
	};

	/// <summary>
	///  	+---+---+
	///	    | ^ | A |
	///	+---+---+---+
	///	| < | v | > |
	///	+---+---+---+
	/// </summary>
	protected Dictionary<char, Point2D> DirectionPadDictionary = new()
	{
		{ '^', new (1, 0) },
		{ 'A', new (2, 0) },
		{ '<', new (0, 1) },
		{ 'v', new (1, 1) },
		{ '>', new (2, 1) }
	};

	protected static bool IsInBoundsNumpad(Point2D position)
	{
		if (position.X == 0 && position.Y == 3)
			return false;

		return position.X >= 0 && position.Y >= 0 && position.X < 3 && position.Y < 4;
	}

	protected static bool IsInBoundsDirectionPad(Point2D position)
	{
		if (position.X == 0 && position.Y == 0)
			return false;

		return position.X >= 0 && position.Y >= 0 && position.X < 3 && position.Y < 2;
	}

	protected double CalculateSumOfKeyPresses(List<List<char>> padInput)
	{
		// Cache of shortest path between 2 buttons at a specific layer
		var cache = new Dictionary<(char start, char end, int layer), double>();

		var sum = 0d;
		foreach (var inputLine in padInput)
		{
			var currentLength = 0d;
			var currentButton = 'A';

			foreach (var targetButton in inputLine)
			{
				currentLength += CalculateShortestLengthToNextButton(currentButton, targetButton, 0, cache);
				currentButton = targetButton;
			}

			sum += double.Parse(new string(inputLine[..^1].ToArray())) * currentLength;
		}

		return sum;
	}

    protected double CalculateShortestLengthToNextButton(char current, char target, int layer, Dictionary<(char start, char end, int layer), double> cache)
    {
		if (layer == MaxLayers - 1)
			return 1;

		var cacheKey = (start: current, end: target, layer: layer);
		if (cache.TryGetValue(cacheKey, out var value))
			return value;

		var shortestLength = -1d;

		// If bottom layer, than use the numpad dictionary, otherweise use the direction pad dictionary for calculations
		var paths = layer == 0
			? GetPaths(NumpadDictionary[current], NumpadDictionary[target], IsInBoundsNumpad)
			: GetPaths(DirectionPadDictionary[current], DirectionPadDictionary[target], IsInBoundsDirectionPad);

		foreach (var path in paths)
		{
			var currentLength = 0d;
			var currentButton = 'A';
			foreach (var nextButton in path)
			{
				currentLength += CalculateShortestLengthToNextButton(currentButton, nextButton, layer + 1, cache);
				currentButton = nextButton;
			}

			if (shortestLength == -1 || currentLength < shortestLength)
				shortestLength = currentLength;
		}

		if (!cache.TryAdd(cacheKey, shortestLength))
			cache[cacheKey] = shortestLength;

		return shortestLength;
    }

	protected static List<string> GetPaths(Point2D start, Point2D end, Func<Point2D, bool> isInBounds)
	{
		var paths = GetPathsReversed(start, end, isInBounds);

		for (var i = 0; i < paths.Count; i++)
		{
			paths[i] = new string(paths[i].Reverse().ToArray());
		}

		return paths;
	}

	protected static List<string> GetPathsReversed(Point2D start, Point2D end, Func<Point2D, bool> isInBounds)
	{
		if (!isInBounds(start))
			return [];

		if (start.Equals(end))
			return [ "A" ];

		var possiblePaths = new List<string>();

		if (end.Y < start.Y)
		{
			foreach (var path in GetPathsReversed(new(start.X, start.Y - 1), end, isInBounds))
				possiblePaths.Add(path + '^');
		}

		if (end.Y > start.Y)
		{
			foreach (var path in GetPathsReversed(new(start.X, start.Y + 1), end, isInBounds))
				possiblePaths.Add(path + 'v');
		}

		if (end.X < start.X)
		{
			foreach (var path in GetPathsReversed(new(start.X - 1, start.Y), end, isInBounds))
				possiblePaths.Add(path + '<');
		}

		if (end.X > start.X)
		{
			foreach (var path in GetPathsReversed(new(start.X + 1, start.Y), end, isInBounds))
				possiblePaths.Add(path + '>');
		}

		return possiblePaths;
	}
}