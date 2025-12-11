using System.Collections.Generic;

namespace AdventOfCode.Puzzles.Solvers;

public interface ISolver
{
	public void PrepareData(List<string> input);

	public string GetResult();
}