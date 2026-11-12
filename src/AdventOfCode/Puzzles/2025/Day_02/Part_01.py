#####
# Year 2025, Day 02, Part 01
#

import sys
sys.path += './AdventOfCode/Common/Python'
from Common.Python.utils.PuzzleContext import PuzzleContext
from Common.Python.utils import SolveWrapper as solver
import math

def prepare_data(context: PuzzleContext) -> list[list[int]]:
    with open(context.input_file) as file:
        input_values = file.read().split(',')
        return [[int(i) for i in r.split('-')] for r in input_values ]

def get_result(input: list[list[int]]) -> str:
    dupe_sum = 0

    for r in input:
        # odd lengthed numbers, can't have sequence of digits repeated twice
        if (int(math.log10(r[0])) + 1) % 2 != 0 and (int(math.log10(r[1])) + 1) %2 != 0:
            continue

        for v in range(r[0], r[1] + 1):
            v_length = int(math.log10(v)) + 1
            # odd lengthed numbers, can't have sequence of digits repeated twice
            if v_length % 2 != 0:
                continue

            div = 10**(v_length // 2)
            # check that the first half of the value is equal to the second half
            if v % div == v // div:
                dupe_sum += v

    return dupe_sum

if __name__ == '__main__':
    solver.run(prepare_data, get_result, sys.argv)
