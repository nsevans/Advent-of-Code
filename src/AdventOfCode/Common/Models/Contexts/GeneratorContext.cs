using AdventOfCode.Common.Extensions;
using AdventOfCode.Common.Models.Enums;

namespace AdventOfCode.Common.Models.Contexts;

public class GeneratorContext(int year, int day, string title, Language language)
{
	public readonly int Year = year;
	public readonly int Day = day;
	public readonly string Title = title;
    public readonly Language Language = language;

	public string DisplayDay => Day.PadLeft(2, '0');
}