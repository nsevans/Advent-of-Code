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

Total Time for 90 puzzles: 15712.9683 ms

| Year | Day | Part | Language | Title | Total Time |
|:-----|:----|:-----|:---------|:------|-----------:|
| 2022 | 1 | 1 | Python | Calorie Counting | 0.5783 ms |
| 2022 | 1 | 2 | Python | Calorie Counting | 0.7430 ms |
| 2022 | 2 | 1 | Python | Rock Paper Scissors | 0.8078 ms |
| 2022 | 2 | 2 | Python | Rock Paper Scissors | 0.5277 ms |
| 2022 | 3 | 1 | Python | Rucksack Reorganization | 1.2143 ms |
| 2022 | 3 | 2 | Python | Rucksack Reorganization | 1.1336 ms |
| 2022 | 4 | 1 | Python | Camp Cleanup | 3.0502 ms |
| 2022 | 4 | 2 | Python | Camp Cleanup | 2.9660 ms |
| 2022 | 5 | 1 | Python | Supply Stacks | 1.1830 ms |
| 2022 | 5 | 2 | Python | Supply Stacks | 1.0103 ms |
| 2023 | 1 | 1 | C# | Trebuchet?! | 4.3238 ms |
| 2023 | 1 | 2 | C# | Trebuchet?! | 14.9576 ms |
| 2023 | 2 | 1 | C# | Cube Conundrum | 0.5357 ms |
| 2023 | 2 | 2 | C# | Cube Conundrum | 0.4496 ms |
| 2023 | 3 | 1 | C# | Gear Ratios | 2.2565 ms |
| 2023 | 3 | 2 | C# | Gear Ratios | 1.3719 ms |
| 2023 | 4 | 1 | C# | Scratchcards | 5.1768 ms |
| 2023 | 4 | 2 | C# | Scratchcards | 2585.7225 ms |
| 2023 | 5 | 1 | C# | If You Give A Seed A Fertilizer | 3.0016 ms |
| 2023 | 5 | 2 | C# | If You Give A Seed A Fertilizer | 4.0765 ms |
| 2024 | 1 | 1 | C# | Historian Hysteria | 3.6760 ms |
| 2024 | 1 | 2 | C# | Historian Hysteria | 20.6945 ms |
| 2024 | 2 | 1 | C# | Red-Nosed Reports | 1.5502 ms |
| 2024 | 2 | 2 | C# | Red-Nosed Reports | 2.1272 ms |
| 2024 | 3 | 1 | C# | Mull It Over | 6.0447 ms |
| 2024 | 3 | 2 | C# | Mull It Over | 1.8837 ms |
| 2024 | 4 | 1 | C# | Ceres Search | 15.4653 ms |
| 2024 | 4 | 2 | C# | Ceres Search | 0.9458 ms |
| 2024 | 5 | 1 | C# | Print Queue | 3.3851 ms |
| 2024 | 5 | 2 | C# | Print Queue | 4.4281 ms |
| 2024 | 6 | 1 | C# | Guard Gallivant | 1.4715 ms |
| 2024 | 6 | 2 | C# | Guard Gallivant | 481.1467 ms |
| 2024 | 7 | 1 | C# | Bridge Repair | 34.3950 ms |
| 2024 | 7 | 2 | C# | Bridge Repair | 817.2896 ms |
| 2024 | 8 | 1 | C# | Resonant Collinearity | 6.9144 ms |
| 2024 | 8 | 2 | C# | Resonant Collinearity | 1.3138 ms |
| 2024 | 9 | 1 | C# | Disk Fragmenter | 22.5503 ms |
| 2024 | 9 | 2 | C# | Disk Fragmenter | 837.1335 ms |
| 2024 | 10 | 1 | C# | Hoof It | 5.2252 ms |
| 2024 | 10 | 2 | C# | Hoof It | 2.3599 ms |
| 2024 | 11 | 1 | C# | Plutonian Pebbles | 33.6513 ms |
| 2024 | 11 | 2 | C# | Plutonian Pebbles | 46.5081 ms |
| 2024 | 12 | 1 | C# | Garden Groups | 3.8037 ms |
| 2024 | 12 | 2 | C# | Garden Groups | 64.4748 ms |
| 2024 | 13 | 1 | C# | Claw Contraption | 2.9059 ms |
| 2024 | 13 | 2 | C# | Claw Contraption | 1.1368 ms |
| 2024 | 14 | 1 | C# | Restroom Redoubt | 2.3635 ms |
| 2024 | 14 | 2 | C# | Restroom Redoubt | 23.1819 ms |
| 2024 | 15 | 1 | C# | Warehouse Woes | 7.4952 ms |
| 2024 | 15 | 2 | C# | Warehouse Woes | 53.6883 ms |
| 2024 | 16 | 1 | C# | Reindeer Maze | 791.9670 ms |
| 2024 | 16 | 2 | C# | Reindeer Maze | 820.0054 ms |
| 2024 | 17 | 1 | C# | Chronospatial Computer | 12.8702 ms |
| 2024 | 17 | 2 | C# | Chronospatial Computer | 0.4525 ms |
| 2024 | 18 | 1 | C# | RAM Run | 8.3785 ms |
| 2024 | 18 | 2 | C# | RAM Run | 5.8479 ms |
| 2024 | 19 | 1 | C# | Linen Layout | 68.8753 ms |
| 2024 | 19 | 2 | C# | Linen Layout | 199.2438 ms |
| 2024 | 20 | 1 | C# | Race Condition | 15.3030 ms |
| 2024 | 20 | 2 | C# | Race Condition | 1484.1215 ms |
| 2024 | 21 | 1 | C# | Keypad Conundrum | 4.3401 ms |
| 2024 | 21 | 2 | C# | Keypad Conundrum | 0.7757 ms |
| 2024 | 22 | 1 | C# | Monkey Market | 70.8444 ms |
| 2024 | 22 | 2 | C# | Monkey Market | 1792.3365 ms |
| 2024 | 23 | 1 | C# | LAN Party | 318.5131 ms |
| 2024 | 23 | 2 | C# | LAN Party | 18.6204 ms |
| 2024 | 24 | 1 | C# | Crossed Wires | 15.6074 ms |
| 2024 | 25 | 1 | C# | Code Chronicle | 3.5158 ms |
| 2025 | 1 | 1 | Python | Secret Entrance | 2.0604 ms |
| 2025 | 1 | 2 | Python | Secret Entrance | 3.0264 ms |
| 2025 | 2 | 1 | Python | Gift Shop | 464.4785 ms |
| 2025 | 2 | 2 | Python | Gift Shop | 2520.4233 ms |
| 2025 | 3 | 1 | Python | Lobby | 0.1651 ms |
| 2025 | 3 | 2 | Python | Lobby | 0.1657 ms |
| 2025 | 4 | 1 | Python | Printing Department | 25.5076 ms |
| 2025 | 4 | 2 | Python | Printing Department | 326.3709 ms |
| 2025 | 5 | 1 | Python | Cafeteria | 1.3978 ms |
| 2025 | 5 | 2 | Python | Cafeteria | 0.5794 ms |
| 2025 | 6 | 1 | Python | Trash Compactor | 1.6838 ms |
| 2025 | 6 | 2 | Python | Trash Compactor | 2.0064 ms |
| 2025 | 7 | 1 | Python | Laboratories | 5.1831 ms |
| 2025 | 7 | 2 | Python | Laboratories | 1.3770 ms |
| 2025 | 8 | 1 | Python | Playground | 424.7381 ms |
| 2025 | 8 | 2 | Python | Playground | 457.0182 ms |
| 2025 | 9 | 1 | Python | Movie Theater | 48.7738 ms |
| 2025 | 9 | 2 | Python | Movie Theater | 118.2833 ms |
| 2025 | 10 | 1 | Python | Factory | 149.5882 ms |
| 2025 | 10 | 2 | Python | Factory | 380.7772 ms |
| 2025 | 11 | 1 | Python | Reactor | 1.1420 ms |
| 2025 | 11 | 2 | Python | Reactor | 2.3069 ms |
