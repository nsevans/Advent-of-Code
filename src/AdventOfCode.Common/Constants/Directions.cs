using System.Collections.Generic;

namespace AdventOfCode.Common.Constants;

public static class Directions
{
    public static List<(int x, int y)> All =>
    [
		( 0, -1),	// NORTH
		( 1, -1),	// NORTH EAST
		( 1,  0),	// EAST
		( 1,  1),	// SOUTH EAST
		( 0,  1),	// SOUTH
		(-1,  1),	// SOUTH WEST
        (-1,  0),	// WEST
		(-1, -1),	// NORTH WEST
    ];

    public static List<(int x, int y)> Cardinal =>
    [
        (0,  -1),	// NORTH
        ( 1,  0),	// EAST
        ( 0,  1),	// SOUTH
        (-1,  0),	// WEST
    ];

    public static List<(int x, int y)> Ordinal =>
    [
		( 1, -1),	// NORTH EAST
		( 1,  1),	// SOUTH EAST
		(-1,  1),	// SOUTH WEST
        (-1, -1),	// NORTH WEST
    ];
}