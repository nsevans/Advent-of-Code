using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode.Puzzles.Year_2024.Day_23;

public class Part_01 : Day_23
{
    public override int Part => 1;

	private Dictionary<string, List<string>> _connectedComputers;

	public override void PrepareData(List<string> input)
	{
		_connectedComputers = ParseConnectedComputers(input);
	}

	public override string ResultMessage => "Number of tri-connected computers where at least 1 starts with the letter 't'";

	public override string GetResult()
	{
		return CalculateNumberOfTriConnectedComputersContainingT(_connectedComputers).ToString();
	}

	private int CalculateNumberOfTriConnectedComputersContainingT(Dictionary<string, List<string>> connectedComputers)
	{
		var sum = 0;
		var triConnectedComputers = new List<List<string>>();

		foreach (var computer1 in connectedComputers.Keys)
		{
			foreach (var computer2 in connectedComputers[computer1])
			{
			
				foreach (var computer3 in connectedComputers[computer1].Where(c2 => c2 != computer2))
				{
					// Order connections so they can be found reliably in the list of already found tri connections list
					var connections = new List<string> { computer1, computer2, computer3 }.OrderBy(x => x).ToList();

					if (!connections.Any(c => c.StartsWith('t')))
						continue;
					
					// Ensure the computers are connected (aka appear in each others list of connected computers)
					var computer1IsConnected = connections.Where(c => c != computer2).All(c => connectedComputers[computer2].Contains(c));
					var computer2IsConnected = connections.Where(c => c != computer3).All(c => connectedComputers[computer3].Contains(c));
					var triConnectionFound = triConnectedComputers.Any(x => x.SequenceEqual(connections));

					if (computer1IsConnected && computer2IsConnected && !triConnectionFound)
					{
						triConnectedComputers.Add(connections);
						sum++;
					}
				}
			}
		}
		return sum;
	}
}