using System;
using System.Linq;

namespace AdventOfCode.Extensions;

public static class ArgumentExtensions
{
	public static string GetValueForArgument(this string[] args, string arg, bool isRequired = false)
	{
		var argsList = args.ToList();
		var argIndex = argsList.IndexOf(arg);

		// Argument not found
		if (argIndex == -1)
		{
			if (isRequired)
				throw new InvalidOperationException($"Argument '{arg}' is a required argument.");
			return null;
		}

		// Argument value not given
		if (argIndex + 1 >= argsList.Count)
			throw new InvalidOperationException($"Argument '{arg}' requires a corresponding value.");

		return argsList[argIndex + 1];
	}

	public static bool HasArgument(this string[] args, string arg) => args.Contains(arg);
}