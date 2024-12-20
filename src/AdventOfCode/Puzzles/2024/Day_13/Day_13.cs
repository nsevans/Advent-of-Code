using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Puzzles.Year_2024.Day_13;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
/// 
/// Link to Day https://adventofcode.com/2024/day/13
/// 
/// Example/Test Input: 
/// 	Button A: X+94, Y+34
///		Button B: X+22, Y+67
///		Prize: X=8400, Y=5400
///		
///		Button A: X+26, Y+66
///		Button B: X+67, Y+21
///		Prize: X=12748, Y=12176
///		
///		Button A: X+17, Y+86
///		Button B: X+84, Y+37
///		Prize: X=7870, Y=6450
///		
///		Button A: X+69, Y+23
///		Button B: X+27, Y+71
///		Prize: X=18641, Y=10279
/// </summary>
public abstract class Day_13 : BaseSolver
{
	public override string Title => "Claw Contraption";
	public override int Day => 13;
	public override int Year => 2024;

	
	protected record Game(Vector2 ButtonA, Vector2 ButtonB, Vector2 Prize);

	protected static List<Game> ParseGames(List<string> input)
	{
		var games = new List<Game>();

		for (var i = 0; i < input.Count(); i += 4)
		{
			var buttonA = ParseGameData(input[i]);
			var buttonB = ParseGameData(input[i+1]);
			var prize = ParseGameData(input[i+2]);
			
			games.Add(new Game(buttonA, buttonB, prize));
		}

		return games;
	}

	protected static Vector2 ParseGameData(string line)
	{
		// Select indeces with integers present (last two) and strip out any non digit characters
		var parsedLine = line
			.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[^2..]
			.Select(x => int.Parse(new string(x.Where(c => char.IsDigit(c)).ToArray())))
			.ToList();
		
		return new Vector2(parsedLine[0], parsedLine[1]);
	}

	protected static double CalculateButtonPresses(Game game)
	{
		return CalculateButtonPressesWithOffset(game, 0);
	}

	protected static double CalculateButtonPressesWithOffset(Game game, double offset)
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

}