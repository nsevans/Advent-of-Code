namespace AdventOfCode.Common.Models.Contexts;

public class GeneratorContext(int year, int day, string title, string language)
{
	public readonly int Year = year;
	public readonly int Day = day;
	public readonly string Title = title;
    public readonly string Language = language;

	public string DisplayDay => Day.ToString().PadLeft(2, '0');
}