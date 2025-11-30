# Part 02

import sys

def parse_data(input_file: str) -> list[int]:
    with open(input_file) as file:
        input_values = file.read()
        # Convert to list of ints and replace all empty lines with a 0
        return list(map(int, input_values.replace('\n\n', '\n0\n').split('\n')))

def get_result(input: list[int]) -> str:
    most_calories = [0, 0, 0]
    current_calories = 0
    for i in input:
        current_calories += i
        if i == 0:
            # Find the first index in mostCalories that is less than currentCalories
            index = next((i for i, m in enumerate(most_calories) if m < current_calories), None)
            if index != None:
                most_calories[index] = current_calories
                # Sort so that the smallest values are at the beginning of the list
                most_calories.sort()
            current_calories = 0

    return sum(most_calories)

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = parse_data(input_file)
    print(get_result(input_values))
