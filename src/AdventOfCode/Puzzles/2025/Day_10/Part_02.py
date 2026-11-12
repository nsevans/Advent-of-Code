#####
# Year 2025, Day 10, Part 02
#

import sys
sys.path += './AdventOfCode/Common/Python'
from Common.Python.utils.PuzzleContext import PuzzleContext
from Common.Python.utils import SolveWrapper as solver
from scipy.optimize import linprog

def prepare_data(context: PuzzleContext) -> list[tuple[list]]:
    input = []
    for _, *button_wiring, joultage in map(str.split, open(context.input_file)):
        button_wiring = [eval(b[:-1]+',)') for b in button_wiring]
        joultage = eval(joultage[1:-1])
        input.append((button_wiring, joultage))
    return input

def get_result(input: list[tuple[list]]) -> str:
    total_presses = 0
    for i in range(0, len(input)):
        button_wiring = input[i][0]
        joultages = input[i][1]
        possible_combos = range(len(joultages))

        c = [1 for _ in button_wiring]
        A = [[c in b for b in button_wiring] for c in possible_combos]
        total_presses += linprog(c, A_eq=A, b_eq=joultages, integrality=1).fun

    return int(total_presses)

if __name__ == '__main__':
    solver.run(prepare_data, get_result, sys.argv)