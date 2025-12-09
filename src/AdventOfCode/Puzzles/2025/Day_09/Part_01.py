#####
# Year 2025, Day 09, Part 01
#

import sys

def prepare_data(input_file: str) -> list[tuple]:
    with open(input_file) as file:
        input_values = file.read().split()
        return sorted([coord for coord in map(eval, input_values)])

def get_result(input: list[tuple]) -> str:
    largest_area = 0
    for i in range(0, len(input)):
        for j in range(len(input) - 1, i, -1):
            area = get_area(input[i], input[j])
            if area > largest_area:
                largest_area = area
    return largest_area

def get_area(a: tuple, b: tuple) -> int:
    return (abs(a[0] - b[0]) + 1) * (abs(a[1] - b[1]) + 1)

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    # print(input_values)
    print(get_result(input_values))
