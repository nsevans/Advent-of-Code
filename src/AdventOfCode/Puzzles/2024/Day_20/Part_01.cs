using System;
using System.Collections.Generic;
using AdventOfCode.Common.Constants;
using AdventOfCode.Common.Extensions;
using AdventOfCode.Common.Models;

namespace AdventOfCode.Puzzles.Year_2024.Day_20;

public class Part_01 : Day_20
{
	public override int Part => 1;

    public override string ResultMessage => "100 picosecond saving cheats when skipping for no more than 2 picoseconds";

	private List<List<char>> _raceMap;
	private Dictionary<Point2D, int> _positionTimeDictionary;

	public override void PrepareData(List<string> input)
	{
		_raceMap = input.To2DCharList();
        _positionTimeDictionary = new() { { new Point2D(_raceMap.IndexOf('S')), 0 } };

        // Calculate time at each valid position on the track
        CalculateRaceMapTraversalTimes(_raceMap, _positionTimeDictionary);
	}

	public override string GetResult()
	{
		return CalculateSumOfSkipsSavingOver100Picoseconds(_raceMap, _positionTimeDictionary).ToString();
	}

	private static long CalculateSumOfSkipsSavingOver100Picoseconds(List<List<char>> raceMap, Dictionary<Point2D, int> positionTimeDictionary)
	{

		var skipsSavingMoreThan100Picoseconds = 0L;
		foreach (var positionTime in positionTimeDictionary)
		{
			skipsSavingMoreThan100Picoseconds += SimulateRaceWithWallSkip(raceMap, positionTimeDictionary, positionTime.Key);
		}

		return skipsSavingMoreThan100Picoseconds;
	}

	private static long SimulateRaceWithWallSkip(List<List<char>> raceMap, Dictionary<Point2D, int> positionTimeDictionary, Point2D currentPosition)
    {
		var timesSaved = 0L;

		var timeAtCurrentPosition = positionTimeDictionary[currentPosition];

		foreach (var direction in Directions.Cardinal)
		{
			var timeAtSkippedPosition = GetTimeAtNextPosition(raceMap, positionTimeDictionary, currentPosition, direction);

			if (timeAtSkippedPosition - timeAtCurrentPosition - 2 >= 100)
				timesSaved += 1;
		}

		return timesSaved;
    }

	private static long GetTimeAtNextPosition(List<List<char>> raceMap, Dictionary<Point2D, int> positionTimeDictionary, Point2D currentPosition, (int x, int y) direction)
	{
		var skipPosition = new Point2D(currentPosition.X + direction.x, currentPosition.Y + direction.y);

		// Skips should only happen over walls
		if (raceMap.GetValueAtIndex(skipPosition) != '#')
			return 0;

		var nextPosition = new Point2D(skipPosition.X + direction.x, skipPosition.Y + direction.y);

		// The next position must be a valid race track position, and in bounds
		if (raceMap.IsInBounds(nextPosition) && raceMap.GetValueAtIndex(nextPosition) != '#')
			return positionTimeDictionary[nextPosition];

		return 0;
	}
}