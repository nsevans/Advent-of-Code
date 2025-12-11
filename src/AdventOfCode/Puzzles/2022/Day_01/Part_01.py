#####
# Year 2022, Day 01, Part 01
#

import sys
sys.path += './AdventOfCode/Common/Python'
import Common.Python.utils.solve_wrapper as solver

def prepare_data(input_file: str) -> list[int]:
    with open(input_file) as file:
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
    solver.run(sys.argv[1], prepare_data, get_result)
