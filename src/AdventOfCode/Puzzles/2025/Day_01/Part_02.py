#####
# Year 2025, Day 01, Part 02
#

import sys
import math

def prepare_data(input_file: str) -> list[list]:
    with open(input_file) as file:
        input_values = file.read().split('\n')
        return [[l[0], int(l[1:])] for l in input_values]

def get_result(input: list[list]) -> str:
    zero_count = 0
    dial_value = 50

    for i in input:
        direction = i[0]
        amount = i[1]
        new_dial_value = dial_value

        total_rotations = math.floor(amount / 100)
        amount = amount % 100

        if direction == 'L':
            new_dial_value -= amount
        elif direction == 'R':
            new_dial_value += amount

        if dial_value != 0 and (new_dial_value <= 0 or new_dial_value >= 100):
            zero_count += 1
        zero_count += total_rotations

        dial_value = new_dial_value % 100

    return zero_count

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(get_result(input_values))
