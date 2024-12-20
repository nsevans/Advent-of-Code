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
	public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, List<TValue>> dict, TKey key, TValue value)
	{
		if (dict.ContainsKey(key))
			dict[key].Add(value);
		else
			dict.Add(key, [ value ]);
	}

	/// <summary>
	/// if the key exists set the value for that key, otherwise create the entry for the key and
	/// set the value.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	/// <param name="dict"></param>
	/// <param name="key"></param>
	/// <param name="value"></param>
	public static void SetOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
	{
		if (dict.ContainsKey(key))
			dict[key] = value;
		else
			dict.Add(key, value);
	}
}