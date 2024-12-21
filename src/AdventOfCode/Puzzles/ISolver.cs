using System.Collections.Generic;

namespace AdventOfCode.Puzzles;

public interface ISolver
{
	public void PrepareData(List<string> input);

	public string GetResult();
}