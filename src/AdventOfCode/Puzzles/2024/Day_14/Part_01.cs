using System;
using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Year_2024.Day_14;

public class Part_01 : Day_14
{
	public override int Part => 1;

	private List<RobotData> _robotData;

	public override void PrepareData(List<string> input)
	{
		_robotData = ParseRobotData(input);
	}

	public override void Solve()
	{
		var result = CalculateSafetyFactorAfter100Seconds(_robotData);
		Console.WriteLine($"Robot safety factor after 100 seconds simulated: {result}");
	}

	private int CalculateSafetyFactorAfter100Seconds(List<RobotData> robotData)
	{
		var newPositions = SimulateRobotsToSecond(robotData, SecondsToSimulate);
		return CalculateSafetyFactor(newPositions);
	}

	private int CalculateSafetyFactor(List<(double X, double Y)> positionData)
    {
        var lowerMapWidth = Math.Floor((double)MapWidth / 2) - 1;
		var upperMapWidth = Math.Ceiling((double)MapWidth / 2);
		var lowerMapHeight = Math.Floor((double)MapHeight / 2) - 1;
		var upperMapHeight = Math.Ceiling((double)MapHeight / 2);

		var xBoundry = MapWidth - upperMapWidth;
		var yBoundry = MapHeight - upperMapHeight;

		var quadrantCount = (a: 0, b: 0, c: 0, d: 0);
		foreach (var newPosition in positionData)
		{
			if (newPosition.X == xBoundry || newPosition.Y == yBoundry)
				continue;
			else if (newPosition.X <= lowerMapWidth && newPosition.Y <= lowerMapHeight)
				quadrantCount.a++;
			else if (newPosition.X <= lowerMapWidth && newPosition.Y >= upperMapHeight)
				quadrantCount.b++;
			else if (newPosition.X >= upperMapWidth && newPosition.Y <= lowerMapHeight)
				quadrantCount.c++;
			else if (newPosition.X >= upperMapWidth && newPosition.Y >= upperMapHeight)
				quadrantCount.d++;
		}

		return quadrantCount.a * quadrantCount.b * quadrantCount.c * quadrantCount.d;
    }
}