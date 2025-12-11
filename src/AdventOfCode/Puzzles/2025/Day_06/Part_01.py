#####
# Year 2025, Day 06, Part 01
#

import sys
sys.path += './AdventOfCode/Common/Python'
import Common.Python.utils.solve_wrapper as solver

def prepare_data(input_file: str) -> list[list[str]]:
    with open(input_file) as file:
        input_values = file.read()
        return [[i for i in line.split(' ') if i != ''] for line in input_values.split('\n')]

def get_result(input: list[list[str]]) -> str:
    sum = 0
    for col in range(0, len(input[0])):
        operand = input[len(input) - 1][col]
        result = 0 if operand == '+' else 1
        for row in range(len(input) - 2, -1, -1):
            if operand == '+':
                result += int(input[row][col])
            elif operand == '*':
                result *= int(input[row][col])
        sum += result
    return sum

if __name__ == '__main__':
    solver.run(sys.argv[1], prepare_data, get_result)
