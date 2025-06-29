# Advent Of Code Attempts

This project is for all of my attempts at the Advent of Code puzzles found on the [Advent of Code](https://adventofcode.com) website and the tools I've built alongside the attempts to help with solving puzzles.

## Index

* [Solvers](#solvers)
  * [Running Solvers](#running-solvers)
  * [Solver Examples](#solver-examples)
* [Generators](#generator)
  * [Running Generators](#running-generators)
  * [Generator Examples](#generator-examples)
* [Solver Run Results](#solver-run-results)

## Solvers

Each puzzle solver is split up by year, day, and part and can be found within /src/AdventOfCode/Puzzles.

The inputs to the corresponding puzzle can be found within /src/AdventOfCode/Inputs where each input file is denoted by the year and day (ex: `input_2024_09.in`). The inputs are **not** included in this project and are **not** automatically downloaded from the AoC website. Download them seperately andcopy them into the Inputs folder.

When running the solver, the puzzles will be executed in ascending order of year, day, and part.

### Running Solvers

```Powershell
dotnet run -- [-s|--solve]
    [-y|--year <int>]
    [-d|--day <int>]
    [-p|--part <int>]
    [-v|--verbose]
    [-o|--markdown-output <string>]
```

|  Flag |  Example |  Accepted Values/Types | Required | Description |
|-------|----------|------------------------|----------|-------------|
| `[-s\|--solve]` | `--solve` | N/A | Yes | Execute the puzzle solver. |
| `[-y\|--year]` | `--year 2024` | `int` | No | Execute all puzzle solvers for the given year. |
| `[-d\|--day]` | `--day 1` | `int`, 1 to 25 | No | Execute all puzzle solvers for the given day. |
| `[-p\|--part]` | `--part 1` | `int`, 1 or 2 | No | Execute all puzzle solvers for the given part. |
| `[-v\|--verbose]` | `--verbose` | N/A | No | Display more detailed information about each puzzle's results. |
| `[-o\|--markdown-output]` | `--markdown-output` | `string`, file path | No | Outputs a markdown ready table to the given file path. |

### Solver Examples

The above flags can be combined to execute a more refined subset of puzzle solvers:

* Execute all puzzles in the project  
`dotnet run -- --solve`
* Execute both parts of day 9 from the 2024 advent calendar:  
`dotnet run -- --solve --year 2024 --day 9`
* Execute part 1 of day 9 from the 2024 advent calendar:  
`dotnet run -- --solve --year 2024 --day 9 --part 1`
* Execute all part 1s of day 10 for each advent calendar year:  
`dotnet run -- --solve --day 10 --part 1`
* Execute part 2 of day 12 from the 2024 advent calendar with a more detailed output:  
`dotnet run -- --solve --year 2024 --day 12 --part 2 -verbose`
* Execute all solvers and output the results in a formatted markdown table to "results-table.md":  
`dotnet run -- --solve --markdown-output "/results-table.md"`

## Generator

The generator makes it easier to set up the base classes for each new Advent of Code puzzle. All it requires is the **year** and **day** and it can generate a complete base class. The only things required after the generation is complete is for the `Title` to be added (if not specified at run time), and the `Example/Test Input:` comment to added. The `Example/Test Input:` is the input that is given in the explanation for the puzzle.

### Running Generators

```Powershell
dotnet run -- [-g|--generate]
    [-y|--year <int>]
    [-d|--day <int>]
```

|  Flag |  Example |  Accepted Values/Types | Required | Description |
|-------|----------|------------------------|----------|-------------|
| `[-g\|--generate]` | `--generate` | N/A | Yes | Execute the puzzle class generator. |
| `[-y\|--year]` | `--year 2025` | `int` | Yes | Set the year that the generator is generating the classes for. |
| `[-d\|--day]` | `--day 1` | `int`, 1 to 25 | Yes | Set the day that the generator is generating the classes for. |
| `[-t\|--title]` | `--title "Title of the puzzle"` | `string` | No | Set the title of the puzzle. |

### Generator Examples

* Generate a puzzle and its parts:  
`dotnet run -- --generate --year 2025 --day 1`
* Generate a puzzle, its parts and give it a title:  
`dotnet run -- --generate --year 2025 --day 1 --title "Title of the puzzle"`

## Solver Run Results

Total Time for 57 puzzles: 8015.4398 ms

| Year | Day | Part | Title | Total Time |
|:-----|:----|:-----|:------|-----------:|
| 2023 | 1  | 1   | Trebuchet?! | 1.4731 ms |
| 2023 | 1  | 2   | Trebuchet?! | 17.3211 ms |
| 2023 | 2  | 1   | Cube Conundrum | 0.5704 ms |
| 2023 | 2  | 2   | Cube Conundrum | 0.4779 ms |
| 2023 | 3  | 1   | Gear Ratios | 2.2159 ms |
| 2023 | 3  | 2   | Gear Ratios | 1.5071 ms |
| 2023 | 4  | 1   | Scratchcards | 5.2506 ms |
| 2023 | 4  | 2   | Scratchcards | 2843.2961 ms |
| 2023 | 5  | 1   | If You Give A Seed A Fertilizer | 3.6170 ms |
| 2023 | 5  | 2   | If You Give A Seed A Fertilizer | 3.8678 ms |
| 2024 | 1  | 1   | Historian Hysteria | 3.4497 ms |
| 2024 | 1  | 2   | Historian Hysteria | 52.7011 ms |
| 2024 | 2  | 1   | Red-Nosed Reports | 1.4804 ms |
| 2024 | 2  | 2   | Red-Nosed Reports | 1.9742 ms |
| 2024 | 3  | 1   | Mull It Over | 6.4917 ms |
| 2024 | 3  | 2   | Mull It Over | 1.6648 ms |
| 2024 | 4  | 1   | Ceres Search | 15.9219 ms |
| 2024 | 4  | 2   | Ceres Search | 0.9427 ms |
| 2024 | 5  | 1   | Print Queue | 3.0555 ms |
| 2024 | 5  | 2   | Print Queue | 3.8016 ms |
| 2024 | 6  | 1   | Guard Gallivant | 1.2810 ms |
| 2024 | 6  | 2   | Guard Gallivant | 415.0967 ms |
| 2024 | 7  | 1   | Bridge Repair | 34.7377 ms |
| 2024 | 7  | 2   | Bridge Repair | 748.6030 ms |
| 2024 | 8  | 1   | Resonant Collinearity | 6.1530 ms |
| 2024 | 8  | 2   | Resonant Collinearity | 1.1765 ms |
| 2024 | 9  | 1   | Disk Fragmenter | 25.2028 ms |
| 2024 | 9  | 2   | Disk Fragmenter | 843.1995 ms |
| 2024 | 10  | 1   | Hoof It | 4.5673 ms |
| 2024 | 10  | 2   | Hoof It | 2.0170 ms |
| 2024 | 11  | 1   | Plutonian Pebbles | 31.5439 ms |
| 2024 | 11  | 2   | Plutonian Pebbles | 47.1243 ms |
| 2024 | 12  | 1   | Garden Groups | 3.4989 ms |
| 2024 | 12  | 2   | Garden Groups | 14.9284 ms |
| 2024 | 13  | 1   | Claw Contraption | 3.6550 ms |
| 2024 | 13  | 2   | Claw Contraption | 1.7482 ms |
| 2024 | 14  | 1   | Restroom Redoubt | 2.2377 ms |
| 2024 | 14  | 2   | Restroom Redoubt | 28.3336 ms |
| 2024 | 15  | 1   | Warehouse Woes | 7.3234 ms |
| 2024 | 15  | 2   | Warehouse Woes | 60.5939 ms |
| 2024 | 16  | 1   | Reindeer Maze | 908.8644 ms |
| 2024 | 16  | 2   | Reindeer Maze | 925.9132 ms |
| 2024 | 17  | 1   | Chronospatial Computer | 11.5786 ms |
| 2024 | 17  | 2   | Chronospatial Computer | 0.4262 ms |
| 2024 | 18  | 1   | RAM Run | 6.3334 ms |
| 2024 | 18  | 2   | RAM Run | 6.3221 ms |
| 2024 | 19  | 1   | Linen Layout | 82.7112 ms |
| 2024 | 19  | 2   | Linen Layout | 227.2122 ms |
| 2024 | 20  | 1   | Race Condition | 11.6388 ms |
| 2024 | 21  | 1   | Keypad Conundrum | 9.5032 ms |
| 2024 | 21  | 2   | Keypad Conundrum | 0.9580 ms |
| 2024 | 22  | 1   | Monkey Market | 1.0738 ms |
| 2024 | 22  | 2   | Monkey Market | 0.5370 ms |
| 2024 | 23  | 1   | LAN Party | 486.5946 ms |
| 2024 | 23  | 2   | LAN Party | 23.2362 ms |
| 2024 | 24  | 1   | Crossed Wires | 13.5619 ms |
| 2024 | 25  | 1   | Code Chronicle | 4.0005 ms |
