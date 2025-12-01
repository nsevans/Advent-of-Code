#####
# Year 2025, Day 01, Part 02
#

import sys
import math

def prepare_data(input_file: str) -> list[int]:
    with open(input_file) as file:
        input_values = file.read().split('\n')
        return [int(l[1:]) if l[0] == 'R' else -int(l[1:]) for l in input_values]

def get_result(input: list[int]) -> str:
    zero_count = 0
    dial_value = 50

    for amount in input:
        total_rotations = math.floor(abs(amount) / 100)
        amount = (-1 if amount < 0 else 1) * (abs(amount) % 100)

        new_dial_value = dial_value + amount

        if dial_value != 0 and (new_dial_value <= 0 or new_dial_value >= 100):
            zero_count += 1
        zero_count += total_rotations

        dial_value = new_dial_value % 100

    return zero_count

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(get_result(input_values))
