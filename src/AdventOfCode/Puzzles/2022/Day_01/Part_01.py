#####
# Year 2022, Day 01, Part 01
#

import sys
sys.path += './AdventOfCode/Common/Python'
from Common.Python.utils.PuzzleContext import PuzzleContext
from Common.Python.utils import SolveWrapper as solver

def prepare_data(context: PuzzleContext) -> list[int]:
    with open(context.input_file) as file:
        input_values = file.read()
        # Convert to list of ints and replace all empty lines with a 0
        return list(map(int, input_values.replace('\n\n', '\n0\n').split('\n')))

def get_result(input: list[int]) -> str:
    most_calories = 0
    current_calories = 0
    for i in input:
        if i == 0:
            most_calories = current_calories if current_calories > most_calories else most_calories
            current_calories = 0
            continue
        current_calories += i

    return most_calories

if __name__ == '__main__':
    solver.run(prepare_data, get_result, sys.argv)
