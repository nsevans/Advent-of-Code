#####
# Year 2025, Day 01, Part 01
#

import sys

def prepare_data(input_file: str) -> list[int]:
    with open(input_file) as file:
        input_values = file.read().split('\n')
        return [int(l[1:]) if l[0] == 'R' else -int(l[1:]) for l in input_values]

def get_result(input: list[int]) -> str:
    zero_count = 0
    dial_value = 50

    for i in input:
        dial_value = (dial_value + i) % 100

        if dial_value == 0:
            zero_count += 1

    return zero_count

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(get_result(input_values))
