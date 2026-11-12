#####
# Year 2022, Day 04, Part 01
#

import sys
sys.path += './AdventOfCode/Common/Python'
from Common.Python.utils.PuzzleContext import PuzzleContext
from Common.Python.utils import SolveWrapper as solver
import re

def prepare_data(context: PuzzleContext) -> list[list[int]]:
    with open(context.input_file) as file:
        input_values = file.read()
        return [list(int(i) for i in re.split(r'[,-]', l)) for l in input_values.split('\n')]

def get_result(input: list[list[int]]) -> int:
    redundencies = 0
    for p in input:
        if p[0] <= p[2] and p[1] >= p[2] and p[0] <= p[3] and p[1] >= p[3]:
            redundencies += 1
        elif p[2] <= p[0] and p[3] >= p[0] and p[2] <= p[1] and p[3] >= p[1]:
            redundencies += 1
    return redundencies

if __name__ == '__main__':
    solver.run(prepare_data, get_result, sys.argv)
