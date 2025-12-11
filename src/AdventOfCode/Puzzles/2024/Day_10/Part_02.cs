using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode.Puzzles.Year_2024.Day_10;

public class Part_02 : Day_10
{
	public override int Part => 2;

	public override string ResultMessage => "Sum of all trail heads to trail ends";

	private List<List<int>> _trailMap;
	private List<(int x, int y)> _startLocations;
	private List<(int x, int y)> _endLocations;

	public override void PrepareData(List<string> input)
	{
		_trailMap = input.To2DIntList();
		_startLocations = ParseLocations(_trailMap, 0);
		_endLocations = ParseLocations(_trailMap, 9);
	}

	public override string GetResult()
	{
		return CalculateSumOfTrailHeadScores(_trailMap, _startLocations, _endLocations).ToString();
	}

	private static long CalculateSumOfTrailHeadScores(List<List<int>> trailMap, List<(int x, int y)> startLocations, List<(int x, int y)> endLocations)
	{
		var endsFound = new List<(int x, int y)>();

		foreach (var startLocation in startLocations)
		{
			var potentialEndsFound = TryFindEndOfTrailRecursiveDFS(trailMap, startLocation);
			var trailEndsFound = potentialEndsFound
				.Where(x => endLocations.Contains(x));

			endsFound.AddRange(trailEndsFound);
		}

		return endsFound.Count;
	}
}