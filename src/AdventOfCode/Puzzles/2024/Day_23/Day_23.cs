using System;
using System.Collections.Generic;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode.Puzzles.Year_2024.Day_23;

/// <summary>
/// This base class is to store shared logic between Parts 1 and 2 of this day
///
/// Link to Day https://adventofcode.com/2024/day/23
///
/// Example/Test Input:
///		kh-tc
///		qp-kh
///		de-cg
///		ka-co
///		yn-aq
///		qp-ub
///		cg-tb
///		vc-aq
///		tb-ka
///		wh-tc
///		yn-cg
///		kh-ub
///		ta-co
///		de-co
///		tc-td
///		tb-wq
///		wh-td
///		ta-ka
///		td-qp
///		aq-cg
///		wq-ub
///		ub-vc
///		de-ta
///		wq-aq
///		wq-vc
///		wh-yn
///		ka-de
///		kh-ta
///		co-tc
///		wh-qp
///		tb-vc
///		td-yn
/// </summary>
public abstract class Day_23 : BaseDotnetSolver
{
	public override string Title => "LAN Party";
	public override int Day => 23;
	public override int Year => 2024;


	protected static Dictionary<string, List<string>> ParseConnectedComputers(List<string> input)
	{
		var connectedComputers = new Dictionary<string, List<string>>();

		foreach (var line in input)
		{
			var computers = line.Split('-', StringSplitOptions.RemoveEmptyEntries);
			var c1 = computers[0];
			var c2 = computers[1];
			connectedComputers.CreateOrAdd(c1, c2);
			connectedComputers.CreateOrAdd(c2, c1);
		}

		return connectedComputers;
	}
}