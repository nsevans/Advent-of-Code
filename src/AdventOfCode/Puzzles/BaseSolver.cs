using System.Collections.Generic;

namespace AdventOfCode.Puzzles;

public abstract class BaseSolver : ISolver
{
    public string DayLink => $"{Program.AdventOfCodeUrl}/{Year}/day/{Day}";

    public string DownloadLink => $"{DayLink}/input";

    public string InputFilePath => $"./Inputs/input_{Year}_{Day.ToString().PadLeft(2, '0')}.in";

    public abstract string Title { get; }

    public abstract int Year { get; }

    public abstract int Day { get; }

    public abstract int Part { get; }

    public abstract string ResultMessage { get; }

    public string DetailedHeading => $"#### Solving {Year}, Day {Day}, Part {Part}: {Title} ####\nLink: {DayLink}{(Part == 2 ? "#part2" : "")}\nLanguage: {GetSolvingLanguage()}";
    public string MinimalHeading => $"y{Year}-d{Day.ToString().PadLeft(2, '0')}-p{Part.ToString().PadLeft(2, '0')}";
    public string TableHeading => $"{Year} | {Day} | {Part} | {Title}";

    public abstract void PrepareData(List<string> input);

    public abstract string GetResult();

    public string GetSolvingLanguage()
    {
        if (this is BaseDotnetSolver)
            return "C#";
        else if (this is BasePythonSolver)
            return "Python";
        else
            return "Unknown";
    }
}