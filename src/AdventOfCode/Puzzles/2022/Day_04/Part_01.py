#####
# Year 2022, Day 04, Part 01
#

import sys
import re

def prepare_data(input_file: str) -> list[list[int]]:
    with open(input_file) as file:
        input_values = file.read()
        return [list(int(i) for i in re.split(r'[,-]', l)) for l in input_values.split('\n')]

def get_result(input: list[list[int]]) -> int:
    redundencies = 0
    for p in input:
        if p[0] <= p[2] and p[1] >= p[2] and p[0] <= p[3] and p[1] >= p[3]:
            redundencies += 1
        elif p[2] <= p[0] and p[3] >= p[0] and p[2] <= p[1] and p[3] >= p[1]:
            redundencies += 1
    return redundencies

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(get_result(input_values))
