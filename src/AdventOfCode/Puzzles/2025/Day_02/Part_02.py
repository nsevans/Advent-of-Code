#####
# Year 2025, Day 02, Part 02
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
        for v in range(r[0], r[1] + 1):
            v_length = int(math.log10(v)) + 1
            for n in range(2, v_length + 1):
                # skip over any lengths that don't divide the value equaly
                if v_length % n != 0:
                    continue

                str_v = str(v)
                # check if a section of the string repeated n times matches the value
                if str_v[:v_length // n] * n == str_v:
                    dupe_sum += v
                    break

    return dupe_sum

if __name__ == '__main__':
    solver.run(prepare_data, get_result, sys.argv)
