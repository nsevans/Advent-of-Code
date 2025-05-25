namespace AdventOfCode.Common.Models;

public class GeneratorContext(int year, int day, string title)
{
	public readonly int Year = year;
	public readonly int Day = day;
	public readonly string Title = title;

	public string DisplayDay => Day.ToString().PadLeft(2, '0');
}