#####
# Year 2025, Day 05, Part 02
#

import sys
sys.path += './AdventOfCode/Common/Python'
from Common.Python.utils.PuzzleContext import PuzzleContext
from Common.Python.utils import SolveWrapper as solver

def prepare_data(context: PuzzleContext) -> list[list[int]]:
    with open(context.input_file) as file:
        input_values = file.read().split('\n')
        ranges = [[int(i) for i in line.split('-')] for line in input_values if '-' in line]
        ranges.sort()
        return ranges

def get_result(input: list[list[int]]) -> str:
    fresh_count = 0
    highest_id = 0

    for i in range(0, len(input)):

        if highest_id >= input[i][1]:
            continue

        fresh_count += (input[i][1] - max(input[i][0], highest_id + 1)) + 1
        highest_id = max(highest_id + 1, input[i][1])

    return fresh_count

if __name__ == '__main__':
    solver.run(prepare_data, get_result, sys.argv)
