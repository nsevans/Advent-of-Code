#####
# Year 2022, Day 04, Part 02
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
        if p[0] <= p[2] and p[1] >= p[2]:
            redundencies += 1
        elif p[2] <= p[0] and p[3] >= p[0]:
            redundencies += 1
    return redundencies

def intersects(a1: int, a2: int, b1: int, b2: int) -> bool:
    if a1 <= b1 and a2 >= b1:
        return True
    if b1 <= a1 and b2 >= a1:
        return True

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(get_result(input_values))
