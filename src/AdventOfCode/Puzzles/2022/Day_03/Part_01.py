#####
# Year 2022, Day 03, Part 01
#

import sys
sys.path += './AdventOfCode/Common/Python'
import Common.Python.utils.solve_wrapper as solver

priority = [x for x in ' abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ']

def prepare_data(input_file: str) -> list[(set[str], set[str])]:
    with open(input_file) as file:
        input_values = file.read()
        return [(set(x[:int(len(x)/2)]), set(x[int(len(x)/2):])) for x in input_values.split("\n")]

def get_result(input: list[(set[str], set[str])]) -> str:
    # Get the intersection of each group of strings (there should only ever be 1), then sum the index
    # of the value in the priority list
    total_priority = sum([priority.index(list(l[0] & l[1])[0]) for l in input])
    return total_priority

if __name__ == '__main__':
    solver.run(sys.argv[1], prepare_data, get_result)
