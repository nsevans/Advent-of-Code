using System.Collections.Generic;

namespace AdventOfCode;

public interface ISolver
{
	public void PrepareData(List<string> input);

	public void Solve();
}

public abstract class BaseSolver : ISolver
{
	public string DayLink => $"{Program.BaseUrl}/{Year}/day/{Day}";
	
	public string DownloadLink => $"{DayLink}/input";

	public abstract string Title { get; }

	public abstract int Year { get; }

	public abstract int Day { get; }

	public abstract int Part { get; }

	public abstract void PrepareData(List<string> input);
	
	public abstract void Solve();
}