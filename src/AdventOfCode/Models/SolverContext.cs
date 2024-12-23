namespace AdventOfCode.Common.Models;

public class SolverContext(int? year, int? day, int? part, bool verbose)
{
	public readonly int? Year = year;
	public readonly int? Day = day;
	public readonly int? Part = part;
	public readonly bool Verbose = verbose;

	public bool NoFiltersSpecified() => Year == null && Day == null && Part == null;
	public string YearText => Year != null ? $"year {Year}" : "all years";
	public string DayText => Day != null ? $"day {Day}" : "all days";
	public string PartText => Part != null ? $"part {Part}" : "all parts";
}