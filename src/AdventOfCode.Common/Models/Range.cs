namespace AdventOfCode.Common.Models;

public class ValueRange(long start, long end)
{
    public long Start { get; } = start;
    public long End { get; } = end;

	/// <summary>
	/// Check wether this range contains the given range
	/// </summary>
	/// <param name="other"></param>
	/// <returns></returns>
    public bool Contains(ValueRange other) => other.Start >= Start && other.End <= End;

	/// <summary>
	/// Get the intersecting range of this range and another given range
	/// </summary>
	/// <param name="other"></param>
	/// <returns></returns>
	public ValueRange GetIntersection(ValueRange other)
	{
		// If this range is completely contained within the other range
		// other: 	|------------------|
		// this:		|--------|
		// result:		|--------|
		if (other.Contains(this))
			return new ValueRange(Start, End);

		// If the other range is completely contained within this range
		// other:		|--------|
		// this: 	|------------------|
		// result:		|--------|
		if (Contains(other))
			return new ValueRange(other.Start, other.End);

		// If the start of the other range is outside (below) this range but the end of the other range
		// is within this range
		// other: 	|----------|
		// this:		|----------|
		// result:		|------|
		if (other.Start <= Start && other.End >= Start && other.End <= End)
			return new ValueRange(Start, other.End);

		// If the start of the other range is within this range but the end of the other range
		// is outside (above) this range
		// other: 		|--------|
		// this:	|---------|
		// result:		|-----|
		if (other.End >= End && other.Start >= Start && other.Start <= End)
			return new ValueRange(other.Start, End);

		// Does not intersect
		return null;
	}
}