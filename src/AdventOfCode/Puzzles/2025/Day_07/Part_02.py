#####
# Year 2025, Day 07, Part 02
#

import sys

def prepare_data(input_file: str) -> list[list[chr]]:
    with open(input_file) as file:
        input_values = file.read().split()
        return [[c for c in line] for line in input_values]

def get_result(input: any) -> list[list[chr]]:
    beam_count = [0] * len(input)
    beam_count[input[0].index('S')] = 1

    for row in range(2, len(input), 2):
        for col in range(1, len(input[row]) - 1):

            if input[row][col] == '^':
                beam_count[col + 1] += beam_count[col]
                beam_count[col - 1] += beam_count[col]
                beam_count[col] = 0

    return sum(beam_count)

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(get_result(input_values))
