using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode.Puzzles;

public abstract class BasePythonSolver : BaseSolver
{
    private string GetResultScriptPath => $"./Puzzles/{Year}/Day_{Day.ToString().PadLeft(2, '0')}/Part_{Part.ToString().PadLeft(2, '0')}.py";

    public override void PrepareData(List<string> input) { }

    public override string GetResult()
    {
        var process = new Process()
        {
            StartInfo = new()
            {
                FileName = "python",
                UseShellExecute = false,
                Arguments = $"{GetResultScriptPath} {InputFilePath}",
                RedirectStandardOutput = true,
                RedirectStandardError = false
            }
        };

        var result = "";

        process.OutputDataReceived += (sender, arg) =>
        {
            if (!string.IsNullOrEmpty(arg.Data))
                result += arg.Data;
        };

        process.Start();
        process.BeginOutputReadLine();
        process.WaitForExit();
        process.Close();

        return result;
    }
}