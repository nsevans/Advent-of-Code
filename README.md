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

Total Time for 70 puzzles: 9835.6792 ms

| Year | Day | Part | Language | Title | Total Time |
|:-----|:----|:-----|:---------|:------|-----------:|
| 2022 | 1 | 1 | Python | Calorie Counting | 36.8073 ms |
| 2022 | 1 | 2 | Python | Calorie Counting | 41.4759 ms |
| 2022 | 2 | 1 | Python | Rock Paper Scissors | 36.3198 ms |
| 2022 | 2 | 2 | Python | Rock Paper Scissors | 35.0289 ms |
| 2022 | 3 | 1 | Python | Rucksack Reorganization | 25.5139 ms |
| 2022 | 3 | 2 | Python | Rucksack Reorganization | 36.1102 ms |
| 2022 | 4 | 1 | Python | Camp Cleanup | 51.7083 ms |
| 2022 | 4 | 2 | Python | Camp Cleanup | 43.9528 ms |
| 2022 | 5 | 1 | Python | Supply Stacks | 48.0473 ms |
| 2022 | 5 | 2 | Python | Supply Stacks | 42.1386 ms |
| 2023 | 1 | 1 | C# | Trebuchet?! | 1.3484 ms |
| 2023 | 1 | 2 | C# | Trebuchet?! | 22.6638 ms |
| 2023 | 2 | 1 | C# | Cube Conundrum | 0.5611 ms |
| 2023 | 2 | 2 | C# | Cube Conundrum | 0.5455 ms |
| 2023 | 3 | 1 | C# | Gear Ratios | 2.3812 ms |
| 2023 | 3 | 2 | C# | Gear Ratios | 1.6868 ms |
| 2023 | 4 | 1 | C# | Scratchcards | 7.1403 ms |
| 2023 | 4 | 2 | C# | Scratchcards | 2874.7331 ms |
| 2023 | 5 | 1 | C# | If You Give A Seed A Fertilizer | 3.0246 ms |
| 2023 | 5 | 2 | C# | If You Give A Seed A Fertilizer | 3.6902 ms |
| 2024 | 1 | 1 | C# | Historian Hysteria | 3.7822 ms |
| 2024 | 1 | 2 | C# | Historian Hysteria | 46.6393 ms |
| 2024 | 2 | 1 | C# | Red-Nosed Reports | 1.7530 ms |
| 2024 | 2 | 2 | C# | Red-Nosed Reports | 2.2269 ms |
| 2024 | 3 | 1 | C# | Mull It Over | 6.8635 ms |
| 2024 | 3 | 2 | C# | Mull It Over | 1.4691 ms |
| 2024 | 4 | 1 | C# | Ceres Search | 15.0647 ms |
| 2024 | 4 | 2 | C# | Ceres Search | 1.1298 ms |
| 2024 | 5 | 1 | C# | Print Queue | 3.5005 ms |
| 2024 | 5 | 2 | C# | Print Queue | 3.9561 ms |
| 2024 | 6 | 1 | C# | Guard Gallivant | 1.6007 ms |
| 2024 | 6 | 2 | C# | Guard Gallivant | 474.9609 ms |
| 2024 | 7 | 1 | C# | Bridge Repair | 36.9314 ms |
| 2024 | 7 | 2 | C# | Bridge Repair | 727.9609 ms |
| 2024 | 8 | 1 | C# | Resonant Collinearity | 6.4806 ms |
| 2024 | 8 | 2 | C# | Resonant Collinearity | 1.4729 ms |
| 2024 | 9 | 1 | C# | Disk Fragmenter | 21.2277 ms |
| 2024 | 9 | 2 | C# | Disk Fragmenter | 914.4541 ms |
| 2024 | 10 | 1 | C# | Hoof It | 4.4282 ms |
| 2024 | 10 | 2 | C# | Hoof It | 2.4722 ms |
| 2024 | 11 | 1 | C# | Plutonian Pebbles | 32.2024 ms |
| 2024 | 11 | 2 | C# | Plutonian Pebbles | 45.1449 ms |
| 2024 | 12 | 1 | C# | Garden Groups | 3.6457 ms |
| 2024 | 12 | 2 | C# | Garden Groups | 88.8202 ms |
| 2024 | 13 | 1 | C# | Claw Contraption | 3.9651 ms |
| 2024 | 13 | 2 | C# | Claw Contraption | 2.0947 ms |
| 2024 | 14 | 1 | C# | Restroom Redoubt | 2.2801 ms |
| 2024 | 14 | 2 | C# | Restroom Redoubt | 28.6742 ms |
| 2024 | 15 | 1 | C# | Warehouse Woes | 7.3185 ms |
| 2024 | 15 | 2 | C# | Warehouse Woes | 59.4211 ms |
| 2024 | 16 | 1 | C# | Reindeer Maze | 825.9448 ms |
| 2024 | 16 | 2 | C# | Reindeer Maze | 923.0020 ms |
| 2024 | 17 | 1 | C# | Chronospatial Computer | 11.6523 ms |
| 2024 | 17 | 2 | C# | Chronospatial Computer | 0.4257 ms |
| 2024 | 18 | 1 | C# | RAM Run | 6.2949 ms |
| 2024 | 18 | 2 | C# | RAM Run | 6.1500 ms |
| 2024 | 19 | 1 | C# | Linen Layout | 80.2653 ms |
| 2024 | 19 | 2 | C# | Linen Layout | 197.9855 ms |
| 2024 | 20 | 1 | C# | Race Condition | 13.5933 ms |
| 2024 | 20 | 2 | C# | Race Condition | 1007.2571 ms |
| 2024 | 21 | 1 | C# | Keypad Conundrum | 9.4304 ms |
| 2024 | 21 | 2 | C# | Keypad Conundrum | 0.8175 ms |
| 2024 | 22 | 1 | C# | Monkey Market | 144.0835 ms |
| 2024 | 22 | 2 | C# | Monkey Market | 144.4016 ms |
| 2024 | 23 | 1 | C# | LAN Party | 505.0145 ms |
| 2024 | 23 | 2 | C# | LAN Party | 22.6544 ms |
| 2024 | 24 | 1 | C# | Crossed Wires | 12.6169 ms |
| 2024 | 25 | 1 | C# | Code Chronicle | 3.5511 ms |
| 2025 | 1 | 1 | Python | Secret Entrance | 26.0608 ms |
| 2025 | 1 | 2 | Python | Secret Entrance | 27.6580 ms |
