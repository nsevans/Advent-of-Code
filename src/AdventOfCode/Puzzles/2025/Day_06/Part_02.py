#####
# Year 2025, Day 06, Part 02
#

import sys

def prepare_data(input_file: str) -> list[list[str]]:
    with open(input_file) as file:
        input_values = file.read().split("\n")

        # update each row to be the same width (pad left with spaces)
        max_width = len(max(input_values, key=len))
        for i in range(0, len(input_values)):
            input_values[i] = input_values[i].ljust(max_width)

        # get list of operands
        operands = [op.strip() for op in input_values[-1].split()]

        # rotate matrix to make each column a string
        equations = [''.join(list(row)).strip() for row in zip(*input_values[:-1])]

        return [equations, operands]

def get_result(input: list[list[str]]) -> str:
    sum = 0

    # keep track of the current operand
    op_index = 0
    current_op = input[1][op_index]

    current_result = 0 if current_op == '+' else 1

    for num in input[0]:
        if num == '':
            op_index += 1
            current_op = input[1][op_index]
            sum += current_result
            current_result = 0 if current_op == '+' else 1
            continue

        if current_op == '+':
            current_result += int(num)
        else:
            current_result *= int(num)

    sum += current_result
    return sum

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(get_result(input_values))
