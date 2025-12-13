namespace AdventOfCode.Puzzles.Year_2024.Day_20;

public class Part_02 : Day_20
{
    public override int Part => 2;

    public override string ResultMessage => "100 picosecond saving cheats when skipping for no more than 20 picoseconds";

    private static readonly int _cheatTimeToBeatThreshold = 100;
    private static readonly int _maxCheatTime = 20;

    private List<List<char>> _raceMap;
    private Dictionary<Point2D, int> _positionTimeDictionary;

    public override void PrepareData(List<string> input)
    {
        _raceMap = input.To2DCharList();
        _positionTimeDictionary = new() { { new Point2D(_raceMap.IndexOf('S')), 0 } };

        // Calculate time at each valid position on the track
        CalculateRaceMapTraversalTimes(_raceMap, _positionTimeDictionary);
    }

    public override string GetResult()
    {
        return CalculateSumOfSkipsSavingOver100Picoseconds(_raceMap, _positionTimeDictionary).ToString();
    }

    private static long CalculateSumOfSkipsSavingOver100Picoseconds(List<List<char>> raceMap, Dictionary<Point2D, int> positionTimeDictionary)
    {
        var validCheats = 0;
        // Map is surrounded by '#' on all sides so can ignore 0th and last x and y indeces
        for (var y = 1; y < raceMap.Count - 1; y++)
        {
            for (var x = 1; x < raceMap[y].Count - 1; x++)
            {
                if (raceMap[y][x] == '#')
                    continue;

                var currentPosition = new Point2D(x, y);

                // Check radius around current point, no greater than the max cheat length
                for (var cheatTime = 2; cheatTime < _maxCheatTime + 1; cheatTime++)
                {
                    for (var dy = 0; dy < cheatTime + 1; dy++)
                    {
                        var dx = cheatTime - dy;
                        var cheatPositions = new Point2D[]
                        {
                            currentPosition + ( dx,  dy),
                            currentPosition + (-dx,  dy),
                            currentPosition + ( dx, -dy),
                            currentPosition + (-dx, -dy)
                        }.Distinct();

                        // Check each position, if it is safe and the time saved is greater than the cheat
                        // threshold plus the time it took to perform the cheat (r) then it is a valid time
                        // saving cheat
                        foreach (var cheatPosition in cheatPositions)
                        {
                            if (!raceMap.IsSafe(cheatPosition, ['#']))
                                continue;

                            var timeAtCurrentPosition = positionTimeDictionary[currentPosition];
                            var timeAtCheatedPosition = positionTimeDictionary[cheatPosition];

                            var savedTime = timeAtCheatedPosition - timeAtCurrentPosition;
                            var timeToBeat = _cheatTimeToBeatThreshold + cheatTime;

                            if (savedTime >= timeToBeat)
                                validCheats++;
                        }
                    }
                }
            }
        }

        return validCheats;
    }
}