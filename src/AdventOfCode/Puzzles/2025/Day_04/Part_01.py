#####
# Year 2025, Day 04, Part 01
#

import sys
sys.path += './AdventOfCode/Common/Python'
import Common.Python.utils.solve_wrapper as solver

def prepare_data(input_file: str) -> list[list[str]]:
    with open(input_file) as file:
        input_values = file.read().split('\n')
        return [[i for i in line] for line in input_values]

def get_result(input: list[list[str]]) -> str:
    directions = [[0,-1],[1,-1],[1,0],[1,1],[0,1],[-1,1],[-1,0],[-1,-1]]
    adjacent_limit = 4
    total_removed = 0

    for y in range(0, len(input)):
        for x in range(0, len(input[y])):

            if input[y][x] != '@':
                continue

            adjacents_found = 0
            for dir in directions:
                ny = y + dir[0]
                nx = x + dir[1]

                if ny < 0 or nx < 0 or ny >= len(input) or nx >= len(input[y]):
                    continue

                if input[ny][nx] == '@':
                    adjacents_found += 1

                    # break out early if number of adjacent rolls has met the limit
                    if adjacents_found >= adjacent_limit:
                        break

            if adjacents_found < adjacent_limit:
                total_removed += 1

    return total_removed

if __name__ == '__main__':
    solver.run(sys.argv[1], prepare_data, get_result)
