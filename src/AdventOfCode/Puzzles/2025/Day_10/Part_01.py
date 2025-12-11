#####
# Year 2025, Day 10, Part 01
#

import sys
sys.path += './AdventOfCode/Common/Python'
import Common.Python.utils.solve_wrapper as solver
from itertools import combinations

def prepare_data(input_file: str) -> list[tuple[list]]:
    input = []
    for light_diagram, *button_wiring in map(str.split, open(input_file)):
        light_diagram = [l=='#' for l in light_diagram[1:-1]]
        button_wiring = [eval(b[:-1]+',)') for b in button_wiring[:-1]]
        input.append((light_diagram, button_wiring))
    return input

def get_result(input: list[tuple[list]]) -> str:
    total_presses = 0
    for i in range(0, len(input)):
        light_diagram = input[i][0]
        button_wiring = input[i][1]
        possible_combos = range(len(light_diagram))
        for p in possible_combos:
            found = False
            for pressed in combinations(button_wiring, p):
                attempted = [sum(c in pr for pr in pressed)%2 for c in possible_combos]
                if attempted == light_diagram:
                    total_presses += p
                    found = True
                    break
            if found:
                break

    return total_presses

if __name__ == '__main__':
    solver.run(sys.argv[1], prepare_data, get_result)
