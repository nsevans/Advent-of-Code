# Advent Of Code Attempts

This project is for all of my attempts at the Advent of Code puzzles found on the [Advent of Code](https://adventofcode.com) website and the tools I've built alongside the attempts to help with solving puzzles.

## Index

* [Help](#help)
* [Solve](#solve)
  * [Running Solvers](#running-solvers)
  * [Solver Examples](#solver-examples)
* [Generate](#generate)
  * [Running Generators](#running-generators)
  * [Generator Examples](#generator-examples)
* [Solver Run Results](#solver-run-results)

## Help

Use the command `dotnet run -- --help` to get a list of all available commands.

## Generate

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
| `[-l\|--language]` | `--language "python"` | Supported Languages: `python` or `csharp` | Yes | Set the language the puzzle will be solved with. |

### Generator Examples

* Generate a puzzle and its parts:
`dotnet run -- --generate --year 2025 --day 1 --language "csharp"`
* Generate a puzzle, its parts and give it a title:
`dotnet run -- --generate --year 2025 --day 1 --title "Title of the puzzle" --language "csharp"`

## Solve

Each puzzle solver is split up by year, day, and part and can be found within /src/AdventOfCode/Puzzles. Currently the puzzles can be solved in either C# or Python (all run with the same commands) and must be run from the `/src/AdventOfCode/` project as the file paths are relative.

For python puzzles, `python` is used to run the scripts, the version does not matter.

The inputs to the corresponding puzzle can be found within /src/AdventOfCode/Inputs where each input file is denoted by the year and day (ex: `input_2024_09.in`). The inputs are **not** included in this project and are **not** automatically downloaded from the AoC website as per the [FAQ](https://adventofcode.com/2025/about). Download them seperately and copy them into the Inputs folder.

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

* Execute all puzzles in the project: `dotnet run -- --solve`
* Execute both parts of day 9 from the 2024 advent calendar: `dotnet run -- --solve --year 2024 --day 9`
* Execute part 1 of day 9 from the 2024 advent calendar: `dotnet run -- --solve --year 2024 --day 9 --part 1`
* Execute all part 1s of day 10 for each advent calendar year: `dotnet run -- --solve --day 10 --part 1`
* Execute part 2 of day 12 from the 2024 advent calendar with a more detailed output: `dotnet run -- --solve --year 2024 --day 12 --part 2 -verbose`
* Execute all solvers and output the results in a formatted markdown table to "results-table.md": `dotnet run -- --solve --markdown-output "./results-table.md"`

## Solver Run Results

Total Time for 82 puzzles: 15232.1462 ms

| Year | Day | Part | Language | Title | Total Time |
|:-----|:----|:-----|:---------|:------|-----------:|
| 2022 | 1 | 1 | Python | Calorie Counting | 25.2918 ms |
| 2022 | 1 | 2 | Python | Calorie Counting | 25.9855 ms |
| 2022 | 2 | 1 | Python | Rock Paper Scissors | 24.9924 ms |
| 2022 | 2 | 2 | Python | Rock Paper Scissors | 26.1276 ms |
| 2022 | 3 | 1 | Python | Rucksack Reorganization | 25.7789 ms |
| 2022 | 3 | 2 | Python | Rucksack Reorganization | 25.5757 ms |
| 2022 | 4 | 1 | Python | Camp Cleanup | 40.7294 ms |
| 2022 | 4 | 2 | Python | Camp Cleanup | 42.5140 ms |
| 2022 | 5 | 1 | Python | Supply Stacks | 24.8271 ms |
| 2022 | 5 | 2 | Python | Supply Stacks | 26.4894 ms |
| 2023 | 1 | 1 | C# | Trebuchet?! | 1.2607 ms |
| 2023 | 1 | 2 | C# | Trebuchet?! | 20.5696 ms |
| 2023 | 2 | 1 | C# | Cube Conundrum | 0.4431 ms |
| 2023 | 2 | 2 | C# | Cube Conundrum | 0.4368 ms |
| 2023 | 3 | 1 | C# | Gear Ratios | 2.0760 ms |
| 2023 | 3 | 2 | C# | Gear Ratios | 1.4800 ms |
| 2023 | 4 | 1 | C# | Scratchcards | 6.8390 ms |
| 2023 | 4 | 2 | C# | Scratchcards | 2774.4750 ms |
| 2023 | 5 | 1 | C# | If You Give A Seed A Fertilizer | 3.2009 ms |
| 2023 | 5 | 2 | C# | If You Give A Seed A Fertilizer | 3.5619 ms |
| 2024 | 1 | 1 | C# | Historian Hysteria | 3.5451 ms |
| 2024 | 1 | 2 | C# | Historian Hysteria | 47.2137 ms |
| 2024 | 2 | 1 | C# | Red-Nosed Reports | 1.5136 ms |
| 2024 | 2 | 2 | C# | Red-Nosed Reports | 2.1758 ms |
| 2024 | 3 | 1 | C# | Mull It Over | 6.6584 ms |
| 2024 | 3 | 2 | C# | Mull It Over | 1.3396 ms |
| 2024 | 4 | 1 | C# | Ceres Search | 14.6144 ms |
| 2024 | 4 | 2 | C# | Ceres Search | 0.9410 ms |
| 2024 | 5 | 1 | C# | Print Queue | 3.5135 ms |
| 2024 | 5 | 2 | C# | Print Queue | 4.7046 ms |
| 2024 | 6 | 1 | C# | Guard Gallivant | 1.8908 ms |
| 2024 | 6 | 2 | C# | Guard Gallivant | 464.6583 ms |
| 2024 | 7 | 1 | C# | Bridge Repair | 35.8098 ms |
| 2024 | 7 | 2 | C# | Bridge Repair | 768.5273 ms |
| 2024 | 8 | 1 | C# | Resonant Collinearity | 5.8060 ms |
| 2024 | 8 | 2 | C# | Resonant Collinearity | 1.4200 ms |
| 2024 | 9 | 1 | C# | Disk Fragmenter | 24.8209 ms |
| 2024 | 9 | 2 | C# | Disk Fragmenter | 943.0780 ms |
| 2024 | 10 | 1 | C# | Hoof It | 4.4668 ms |
| 2024 | 10 | 2 | C# | Hoof It | 1.9307 ms |
| 2024 | 11 | 1 | C# | Plutonian Pebbles | 31.8503 ms |
| 2024 | 11 | 2 | C# | Plutonian Pebbles | 43.6325 ms |
| 2024 | 12 | 1 | C# | Garden Groups | 3.4411 ms |
| 2024 | 12 | 2 | C# | Garden Groups | 70.8574 ms |
| 2024 | 13 | 1 | C# | Claw Contraption | 3.8515 ms |
| 2024 | 13 | 2 | C# | Claw Contraption | 1.6556 ms |
| 2024 | 14 | 1 | C# | Restroom Redoubt | 2.0869 ms |
| 2024 | 14 | 2 | C# | Restroom Redoubt | 29.1657 ms |
| 2024 | 15 | 1 | C# | Warehouse Woes | 7.7206 ms |
| 2024 | 15 | 2 | C# | Warehouse Woes | 65.7399 ms |
| 2024 | 16 | 1 | C# | Reindeer Maze | 823.7063 ms |
| 2024 | 16 | 2 | C# | Reindeer Maze | 844.7057 ms |
| 2024 | 17 | 1 | C# | Chronospatial Computer | 11.1439 ms |
| 2024 | 17 | 2 | C# | Chronospatial Computer | 0.4651 ms |
| 2024 | 18 | 1 | C# | RAM Run | 6.2635 ms |
| 2024 | 18 | 2 | C# | RAM Run | 6.5594 ms |
| 2024 | 19 | 1 | C# | Linen Layout | 81.0021 ms |
| 2024 | 19 | 2 | C# | Linen Layout | 231.0935 ms |
| 2024 | 20 | 1 | C# | Race Condition | 14.1012 ms |
| 2024 | 20 | 2 | C# | Race Condition | 1491.3687 ms |
| 2024 | 21 | 1 | C# | Keypad Conundrum | 9.2264 ms |
| 2024 | 21 | 2 | C# | Keypad Conundrum | 0.7655 ms |
| 2024 | 22 | 1 | C# | Monkey Market | 70.8193 ms |
| 2024 | 22 | 2 | C# | Monkey Market | 1839.8752 ms |
| 2024 | 23 | 1 | C# | LAN Party | 519.2306 ms |
| 2024 | 23 | 2 | C# | LAN Party | 24.4900 ms |
| 2024 | 24 | 1 | C# | Crossed Wires | 12.0402 ms |
| 2024 | 25 | 1 | C# | Code Chronicle | 3.6016 ms |
| 2025 | 1 | 1 | Python | Secret Entrance | 25.8310 ms |
| 2025 | 1 | 2 | Python | Secret Entrance | 26.7459 ms |
| 2025 | 2 | 1 | Python | Gift Shop | 476.7404 ms |
| 2025 | 2 | 2 | Python | Gift Shop | 2384.7604 ms |
| 2025 | 3 | 1 | Python | Lobby | 24.1180 ms |
| 2025 | 3 | 2 | Python | Lobby | 23.1048 ms |
| 2025 | 4 | 1 | Python | Printing Department | 49.4958 ms |
| 2025 | 4 | 2 | Python | Printing Department | 350.6306 ms |
| 2025 | 5 | 1 | Python | Cafeteria | 24.8731 ms |
| 2025 | 5 | 2 | Python | Cafeteria | 24.7538 ms |
| 2025 | 6 | 1 | Python | Trash Compactor | 37.8028 ms |
| 2025 | 6 | 2 | Python | Trash Compactor | 36.6278 ms |
| 2025 | 7 | 1 | Python | Laboratories | 29.7085 ms |
| 2025 | 7 | 2 | Python | Laboratories | 25.2105 ms |
