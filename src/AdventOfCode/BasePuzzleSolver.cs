using System.Collections.Generic;

namespace AdventOfCode;

public interface IPuzzleSolver
{
	public void PrepareData(List<string> input);
	public void Solve();
}

public abstract class BasePuzzleSolver : IPuzzleSolver
{
	public string PuzzleLink => $"{Program.BaseUrl}/{PuzzleYear}/day/{PuzzleNumber}";
	
	public string DownloadLink => $"{PuzzleLink}/input";

	public abstract string Title { get; }

	public abstract int PuzzleNumber { get; }

	public abstract int PuzzleYear { get; }

	public abstract void PrepareData(List<string> input);
	public abstract void Solve();
}