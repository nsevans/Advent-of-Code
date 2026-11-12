#####
# Year 2022, Day 03, Part 01
#

import sys
sys.path += './AdventOfCode/Common/Python'
from Common.Python.utils.PuzzleContext import PuzzleContext
from Common.Python.utils import SolveWrapper as solver

priority = [x for x in ' abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ']

def prepare_data(context: PuzzleContext) -> list[(set[str], set[str])]:
    with open(context.input_file) as file:
        input_values = file.read()
        return [(set(x[:int(len(x)/2)]), set(x[int(len(x)/2):])) for x in input_values.split("\n")]

def get_result(input: list[(set[str], set[str])]) -> str:
    # Get the intersection of each group of strings (there should only ever be 1), then sum the index
    # of the value in the priority list
    total_priority = sum([priority.index(list(l[0] & l[1])[0]) for l in input])
    return total_priority

if __name__ == '__main__':
    solver.run(prepare_data, get_result, sys.argv)
