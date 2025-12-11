using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

namespace AdventOfCode.Puzzles.Solvers;

public abstract class BasePythonSolver : BaseSolver
{
    private string GetResultScriptPath => $"./Puzzles/{Year}/Day_{Day.ToString().PadLeft(2, '0')}/Part_{Part.ToString().PadLeft(2, '0')}.py";

    public override TimeSpan TimePrepareDateExecution(List<string> input) => TimeSpan.Zero;

    public override void PrepareData(List<string> input) { }

    public override TimeSpan TimeGetResultExecution(out string result)
    {
        var output = JsonSerializer.Deserialize<Dictionary<string, string>>(GetResult());
        result = output["result"];
        return TimeSpan.FromMilliseconds(double.Parse(output["time"]));;
    }

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