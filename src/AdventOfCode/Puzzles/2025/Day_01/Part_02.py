#####
# Year 2025, Day 01, Part 02
#

import sys
sys.path += './AdventOfCode/Common/Python'
from Common.Python.utils.PuzzleContext import PuzzleContext
from Common.Python.utils import SolveWrapper as solver
import math

def prepare_data(context: PuzzleContext) -> list[int]:
    with open(context.input_file) as file:
        input_values = file.read().split('\n')
        return [int(l[1:]) if l[0] == 'R' else -int(l[1:]) for l in input_values]

def get_result(input: list[int]) -> str:
    zero_count = 0
    dial_value = 50

    for amount in input:
        total_rotations = math.floor(abs(amount) / 100)
        amount = (-1 if amount < 0 else 1) * (abs(amount) % 100)

        new_dial_value = dial_value + amount

        if dial_value != 0 and (new_dial_value <= 0 or new_dial_value >= 100):
            zero_count += 1
        zero_count += total_rotations

        dial_value = new_dial_value % 100

    return zero_count

if __name__ == '__main__':
    solver.run(prepare_data, get_result, sys.argv)
