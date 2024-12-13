using System;
using System.Linq;

namespace AdventOfCode.Extensions;

public static class ArgumentExtensions
{
	/// <summary>
	/// Get the value of the corresponding argument
	/// </summary>
	/// <param name="args">The list of arguments</param>
	/// <param name="arg">The requested argument</param>
	/// <param name="isRequired">Flag that determines if the requested argument is required. If it is required and not present an exception is thrown</param>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException"></exception>
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

	/// <summary>
	/// Check if the given argument exists
	/// </summary>
	/// <param name="args">The list of arguments</param>
	/// <param name="arg">The requested argument</param>
	/// <returns></returns>
	public static bool HasArgument(this string[] args, string arg) => args.Contains(arg);
}