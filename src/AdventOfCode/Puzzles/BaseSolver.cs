using System;
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
    public string TableHeading => $"{Year} | {Day} | {Part} | {GetSolvingLanguage()} | {Title}";

    /// <summary>
    /// Executes the PrepareData method and times how long it takes.
    /// </summary>
    /// <param name="input">A list of strings representing a file, each index is a row in the file</param>
    /// <returns>The time it takes the PrepareData method to execute</returns>
    public abstract TimeSpan TimePrepareDateExecution(List<string> input);

    /// <summary>
    /// Prepare any data needed for the solver to solve the puzzle.
    /// </summary>
    /// <param name="input">A list of strings representing a file, each index is a row in the file</param>
    public abstract void PrepareData(List<string> input);

    /// <summary>
    /// Executes the GetResult method and times how long it takes.
    /// </summary>
    /// <param name="result">The result the GetResult method returns on completion</param>
    /// <returns>The time it takes the GetResult method to execute</returns>
    public abstract TimeSpan TimeGetResultExecution(out string result);

    /// <summary>
    /// Solves the puzzle.
    /// </summary>
    /// <returns>The result of the puzzle</returns>
    public abstract string GetResult();

    public string GetSolvingLanguage()
    {
        if (this is BaseCSharpSolver)
            return "C#";
        else if (this is BasePythonSolver)
            return "Python";
        else
            return "Unknown";
    }
}