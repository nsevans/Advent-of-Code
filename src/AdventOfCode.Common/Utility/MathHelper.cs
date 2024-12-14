using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Common.Utility;

public static class MathHelper
{
	/// <summary>
	/// Get the remainder after division regardless of if the value is positive or negative
	/// </summary>
	/// <param name="value"></param>
	/// <param name="maxSize"></param>
	/// <returns></returns>
	public static double Mod(double value, int maxSize)
	{
		while (value < 0)
			value += maxSize;
		return value % maxSize;
	}

	/// <summary>
	/// Attempting to emulate python's pow(base, exp) or pow(base, exp, mod) this method will calculate the base to the 
	/// exponent.
	/// <para>If the modulo is present and is non zero and exponent is negative, then base must be relatively prime to mode.
	/// In that case pow(inverse_base, -exponent, modulo) is returned, where inverse_base is an inverse to base modulo 
	/// modulo.</para>
	/// <para>See Python's documentation on it here: https://docs.python.org/3/library/functions.html#pow</para>
	/// </summary>
	/// <param name="base"></param>
	/// <param name="exponent"></param>
	/// <param name="modulo"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentException"></exception>
	public static double ModPower(double @base, double exponent, int? modulo = null)
	{
		if (modulo != null && modulo <= 0)
			throw new ArgumentException($"{nameof(modulo)} must be a non zero integer");
		
		if (modulo != null && exponent < 0)
		{
			var inverseBase = ModInverse(@base, modulo.Value);
			if (inverseBase == -1)
				throw new ArgumentException($"{nameof(@base)} must be relatively prime to {modulo}");

			return ModPower(inverseBase, -exponent, modulo);
		}

		if (modulo != null)
			return Math.Pow(@base, exponent) % modulo.Value;

		return Math.Pow(@base, exponent);
	}

	/// <summary>
	/// Get the modular inverse of the given values using a naive method
	/// </summary>
	/// <param name="value"></param>
	/// <param name="modulo"></param>
	/// <returns></returns>
    public static double ModInverse(double value, double modulo)
    {
		
		if (GreatestCommonDivisor((ulong)value, (ulong)modulo) != 1)
			return -1;
		
        for (int X = 1; X < modulo; X++)
            if (((value % modulo) * (X % modulo)) % modulo == 1)
                return X;
        return 1;
    }


	/// <summary>
	/// Get the variance of a list of values
	/// </summary>
	/// <param name="data"></param>
	/// <returns></returns>
	public static double Variance(List<double> data)
	{
		var avg = data.Average();
		var d = data.Aggregate(0.0, (total, next) => total += Math.Pow(next - avg, 2));
		return d / data.Count;
	}


	/// <summary>
	/// Find the greatest common divisor between the two given values
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns></returns>
	public static ulong GreatestCommonDivisor(ulong a, ulong b)
	{
        while (a != 0 && b != 0)
		{
			if (a > b)
				a %= b;
			else
				b %= a;
		}

		return a | b;
    }
}