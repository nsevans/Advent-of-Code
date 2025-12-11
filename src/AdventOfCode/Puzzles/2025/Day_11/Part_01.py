#####
# Year 2025, Day 11, Part 01
#

import sys
sys.path += './AdventOfCode/Common/Python'
import Common.Python.utils.solve_wrapper as solver

def prepare_data(input_file: str) -> dict[str, set[str]]:
    input = dict()
    for machine, *links in map(str.split, open(input_file)):
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
    solver.run(sys.argv[1], prepare_data, get_result)
