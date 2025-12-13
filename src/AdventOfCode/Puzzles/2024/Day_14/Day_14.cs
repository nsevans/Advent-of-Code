using System;
using System.Collections.Generic;
using System.Numerics;
using AdventOfCode.Common.Utility;
using AdventOfCode.Services.Solve.BaseSolvers;

namespace AdventOfCode.Puzzles.Year_2024.Day_14;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2024/day/14
///
/// Example/Test Input:
///		p=0,4 v=3,-3
///     p=6,3 v=-1,-3
///     p=10,3 v=-1,2
///     p=2,0 v=2,-1
///     p=0,0 v=1,3
///     p=3,0 v=-2,-2
///     p=7,6 v=-1,-3
///     p=3,0 v=-1,-2
///     p=9,3 v=2,3
///     p=7,3 v=-1,2
///     p=2,4 v=2,-3
///     p=9,5 v=-3,-3
/// </summary>
public abstract class Day_14 : BaseCSharpSolver
{
	public override string Title => "Restroom Redoubt";
    public override int Day => 14;
    public override int Year => 2024;

    protected int MapWidth = 101;
	protected int MapHeight = 103;
	protected int SecondsToSimulate = 100;

    protected class RobotData(Vector2 position, Vector2 velocity)
    {
        public Vector2 Position = position;
        public Vector2 Velocity = velocity;
    };

    protected static List<RobotData> ParseRobotData(List<string> input)
	{
		var data = new List<RobotData>();

		foreach (var line in input)
		{
			var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

			var positionData = split[0].Split("=")[1].Split(',');
			var position = new Vector2(int.Parse(positionData[0]), int.Parse(positionData[1]));

			var velocityData = split[1].Split("=")[1].Split(',');
			var velocity = new Vector2(int.Parse(velocityData[0]), int.Parse(velocityData[1]));

			data.Add(new RobotData(position, velocity));
		}

		return data;
	}

    protected List<(double X, double Y)> SimulateRobotsToSecond(List<RobotData> robotData, int second)
    {
        var newData = new List<(double x, double y)>();
        for (var i = 0; i < robotData.Count; i++)
		{
			var totalDistince = robotData[i].Velocity * second;

			var newX = MathHelper.Mod(robotData[i].Position.X + totalDistince.X, MapWidth);
			var newY = MathHelper.Mod(robotData[i].Position.Y + totalDistince.Y, MapHeight);

			newData.Add((newX, newY));
		}

        return newData;
    }
}