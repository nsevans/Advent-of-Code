using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Puzzles;

namespace AdventOfCode.Services;

public static class InputService
{
	public static List<string> GetInput(BaseSolver solver)
	{
		var paddedDay = solver.Day.ToString().Length == 1 ? $"0{solver.Day}" : solver.Day.ToString();
		var fileName = $"./Inputs/input_{solver.Year}_{paddedDay}.in";

		if (!File.Exists(fileName))
		{
			File.Create(fileName);	// Create file for easier setup if one isn't created
			throw new InvalidOperationException($"Ensure input exists for this day under '{Path.GetFullPath(fileName)}'.\nInput can be downloaded from {solver.DownloadLink}.");
		}

		var input = File.ReadLines(fileName).ToList();

		if (input.Count == 0 || string.IsNullOrWhiteSpace(string.Join("", input)))
			throw new InvalidOperationException($"Ensure the input file '{Path.GetFullPath(fileName)}' is not empty.\nInput can be downloaded from {solver.DownloadLink}.");

		return input;
	}
}