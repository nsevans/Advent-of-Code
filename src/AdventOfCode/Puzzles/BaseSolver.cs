using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Puzzles;

public abstract class BaseSolver : ISolver
{
	public string DayLink => $"{Program.BaseUrl}/{Year}/day/{Day}";
	
	public string DownloadLink => $"{DayLink}/input";

	public string InputFilePath => $"./Inputs/input_{Year}_{Day.ToString().PadLeft(2, '0')}.in";

	public abstract string Title { get; }

	public abstract int Year { get; }

	public abstract int Day { get; }

	public abstract int Part { get; }

	public abstract string ResultMessage { get; }

	public abstract void PrepareData(List<string> input);
	
	public abstract string GetResult();

	private string GetHeading(bool verbose)
	{
		var heading = new StringBuilder();
		if (verbose)
		{
			heading.Append($"#### Solving {Year}, Day {Day}, Part {Part}: {Title} ####");
			heading.Append($"\nLink: {DayLink}");
			if (Part == 2)
				heading.Append($"#part2");
		}
		else
		{
			heading.Append($"y{Year}-d{Day.ToString().PadLeft(2, '0')}-p{Part.ToString().PadLeft(2, '0')}");
		}
		return heading.ToString();
	}

	public void DisplayHeading(bool verbose)
	{
		var heading = GetHeading(verbose);
		if (verbose)
			Console.WriteLine($"{heading}");
		else
			Console.Write($"| {heading.PadRight(15)} | ");
	}	
}