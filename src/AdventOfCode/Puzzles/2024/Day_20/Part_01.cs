using System;
using System.Collections.Generic;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode.Puzzles.Year_2024.Day_20;

public class Part_01 : Day_20
{
	public override int Part => 1;

	private List<List<char>> _raceMap;
	private List<List<bool>> _visited;
	private (int x, int y) _startPosition;
	private (int x, int y) _endPosition;
	private Dictionary<(int x, int y), int> _positionTimeDictionary;

	public override void PrepareData(List<string> input)
	{
		_raceMap = input.To2DCharList();
		_visited = InitializeVisitedMap(_raceMap);

		_startPosition = _raceMap.IndexOf('S');
		_endPosition = _raceMap.IndexOf('E');


		_positionTimeDictionary = new() { { _startPosition, 0} };
	}

	public override string ResultMessage => "Number of cheats that would save at least 100 picoseconds";

	public override string GetResult()
	{
		return CalculateSumOfSecondsSavedFrom2SkipsOver100Picoseconds(_raceMap, _visited, _startPosition, _endPosition, _positionTimeDictionary).ToString();
	}

	private long CalculateSumOfSecondsSavedFrom2SkipsOver100Picoseconds(List<List<char>> raceMap, List<List<bool>> visited, (int x, int y) startPosition, (int x, int y) endPosition, Dictionary<(int x, int y), int> positionTimeDictionary)
	{
        // Calculate time at each valid position on the track
        TraverseRaceMap(raceMap, visited, startPosition, endPosition, positionTimeDictionary);
		
		var skipsSavingMoreThan100Picoseconds = 0L;
		foreach (var positionTime in positionTimeDictionary)
		{
			skipsSavingMoreThan100Picoseconds += SimulateRaceWithSkip(raceMap, positionTimeDictionary, positionTime.Key);
		}

		return skipsSavingMoreThan100Picoseconds;
	}

	private long SimulateRaceWithSkip(List<List<char>> raceMap, Dictionary<(int x, int y), int> positionTimeDictionary, (int x, int y) currentPosition)
    {
		var timeSaved = 0L;

		var timeAtCurrentPosition = positionTimeDictionary[currentPosition];

		foreach (var direction in Directions)
		{
			var timeAtSkippedPosition = GetTimeAtNextPosition(raceMap, positionTimeDictionary, currentPosition, direction);

			if (timeAtSkippedPosition - timeAtCurrentPosition - 2 >= 100)
				timeSaved += 1;
		}

		return timeSaved;
    }

	private static long GetTimeAtNextPosition(List<List<char>> raceMap, Dictionary<(int x, int y), int> positionTimeDictionary, (int x, int y) currentPosition, (int x, int y) direction)
	{
		var skipPosition = (x: currentPosition.x + direction.x, y: currentPosition.y + direction.y);
		
		// Skips should only happen over walls
		if (raceMap.GetValueAtIndex(skipPosition) != '#')
			return 0;

		var nextPosition = (x: skipPosition.x + direction.x, y: skipPosition.y + direction.y);

		// The next position must be a valid race track position, and in bounds
		if (raceMap.IsInBounds(nextPosition) && raceMap.GetValueAtIndex(nextPosition) != '#')
			return positionTimeDictionary[nextPosition];

		return 0;
	}
}