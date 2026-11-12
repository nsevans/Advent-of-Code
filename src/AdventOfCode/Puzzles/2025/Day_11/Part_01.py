#####
# Year 2025, Day 11, Part 01
#

import sys
sys.path += './AdventOfCode/Common/Python'
from Common.Python.utils.PuzzleContext import PuzzleContext
from Common.Python.utils import SolveWrapper as solver

def prepare_data(context: PuzzleContext) -> dict[str, set[str]]:
    input = dict()
    for machine, *links in map(str.split, open(context.input_file)):
        input[machine[:-1]] = set(links)
    return input

def get_result(input: dict[str, set[str]]) -> str:
    path_count = 0
    start = input['you']
    end = 'out'

    for link in start:
        path_count += traverse(input, link, end)
    return path_count


def traverse(input, machine, end):
    if machine == end:
        return 1

    path_count = 0
    for link in input[machine]:
        path_count += traverse(input, link, end)

    return path_count

if __name__ == '__main__':
    solver.run(prepare_data, get_result, sys.argv)
