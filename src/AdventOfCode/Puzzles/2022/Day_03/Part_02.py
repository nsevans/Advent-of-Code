#####
# Year 2022, Day 03, Part 02
#

import sys
sys.path += './AdventOfCode/Common/Python'
import Common.Python.utils.solve_wrapper as solver

priority = [x for x in ' abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ']

def prepare_data(input_file: str) -> list[set[str]]:
    with open(input_file) as file:
        input_values = file.read()
        split_values = input_values.split('\n')
        return [[set(x) for x in split_values[i:i+3]] for i in range(0, len(split_values), 3)]

def get_result(input: list[set[str]]) -> str:
    # Get the intersection of each group of 3 strings (should only ever be 1 value), then sum the index
    # of the found value using the priority list where the index is it's priority value
    total_priority = sum([priority.index(list(g[0] & g[1] & g[2])[0]) for g in input])
    return total_priority

if __name__ == '__main__':
    solver.run(sys.argv[1], prepare_data, get_result)
