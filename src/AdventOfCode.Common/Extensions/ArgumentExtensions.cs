using System;
using System.Linq;

namespace AdventOfCode.Common.Extensions;

public static class ArgumentExtensions
{
	/// <summary>
	/// Get the value of the corresponding argument
	/// </summary>
	/// <param name="args">The list of arguments</param>
	/// <param name="arg">The requested argument</param>
	/// <param name="isRequired">Flag that determines if the requested argument is required. If it is required and not present an exception is thrown</param>
	/// <returns></returns>
	/// <exception cref="ArgumentException"></exception>
	public static string GetValueForArgument(this string[] args, string arg, bool isRequired = false)
	{
		var argIndex = Array.IndexOf(args, arg);

		// Argument not found
		if (argIndex == -1)
		{
			if (isRequired)
				throw new ArgumentException($"Argument '{arg}' is a required argument.");
			return null;
		}

		// Argument value not given
		if (argIndex + 1 >= args.Length)
			throw new ArgumentException($"Argument '{arg}' requires a corresponding value.");

		return args[argIndex + 1];
	}

	/// <summary>
	/// Get the first value of the corresponding list of argument options.
	/// Used for scenarios multiple arguments are for the same value, i.e.: "--year" and "-y"
	/// </summary>
	/// <param name="args">The list of arguments</param>
	/// <param name="argOptions">A list of requested arguments</param>
	/// <param name="isRequired">Flag that determines if the requested argument is required. If it is required and not present an exception is thrown</param>
	/// <returns></returns>
	/// <exception cref="ArgumentException"></exception>
	public static string GetValueForArgument(this string[] args, string[] argOptions, bool isRequired = false, string[] allowedValues = null)
	{
		if (!argOptions.Any(a => args.Contains(a)))
		{
			if (isRequired)
				throw new ArgumentException($"Argument '{argOptions[0]}' is a required argument.");
			return null;
		}

		if (argOptions.Intersect(args).Count() >= 2)
			throw new ArgumentException($"Cannot specify the same arguments twice: [{string.Join(", ", argOptions)}]");

		var value = (string)null;

		foreach (var argOption in argOptions)
		{
			var argIndex = Array.IndexOf(args, argOption);

			// Argument not found, try next
			if (argIndex == -1)
				continue;

			// Argument value not given
			if (argIndex + 1 >= args.Length)
				throw new ArgumentException($"Argument '{argOption}' requires a corresponding value.");

			value = args[argIndex + 1];
			break;
		}

        if (allowedValues != null && !allowedValues.Contains(value))
            throw new ArgumentException($"Argument '{argOptions}' passed invalid value '{value}'.");

		return value;
	}

	/// <summary>
	/// Check if the given argument exists
	/// </summary>
	/// <param name="args">The list of arguments</param>
	/// <param name="arg">The requested argument</param>
	/// <returns></returns>
	public static bool HasArgument(this string[] args, string arg) => args.Contains(arg);

	/// <summary>
	/// Check if any of the given arguments exists
	/// </summary>
	/// <param name="args">The list of arguments</param>
	/// <param name="argOptions">The list of requested arguments</param>
	/// <returns></returns>
	public static bool HasArgument(this string[] args, string[] argOptions) => argOptions.Any(a => args.Contains(a));
}