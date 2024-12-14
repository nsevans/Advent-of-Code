using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles.Year_2024.Day_14;

public class Part_02 : Day_14
{
	public override int Part => 2;

	private List<RobotData> _robotData;

	public override void PrepareData(List<string> input)
	{
		_robotData = ParseRobotData(input);
	}

	public override void Solve()
	{
		var result = CalculateLowestSecondWhereRobotsFormChristmasTree(_robotData);
		Console.WriteLine($"Lowest second where robots form christman tree: {result}");
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
			
			var xVariance = Variance(simulatedPositions.Select(d => d.X).ToList());
			var yVariance = Variance(simulatedPositions.Select(d => d.Y).ToList());

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

		var calculatedMinTime = minXVarianceTime + ((((Math.Pow(ModuloInverse(MapWidth, MapHeight), 1) % MapHeight) * (minYVarianceTime - minXVarianceTime)) % MapHeight) * MapWidth);
		return (int) calculatedMinTime;
	}

	private double Variance(List<double> data)
	{
		var avg = data.Average();
		var d = data.Aggregate(0.0, (total, next) => total += Math.Pow(next - avg, 2));
		return d / data.Count;
	}
    
    private int ModuloInverse(int a, int m)
	{
		// modulo inverse does not exist
        if (GreatestCommonDivisor(a, m) > 1)
            return -1;
           
        for (int X = 1; X < m; X++)
            if (((a % m) * (X % m)) % m == 1)
                return X;
        return 1;
    }

	private int GreatestCommonDivisor(int a, int b) {
        if (b == 0) {
            return a;
        }
        return GreatestCommonDivisor(b, a % b);
    }
}