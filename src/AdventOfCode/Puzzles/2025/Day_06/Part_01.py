#####
# Year 2025, Day 06, Part 01
#

import sys
sys.path += './AdventOfCode/Common/Python'
from Common.Python.utils.PuzzleContext import PuzzleContext
from Common.Python.utils import SolveWrapper as solver

def prepare_data(context: PuzzleContext) -> list[list[str]]:
    with open(context.input_file) as file:
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
    solver.run(prepare_data, get_result, sys.argv)
