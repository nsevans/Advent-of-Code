using System.Collections.Generic;

namespace AdventOfCode.Common.Extensions;

public static class DictionaryExtensions
{
	/// <summary>
	/// If the key exists add the value to the existing list, otherwise create the entry for the
	/// key and add the value to the new list.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	/// <param name="dict"></param>
	/// <param name="key"></param>
	/// <param name="value"></param>
	public static void CreateOrAdd<TKey, TValue>(this Dictionary<TKey, List<TValue>> dict, TKey key, TValue value)
	{
		if (!dict.TryAdd(key, [value]))
			dict[key].Add(value);
	}

	/// <summary>
	/// If the key exists set the value for that key, otherwise create the entry for the key and
	/// set the value.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	/// <param name="dict"></param>
	/// <param name="key"></param>
	/// <param name="value"></param>
	public static void CreateOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
	{
		if (dict.TryAdd(key, value))
			dict[key] = value;
	}

	/// <summary>
	/// Try to get the value of the given key from the dictionary, otherwise return the defined default value
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	/// <param name="dict"></param>
	/// <param name="key"></param>
	/// <param name="definedDefault"></param>
	/// <returns></returns>
	public static TValue GetValueOrDefinedDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue definedDefault)
	{
		if (dict.TryGetValue(key, out var value))
			return value;

		return definedDefault;
	}
}