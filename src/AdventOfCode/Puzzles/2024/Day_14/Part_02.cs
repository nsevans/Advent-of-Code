using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common.Utility;

namespace AdventOfCode.Puzzles.Year_2024.Day_14;

public class Part_02 : Day_14
{
	public override int Part => 2;

	private List<RobotData> _robotData;

	public override void PrepareData(List<string> input)
	{
		_robotData = ParseRobotData(input);
	}

	public override string ResultMessage => "Lowest second where robots form christman tree";

	public override string GetResult()
	{
		return CalculateLowestSecondWhereRobotsFormChristmasTree(_robotData).ToString();
	}

	private int CalculateLowestSecondWhereRobotsFormChristmasTree(List<RobotData> robotData)
	{
		// Use chinese remainder theorem to calculate the time in which the robots are neareast to each other
		// aka: the x and y variance is lowest

		var secondsToSimulate = Math.Max(MapHeight, MapWidth);

		var minXVariance = 1000d;
		var minXVarianceTime = 0d;

		var minYVariance = 1000d;
		var minYVarianceTime = 0d;

		for (int t = 0; t < secondsToSimulate; t++)
		{
			var simulatedPositions = SimulateRobotsToSecond(robotData, t);

			var xVariance = MathHelper.Variance(simulatedPositions.Select(d => d.X).ToList());
			var yVariance = MathHelper.Variance(simulatedPositions.Select(d => d.Y).ToList());

			if (xVariance < minXVariance)
			{
				minXVariance = xVariance;
				minXVarianceTime = t;
			}
			if (yVariance < minYVariance)
			{
				minYVariance = yVariance;
				minYVarianceTime = t;
			}
		}

		var calculatedMinTime = minXVarianceTime + (Math.Pow(MathHelper.ModInverse(MapWidth, MapHeight), 1) % MapHeight * (minYVarianceTime - minXVarianceTime) % MapHeight * MapWidth);
		return (int) calculatedMinTime;
	}
}