# Advent Of Code Attempts

This project is for all of my attempts at the Advent of Code puzzles found [here](https://adventofcode.com) and the tools I've built alongside the attempts to help with solving future puzzles.

## Usage

### Running Solvers

```Powershell
dotnet run -- --solve [--year <int>] [--day <int>] [--part <int>] [--verbose] [--test]
```

|  Flag |  Example |  Accepted Values/Types | Required | Description |
|-------|----------|------------------------|----------|-------------|
| `--year` | `--year 2024` | `int` | No | Execute all puzzle solvers for the given year. |
| `--day` | `--day 1` | `int`, 1 to 25 | No |  Execute all puzzle solvers for the given day. |
| `--part` | `--part 1` | `int`, 1 or 2 | No |  Execute all puzzle solvers for the given part. |
| `--verbose` | `--verbose` | N/A | No |  Display more detailed information about each puzzle's results. |

The above flags can be combined to execute a more refined subset of puzzle solvers:

* Execute both parts of day 9 from the 2024 advent calendar:  
`dotnet run -- --year 2024 --day 9`
* Execute part 1 of day 9 from the 2024 advent calendar:  
`dotnet run -- --year 2024 --day 9 --part 1`
* Execute all part 1s of day 10 for each advent calendar year:  
`dotnet run -- --day 10 --part 1`
* Execute all puzzles from the 2023 advent calendar using the test examples:  
`dotnet run -- --year 2023 --test`

### Running Generator

```Powershell
dotnet run -- [--generate] [--year <int>] [--day <int>]
```

|  Flag |  Example |  Accepted Values/Types | Required | Description |
|-------|----------|------------------------|----------|-------------|
| `--year` | `--year 2024` | `int` | Yes | Set the year that the generator is generating the classes for. |
| `--day` | `--day 1` | `int`, 1 to 25 | Yes | Set the day that the generator is generating the classes for. |

## Solver

Each puzzle solver is split up by year, day, and part and can all be found within /src/AdventOfCode/Puzzles.

The inputs to the corresponding puzzle can be found within /src/AdventOfCode/Inputs where each input file is denoted by the year and day (ex: `input_2024_09.in`). The inputs are **not** automatically downloaded from the AoC website and should be copied to the Inputs folder.

## Generator

The generator makes it easier to set up the base classes for each new Advent of Code puzzle. All it requires is the **year** and **day** and it can generate an almost complete base class. The only things required after the generation is complete is for the `Title` and the `Example/Test Input:` to be filled out.
