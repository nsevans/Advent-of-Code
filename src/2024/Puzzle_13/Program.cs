using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Year_2024.Puzzle_13;

/// <summary>
/// Link: https://adventofcode.com/2024/day/13
/// Input Format:
/// 	Button A: X+94, Y+34
///		Button B: X+22, Y+67
///		Prize: X=8400, Y=5400
///		
///		Button A: X+26, Y+66
///		Button B: X+67, Y+21
///		Prize: X=12748, Y=12176
/// </summary>
public class Program
{

	public static void Main(string[] args)
	{
		Console.WriteLine("### 2024 - Puzzle 13 ###\n");
		var input = GetInput("./input.txt");

		Console.WriteLine(CalculateSumOfTokensUsedForClawMachines(input));
		Console.WriteLine(CalculateSumOfTokensUsedForValidClawMachinesWithOffset(input));
	}

	public static List<string> GetInput(string fileName)
	{
		return File.ReadLines(fileName).ToList();
	}

	private static List<Game> ParseInput(List<string> input)
	{
		var games = new List<Game>();

		for (var i = 0; i < input.Count(); i += 4)
		{
			var buttonA = ParseLine(input[i]);
			var buttonB = ParseLine(input[i+1]);
			var prize = ParseLine(input[i+2]);
			
			games.Add(new Game(buttonA, buttonB, prize));
		}

		return games;
	}

	private static Vector2 ParseLine(string line)
	{
		// Select indeces with integers present (last two) and strip out any non digit characters
		var parsedLine = line
			.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[^2..]
			.Select(x => int.Parse(new string(x.Where(c => char.IsDigit(c)).ToArray())))
			.ToList();
		
		return new Vector2(parsedLine[0], parsedLine[1]);
	}

	private record Game(Vector2 ButtonA, Vector2 ButtonB, Vector2 Prize);

	private static double CalculateButtonPresses(Game game)
	{
		return CalculateButtonPressesWithOffset(game, 0);
	}

	private static double CalculateButtonPressesWithOffset(Game game, double offset)
	{
		// Use Cramer's Rule to determine number of presses per button
		var prizeX = game.Prize.X + offset;
		var prizeY = game.Prize.Y + offset;

		var determinant = (double)(game.ButtonA.X * game.ButtonB.Y) - (double)(game.ButtonA.Y * game.ButtonB.X);

		var a = ((prizeX * game.ButtonB.Y) - (prizeY * game.ButtonB.X)) / determinant;
		var b = ((prizeY * game.ButtonA.X) - (prizeX * game.ButtonA.Y)) / determinant;

		var resultX = (game.ButtonA.X * a) + (game.ButtonB.X * b);
		var resultY = (game.ButtonA.Y * a) + (game.ButtonB.Y * b);

		// Ensure results are integers
		if (Math.Floor(a) == a && Math.Floor(b) == b && resultX == prizeX && resultY == prizeY)
			return (a * 3) + b;
		else
			return 0;
	}

	#region PART 1
	private static double CalculateSumOfTokensUsedForClawMachines(List<string> input)
	{
		var games = ParseInput(input);
		var sum = 0d;

		foreach (var game in games)
		{
			sum += CalculateButtonPresses(game);
		}

		return sum;
	}
	#endregion

	#region PART 2
	private static double CalculateSumOfTokensUsedForValidClawMachinesWithOffset(List<string> input)
	{
		var games = ParseInput(input);
		var sum = 0d;

		foreach (var game in games)
		{
			sum += CalculateButtonPressesWithOffset(game, 10_000_000_000_000ul);
		}

		return sum;
	}
	#endregion
}