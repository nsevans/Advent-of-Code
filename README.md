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

Total Time for 89 puzzles: 16705.6745 ms

| Year | Day | Part | Language | Title | Total Time |
|:-----|:----|:-----|:---------|:------|-----------:|
| 2022 | 1 | 1 | Python | Calorie Counting | 0.6212 ms |
| 2022 | 1 | 2 | Python | Calorie Counting | 0.7771 ms |
| 2022 | 2 | 1 | Python | Rock Paper Scissors | 0.8471 ms |
| 2022 | 2 | 2 | Python | Rock Paper Scissors | 0.5500 ms |
| 2022 | 3 | 1 | Python | Rucksack Reorganization | 1.1902 ms |
| 2022 | 3 | 2 | Python | Rucksack Reorganization | 1.1670 ms |
| 2022 | 4 | 1 | Python | Camp Cleanup | 3.3408 ms |
| 2022 | 4 | 2 | Python | Camp Cleanup | 3.9866 ms |
| 2022 | 5 | 1 | Python | Supply Stacks | 1.1681 ms |
| 2022 | 5 | 2 | Python | Supply Stacks | 1.0366 ms |
| 2023 | 1 | 1 | C# | Trebuchet?! | 4.5234 ms |
| 2023 | 1 | 2 | C# | Trebuchet?! | 19.6249 ms |
| 2023 | 2 | 1 | C# | Cube Conundrum | 0.4627 ms |
| 2023 | 2 | 2 | C# | Cube Conundrum | 0.8578 ms |
| 2023 | 3 | 1 | C# | Gear Ratios | 2.0727 ms |
| 2023 | 3 | 2 | C# | Gear Ratios | 1.4275 ms |
| 2023 | 4 | 1 | C# | Scratchcards | 5.5650 ms |
| 2023 | 4 | 2 | C# | Scratchcards | 3067.7963 ms |
| 2023 | 5 | 1 | C# | If You Give A Seed A Fertilizer | 2.9091 ms |
| 2023 | 5 | 2 | C# | If You Give A Seed A Fertilizer | 3.4608 ms |
| 2024 | 1 | 1 | C# | Historian Hysteria | 3.2324 ms |
| 2024 | 1 | 2 | C# | Historian Hysteria | 46.0828 ms |
| 2024 | 2 | 1 | C# | Red-Nosed Reports | 1.5970 ms |
| 2024 | 2 | 2 | C# | Red-Nosed Reports | 2.1038 ms |
| 2024 | 3 | 1 | C# | Mull It Over | 6.0491 ms |
| 2024 | 3 | 2 | C# | Mull It Over | 1.7686 ms |
| 2024 | 4 | 1 | C# | Ceres Search | 15.6509 ms |
| 2024 | 4 | 2 | C# | Ceres Search | 0.9710 ms |
| 2024 | 5 | 1 | C# | Print Queue | 3.1782 ms |
| 2024 | 5 | 2 | C# | Print Queue | 3.9271 ms |
| 2024 | 6 | 1 | C# | Guard Gallivant | 1.4775 ms |
| 2024 | 6 | 2 | C# | Guard Gallivant | 452.8400 ms |
| 2024 | 7 | 1 | C# | Bridge Repair | 36.3902 ms |
| 2024 | 7 | 2 | C# | Bridge Repair | 701.9417 ms |
| 2024 | 8 | 1 | C# | Resonant Collinearity | 5.4365 ms |
| 2024 | 8 | 2 | C# | Resonant Collinearity | 1.1898 ms |
| 2024 | 9 | 1 | C# | Disk Fragmenter | 21.3881 ms |
| 2024 | 9 | 2 | C# | Disk Fragmenter | 857.5661 ms |
| 2024 | 10 | 1 | C# | Hoof It | 4.5199 ms |
| 2024 | 10 | 2 | C# | Hoof It | 2.0426 ms |
| 2024 | 11 | 1 | C# | Plutonian Pebbles | 29.1527 ms |
| 2024 | 11 | 2 | C# | Plutonian Pebbles | 47.1994 ms |
| 2024 | 12 | 1 | C# | Garden Groups | 3.4791 ms |
| 2024 | 12 | 2 | C# | Garden Groups | 72.7021 ms |
| 2024 | 13 | 1 | C# | Claw Contraption | 3.5935 ms |
| 2024 | 13 | 2 | C# | Claw Contraption | 1.6662 ms |
| 2024 | 14 | 1 | C# | Restroom Redoubt | 2.2741 ms |
| 2024 | 14 | 2 | C# | Restroom Redoubt | 28.0052 ms |
| 2024 | 15 | 1 | C# | Warehouse Woes | 12.0115 ms |
| 2024 | 15 | 2 | C# | Warehouse Woes | 60.4537 ms |
| 2024 | 16 | 1 | C# | Reindeer Maze | 834.2245 ms |
| 2024 | 16 | 2 | C# | Reindeer Maze | 906.6128 ms |
| 2024 | 17 | 1 | C# | Chronospatial Computer | 11.6094 ms |
| 2024 | 17 | 2 | C# | Chronospatial Computer | 0.4077 ms |
| 2024 | 18 | 1 | C# | RAM Run | 7.3020 ms |
| 2024 | 18 | 2 | C# | RAM Run | 5.4384 ms |
| 2024 | 19 | 1 | C# | Linen Layout | 77.1113 ms |
| 2024 | 19 | 2 | C# | Linen Layout | 236.8985 ms |
| 2024 | 20 | 1 | C# | Race Condition | 18.1144 ms |
| 2024 | 20 | 2 | C# | Race Condition | 1592.8821 ms |
| 2024 | 21 | 1 | C# | Keypad Conundrum | 3.8129 ms |
| 2024 | 21 | 2 | C# | Keypad Conundrum | 0.7726 ms |
| 2024 | 22 | 1 | C# | Monkey Market | 71.8518 ms |
| 2024 | 22 | 2 | C# | Monkey Market | 1883.8770 ms |
| 2024 | 23 | 1 | C# | LAN Party | 493.8388 ms |
| 2024 | 23 | 2 | C# | LAN Party | 22.8673 ms |
| 2024 | 24 | 1 | C# | Crossed Wires | 15.0544 ms |
| 2024 | 24 | 2 | C# | Crossed Wires | 3.1391 ms |
| 2024 | 25 | 1 | C# | Code Chronicle | 3.7534 ms |
| 2025 | 1 | 1 | Python | Secret Entrance | 1.9806 ms |
| 2025 | 1 | 2 | Python | Secret Entrance | 3.0025 ms |
| 2025 | 2 | 1 | Python | Gift Shop | 460.4170 ms |
| 2025 | 2 | 2 | Python | Gift Shop | 2613.4351 ms |
| 2025 | 3 | 1 | Python | Lobby | 0.1554 ms |
| 2025 | 3 | 2 | Python | Lobby | 0.2201 ms |
| 2025 | 4 | 1 | Python | Printing Department | 25.0952 ms |
| 2025 | 4 | 2 | Python | Printing Department | 332.7495 ms |
| 2025 | 5 | 1 | Python | Cafeteria | 1.4306 ms |
| 2025 | 5 | 2 | Python | Cafeteria | 0.5582 ms |
| 2025 | 6 | 1 | Python | Trash Compactor | 1.6590 ms |
| 2025 | 6 | 2 | Python | Trash Compactor | 1.6925 ms |
| 2025 | 7 | 1 | Python | Laboratories | 4.9771 ms |
| 2025 | 7 | 2 | Python | Laboratories | 1.4259 ms |
| 2025 | 8 | 1 | Python | Playground | 471.3123 ms |
| 2025 | 8 | 2 | Python | Playground | 455.2993 ms |
| 2025 | 9 | 1 | Python | Movie Theater | 43.6240 ms |
| 2025 | 9 | 2 | Python | Movie Theater | 5.6387 ms |
| 2025 | 10 | 1 | Python | Factory | 150.2526 ms |
| 2025 | 10 | 2 | Python | Factory | 387.9048 ms |
