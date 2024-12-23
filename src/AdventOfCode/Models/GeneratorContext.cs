namespace AdventOfCode.Common.Models;

public class GeneratorContext(int year, int day, bool verbose)
{
	public readonly int Year = year;
	public readonly int Day = day;
	public readonly bool Verbose = verbose;

	public string DisplayDay => Day.ToString().PadLeft(2, '0');
}