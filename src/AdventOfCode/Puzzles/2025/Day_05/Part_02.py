#####
# Year 2025, Day 05, Part 02
#

import sys
sys.path += './AdventOfCode/Common/Python'
import Common.Python.utils.solve_wrapper as solver

def prepare_data(input_file: str) -> list[list[int]]:
    with open(input_file) as file:
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
    solver.run(sys.argv[1], prepare_data, get_result)
