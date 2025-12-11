#####
# Year 2025, Day 11, Part 02
#

import sys
sys.path += './AdventOfCode/Common/Python'
import Common.Python.utils.solve_wrapper as solver
from functools import cache
import math

def prepare_data(input_file: str) -> dict[str, list[str]]:
    input = dict()
    for machine, *links in map(str.split, open(input_file)):
        input[machine[:-1]] = links
    input['out'] = []
    return input

def get_result(input: dict[str, list[str]]) -> str:
    # count paths in chunks from one point to another
    checkpoints = [
        ('svr', 'dac'), ('dac', 'fft'), ('fft', 'out'),
        ('svr', 'fft'), ('fft', 'dac'), ('dac', 'out')]
    path_counts = [0] * len(checkpoints)

    for i, (start_machine, destination_machine) in enumerate(checkpoints):
        path_counts[i] = count_paths(start_machine, destination_machine, input)

    # The number of paths from point a to b to c is the same as paths from point a to b multiplied
    # by paths from point b to c
    return math.prod(path_counts[:3]) + math.prod(path_counts[3:])

def count_paths(current_machine: str, destination_machine: str, input: dict[str, set[str]]):

    @cache
    def count_paths_cached(current_machine: str, destination_machine: str):
        if current_machine == destination_machine:
            return 1
        else:
            paths = 0
            for linked_machine in input[current_machine]:
                paths += count_paths_cached(linked_machine, destination_machine)
            return paths

    return count_paths_cached(current_machine, destination_machine)


if __name__ == '__main__':
    solver.run(sys.argv[1], prepare_data, get_result)
