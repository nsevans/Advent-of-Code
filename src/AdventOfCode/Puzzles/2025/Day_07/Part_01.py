#####
# Year 2025, Day 07, Part 01
#

import sys

def prepare_data(input_file: str) -> list[list[chr]]:
    with open(input_file) as file:
        input_values = file.read().split()
        return [[c for c in line] for line in input_values]

def get_result(input: list[list[chr]]) -> str:
    splits = 0
    beam_cols = [input[0].index('S')]

    for row in range(2, len(input), 2):
        for col in range(1, len(input[row]) - 1):

            if col in beam_cols and input[row][col] == '^':
                beam_cols.remove(col)

                if col + 1 not in beam_cols:
                    beam_cols.append(col + 1)

                if col - 1 not in beam_cols:
                    beam_cols.append(col - 1)

                splits += 1

    return splits

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(get_result(input_values))
