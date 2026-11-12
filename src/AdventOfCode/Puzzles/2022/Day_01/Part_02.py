#####
# Year 2022, Day 01, Part 02
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
    most_calories = [0, 0, 0]
    current_calories = 0
    for index, calories in enumerate(input):
        current_calories += calories
        if calories == 0 or index == len(input)-1:
            # Find the first index in mostCalories that is less than currentCalories
            index = next((i for i, m in enumerate(most_calories) if m < current_calories), None)
            if index != None:
                most_calories[index] = current_calories
                # Sort so that the smallest values are at the beginning of the list
                most_calories.sort()
            current_calories = 0

    return sum(most_calories)

if __name__ == '__main__':
    solver.run(prepare_data, get_result, sys.argv)
