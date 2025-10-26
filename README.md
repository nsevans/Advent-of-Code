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

Each puzzle solver is split up by year, day, and part and can be found within /src/AdventOfCode/Puzzles. Currently the puzzles can be solved in either C# or Python (all run with the same commands).

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
`dotnet run -- --solve --markdown-output "./results-table.md"`

## Generator

The generator makes it easier to set up the base classes for each new Advent of Code puzzle. All it requires is the **year** and **day** and it can generate a complete base class. The only things required after the generation is complete is for the `Title` to be added (if not specified at run time), and the `Example/Test Input:` comment to added. The `Example/Test Input:` is the input that is given in the explanation for the puzzle.

### Running Generators

```Powershell
dotnet run -- [-g|--generate]
    [-y|--year <int>]
    [-d|--day <int>]
    [-t|--title <string>]
    [-l|--language <string>]
```

|  Flag |  Example |  Accepted Values/Types | Required | Description |
|-------|----------|------------------------|----------|-------------|
| `[-g\|--generate]` | `--generate` | N/A | Yes | Execute the puzzle class generator. |
| `[-y\|--year]` | `--year 2025` | `int` | Yes | Set the year that the generator is generating the classes for. |
| `[-d\|--day]` | `--day 1` | `int`, 1 to 25 | Yes | Set the day that the generator is generating the classes for. |
| `[-t\|--title]` | `--title "Title of the puzzle"` | `string` | No | Set the title of the puzzle. |
| `[-l\|--language]` | `--language "python"` | Supported Languages: `python` or `dotnet` | Yes | Set the language the puzzle will be solved with. |

### Generator Examples

* Generate a puzzle and its parts:  
`dotnet run -- --generate --year 2025 --day 1 --language "dotnet"`
* Generate a puzzle, its parts and give it a title:  
`dotnet run -- --generate --year 2025 --day 1 --title "Title of the puzzle" --language "dotnet"`

## Solver Run Results

Total Time for 58 puzzles: 8293.0796 ms

| Year | Day | Part | Language | Title | Total Time |
|:-----|:----|:-----|:---------|:------|-----------:|
| 2023 | 1 | 1 | C# | Trebuchet?! | 1.4511 ms |
| 2023 | 1 | 2 | C# | Trebuchet?! | 17.2093 ms |
| 2023 | 2 | 1 | C# | Cube Conundrum | 0.5616 ms |
| 2023 | 2 | 2 | C# | Cube Conundrum | 0.4310 ms |
| 2023 | 3 | 1 | C# | Gear Ratios | 2.1056 ms |
| 2023 | 3 | 2 | C# | Gear Ratios | 1.5456 ms |
| 2023 | 4 | 1 | C# | Scratchcards | 5.2850 ms |
| 2023 | 4 | 2 | C# | Scratchcards | 2889.2055 ms |
| 2023 | 5 | 1 | C# | If You Give A Seed A Fertilizer | 3.1094 ms |
| 2023 | 5 | 2 | C# | If You Give A Seed A Fertilizer | 3.6697 ms |
| 2024 | 1 | 1 | C# | Historian Hysteria | 3.5580 ms |
| 2024 | 1 | 2 | C# | Historian Hysteria | 45.9845 ms |
| 2024 | 2 | 1 | C# | Red-Nosed Reports | 1.5048 ms |
| 2024 | 2 | 2 | C# | Red-Nosed Reports | 1.9926 ms |
| 2024 | 3 | 1 | C# | Mull It Over | 6.7015 ms |
| 2024 | 3 | 2 | C# | Mull It Over | 1.5950 ms |
| 2024 | 4 | 1 | C# | Ceres Search | 15.7518 ms |
| 2024 | 4 | 2 | C# | Ceres Search | 0.9311 ms |
| 2024 | 5 | 1 | C# | Print Queue | 3.2213 ms |
| 2024 | 5 | 2 | C# | Print Queue | 3.5307 ms |
| 2024 | 6 | 1 | C# | Guard Gallivant | 1.3158 ms |
| 2024 | 6 | 2 | C# | Guard Gallivant | 416.2385 ms |
| 2024 | 7 | 1 | C# | Bridge Repair | 34.3842 ms |
| 2024 | 7 | 2 | C# | Bridge Repair | 703.4636 ms |
| 2024 | 8 | 1 | C# | Resonant Collinearity | 6.7143 ms |
| 2024 | 8 | 2 | C# | Resonant Collinearity | 1.2040 ms |
| 2024 | 9 | 1 | C# | Disk Fragmenter | 24.4309 ms |
| 2024 | 9 | 2 | C# | Disk Fragmenter | 823.6223 ms |
| 2024 | 10 | 1 | C# | Hoof It | 4.6493 ms |
| 2024 | 10 | 2 | C# | Hoof It | 2.1213 ms |
| 2024 | 11 | 1 | C# | Plutonian Pebbles | 37.8950 ms |
| 2024 | 11 | 2 | C# | Plutonian Pebbles | 45.5385 ms |
| 2024 | 12 | 1 | C# | Garden Groups | 3.7029 ms |
| 2024 | 12 | 2 | C# | Garden Groups | 15.0802 ms |
| 2024 | 13 | 1 | C# | Claw Contraption | 3.5866 ms |
| 2024 | 13 | 2 | C# | Claw Contraption | 1.7488 ms |
| 2024 | 14 | 1 | C# | Restroom Redoubt | 2.2080 ms |
| 2024 | 14 | 2 | C# | Restroom Redoubt | 29.1890 ms |
| 2024 | 15 | 1 | C# | Warehouse Woes | 8.2411 ms |
| 2024 | 15 | 2 | C# | Warehouse Woes | 59.7498 ms |
| 2024 | 16 | 1 | C# | Reindeer Maze | 923.0491 ms |
| 2024 | 16 | 2 | C# | Reindeer Maze | 905.2665 ms |
| 2024 | 17 | 1 | C# | Chronospatial Computer | 11.4889 ms |
| 2024 | 17 | 2 | C# | Chronospatial Computer | 0.4281 ms |
| 2024 | 18 | 1 | C# | RAM Run | 6.2651 ms |
| 2024 | 18 | 2 | C# | RAM Run | 6.8020 ms |
| 2024 | 19 | 1 | C# | Linen Layout | 92.7478 ms |
| 2024 | 19 | 2 | C# | Linen Layout | 233.9289 ms |
| 2024 | 20 | 1 | C# | Race Condition | 12.0705 ms |
| 2024 | 20 | 2 | C# | Race Condition | 0.3285 ms |
| 2024 | 21 | 1 | C# | Keypad Conundrum | 9.3094 ms |
| 2024 | 21 | 2 | C# | Keypad Conundrum | 0.7601 ms |
| 2024 | 22 | 1 | C# | Monkey Market | 144.7908 ms |
| 2024 | 22 | 2 | C# | Monkey Market | 144.4596 ms |
| 2024 | 23 | 1 | C# | LAN Party | 480.5530 ms |
| 2024 | 23 | 2 | C# | LAN Party | 24.1988 ms |
| 2024 | 24 | 1 | C# | Crossed Wires | 13.2274 ms |
| 2024 | 25 | 1 | C# | Code Chronicle | 3.6235 ms |
