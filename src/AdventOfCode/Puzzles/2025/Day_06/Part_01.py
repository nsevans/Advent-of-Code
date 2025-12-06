#####
# Year 2025, Day 06, Part 01
#

import sys

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
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(input_values)
    print(get_result(input_values))
