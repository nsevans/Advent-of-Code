namespace AdventOfCode.Common.Models;

public class GeneratorContext(int year, int day)
{
	public readonly int Year = year;
	public readonly int Day = day;

	public string DisplayDay => Day.ToString().PadLeft(2, '0');
}