#####
# Year 2025, Day 10, Part 01
#

import sys
from itertools import combinations
from scipy.optimize import linprog

def prepare_data(input_file: str) -> list[tuple[list]]:
    input = []
    for _, *button_wiring, joultage in map(str.split, open(input_file)):
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
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(get_result(input_values))
