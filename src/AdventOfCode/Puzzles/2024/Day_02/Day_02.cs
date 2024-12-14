using System;

namespace AdventOfCode.Puzzles.Year_2024.Day_02;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
/// 
/// Link to Day https://adventofcode.com/2024/day/2
/// 
/// Input Format: 
/// 	7 6 4 2 1
/// 	1 2 7 8 9
/// 	9 7 6 2 1
/// 	1 3 2 4 5
/// 	8 6 4 4 1
/// 	1 3 6 7 9
/// </summary>
public abstract class Day_02 : BaseSolver
{
	public override string Title => "Red-Nosed Reports";
	public override int Day => 2;
	public override int Year => 2024;

	protected bool ValidateSet(int number1, int number2, bool isAscending)
	{
		var difference = Math.Abs(number1 - number2);
		if (number1 == number2)
			return false;

		else if(isAscending && number1 > number2)
			return false;

		else if(!isAscending && number1 < number2)
			return false;

		else if(difference < 1 || difference > 3)
			return false;

		return true;
	}
}