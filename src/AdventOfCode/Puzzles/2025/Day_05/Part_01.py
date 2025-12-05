#####
# Year 2025, Day 05, Part 01
#

import sys

def prepare_data(input_file: str) -> list[list[int]]:
    with open(input_file) as file:
        input_values = file.read().split('\n')
        return [[int(i) for i in line.split('-') if i != ''] for line in input_values]

def get_result(input: list[list[int]]) -> str:
    fresh_count = 0

    split_index = input.index([])
    ranges = input[:split_index]
    ranges.sort()

    # flatten ingredients list
    ingredients = [i for l in input[split_index + 1:] for i in l]
    ingredients.sort()

    r_index = 0
    i_index = 0
    while i_index < len(ingredients) and r_index < len(ranges):
        ingredient = ingredients[i_index]
        lower_range = ranges[r_index][0]
        upper_range = ranges[r_index][1]

        if ingredient >= lower_range and ingredients[i_index] <= upper_range:
            fresh_count += 1
            i_index += 1
        elif ingredient < lower_range:
            i_index += 1
        elif ingredient > upper_range:
            r_index += 1

    return fresh_count
# 342213833999

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(get_result(input_values))
