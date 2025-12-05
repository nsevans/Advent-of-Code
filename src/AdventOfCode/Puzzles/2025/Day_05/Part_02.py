#####
# Year 2025, Day 05, Part 02
#

import sys

def prepare_data(input_file: str) -> list[list[int]]:
    with open(input_file) as file:
        input_values = file.read().split('\n')
        ranges = [[int(i) for i in line.split('-')] for line in input_values if '-' in line]
        ranges.sort()
        return ranges

def get_result(input: list[list[int]]) -> str:
    fresh_count = 0
    highest_id = 0

    for i in range(0, len(input)):

        if highest_id >= input[i][1]:
            continue

        fresh_count += (input[i][1] - max(input[i][0], highest_id + 1)) + 1
        highest_id = max(highest_id + 1, input[i][1])

    return fresh_count

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(get_result(input_values))
