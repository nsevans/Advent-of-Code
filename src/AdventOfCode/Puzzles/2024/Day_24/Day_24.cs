using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode.Puzzles.Year_2024.Day_24;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
/// 
/// Link to Day https://adventofcode.com/2024/day/24
/// 
/// Example/Test Input: 
///		x00: 1
///		x01: 1
///		x02: 1
///		y00: 0
///		y01: 1
///		y02: 0
///		
///		x00 AND y00 -> z00
///		x01 XOR y01 -> z01
///		x02 OR y02 -> z02
/// </summary>
public abstract class Day_24 : BaseSolver
{
	public override string Title => "Crossed Wires";
	public override int Day => 24;
	public override int Year => 2024;

	protected Dictionary<string, bool?> ParseWireValues(List<string> input)
	{
		var dictionary = new Dictionary<string, bool?>();

		foreach (var line in input)
		{
			if (string.IsNullOrEmpty(line))
				break;

			var split = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
			dictionary.Add(split[0], split[1] == "1");
		}

		return dictionary;
	}

	// Pass in the wire value dictionary, as wires are also defined in the output of gates
	protected List<KeyValuePair<(string wire1, string gate, string wire2), string>> ParseWireGateOutputs(List<string> input, Dictionary<string, bool?> wireValueDictionary)
	{
		var dictionary = new List<KeyValuePair<(string wire1, string gate, string wire2), string>>();

		foreach (var line in input)
		{
			// Skip all initial wire definitions and the empty line
			if (line.Contains(':') || string.IsNullOrEmpty(line))
				continue;

			var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
			
			var key = (split[0], split[1], split[2]);
			var value = split[4];

			dictionary.Add(new (key, value));

			wireValueDictionary.CreateOrUpdate(split[4], null);
		}

		// Ensure any operations that use the initial inputs (wires starting with x or y) are first so fewer iterations are needed
		dictionary = dictionary
			.OrderByDescending(t => new List<char> {'x', 'y'}.Any(x => t.Key.wire1.StartsWith(x)))
			.OrderByDescending(t => new List<char> {'x', 'y'}.Any(x => t.Key.wire2.StartsWith(x)))
			.ToList();

		return dictionary;
	}

	protected bool PerformOperation(bool wireValue1, string gate, bool wireValue2)
	{
		if (gate == "AND")
			return wireValue1 && wireValue2;

		if (gate == "OR")
			return wireValue1 || wireValue2;

		if (gate == "XOR")
			return wireValue1 != wireValue2;

		// Should not reach here
		throw new InvalidOperationException($"Invalid logic gate '{gate}'");
	}

	protected double GetRegistryValue(Dictionary<string, bool?> registry, char registryIdentifier)
	{
		var registryValues = registry
			.Where(x => x.Key.StartsWith(registryIdentifier))
			.OrderByDescending(x => x.Key)
			.Select(x => x.Value == true ? 1 : 0);

		var result = 0d;
		foreach (var value in registryValues)
		{
			result *= 2;
			result += value;
		}

		return result;
	}
}