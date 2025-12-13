namespace AdventOfCode.Puzzles.Year_2024.Day_23;

public class Part_02 : Day_23
{
    public override int Part => 2;

    public override string ResultMessage => "Password for largest connected set of computers";

	private Dictionary<string, List<string>> _connectedComputers;

	public override void PrepareData(List<string> input)
	{
		_connectedComputers = ParseConnectedComputers(input);
	}

	public override string GetResult()
	{
		return CalculatePasswordForLargestConnectedSetOfComputers(_connectedComputers);
	}

	private static string CalculatePasswordForLargestConnectedSetOfComputers(Dictionary<string, List<string>> connectedComputers)
	{
		var largestSet = new List<string>();

		// Order by the largest set first as the largest connected set may most likely come from it.
		connectedComputers = connectedComputers.OrderBy(x => x.Value.Count).ToDictionary();

		foreach (var computer in connectedComputers.Keys)
		{
			var currentComputerLargestSet = GetLargestInterConnectedSetForComputer(computer, [], connectedComputers);

			if (currentComputerLargestSet.Count > largestSet.Count)
				largestSet = currentComputerLargestSet;
		}

		return string.Join(',', largestSet.OrderBy(x => x));
	}

	private static List<string> GetLargestInterConnectedSetForComputer(string currentComputer, List<string> currentComputerSet, Dictionary<string, List<string>> connectedComputers)
	{
		if (currentComputerSet.Contains(currentComputer))
			return currentComputerSet;

		currentComputerSet.Add(currentComputer);

		var largestSet = new List<string>();

		// Iterate over all next computers where the next computer isn't already in the set
		foreach (var nextComputer in connectedComputers[currentComputer].Where(c => !currentComputerSet.Contains(c)))
		{
			// Make sure the next computer is connected to all currently established inter connected computers, if it isn't then skip it
			if (!currentComputerSet.All(c => connectedComputers[nextComputer].Contains(c)))
				continue;

			var nextComputerLargestSet = GetLargestInterConnectedSetForComputer(nextComputer, currentComputerSet, connectedComputers);
			if (nextComputerLargestSet.Count > largestSet.Count)
				largestSet = nextComputerLargestSet;
		}

		if (currentComputerSet.Count > largestSet.Count)
			largestSet = currentComputerSet;

		return largestSet;
	}
}