namespace AdventOfCode.Services.Solve.BaseSolvers;

public interface ISolver
{
	public void PrepareData(List<string> input);

	public string GetResult();
}