using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2024.Day_24;

public class Part_01 : Day_24
{
    public override int Part => 1;

	// Registry of each wire and it's corresponding value
	private Dictionary<string, bool?> _wireValueDictionary;

	// Use list instead of dictionary to ensure insert order stays consistent
	private List<KeyValuePair<(string wire1, string gate, string wire2), string>> _wireGateOutputDictionary;

	public override void PrepareData(List<string> input)
	{
		_wireValueDictionary = ParseWireValues(input);
		_wireGateOutputDictionary = ParseWireGateOutputs(input, _wireValueDictionary);
	}

	public override string ResultMessage => "Binary logic gate operation result to decimal";

	public override string GetResult()
	{
		return CalculateBinaryToIntResultOfGateOperations(_wireValueDictionary, _wireGateOutputDictionary).ToString();
	}

	private static double CalculateBinaryToIntResultOfGateOperations(Dictionary<string, bool?> wireValueDictionary, List<KeyValuePair<(string wire1, string gate, string wire2), string>> wireGateOutputDictionary)
	{

		for (var i = 0; i < wireGateOutputDictionary.Count; i++)
		{
			var logicOperation = wireGateOutputDictionary[i];

			var wireValue1 = wireValueDictionary[logicOperation.Key.wire1];
			var wireValue2 = wireValueDictionary[logicOperation.Key.wire2];

			// If a logic operation is encountered where one or both of the inputs don't have a value, add it to the 
			// end of the list to try again
			if (!wireValue1.HasValue || !wireValue2.HasValue)
			{
				wireGateOutputDictionary.Add(logicOperation);
				continue;
			}

			var gate = logicOperation.Key.gate;

			// At time of operation, wires used in operation should have value set. Nullability is only for  of output wires
			wireValueDictionary[logicOperation.Value] = PerformOperation(wireValue1.Value, gate, wireValue2.Value);
		}

		return GetRegistryValue(wireValueDictionary, 'z');
	}
}