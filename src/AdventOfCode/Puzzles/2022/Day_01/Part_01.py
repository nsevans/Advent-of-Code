# Part 01

import sys

def parse_data(input_file: str) -> list[int]:
    with open(input_file) as file:
        input_values = file.read()
        # Convert to list of ints and replace all empty lines with a 0
        return list(map(int, input_values.replace('\n\n', '\n0\n').split('\n')))

def get_gesult(input: list[int]) -> str:
    most_calories = 0
    current_calories = 0
    for i in input:
        if i == 0:
            most_calories = current_calories if current_calories > most_calories else most_calories
            current_calories = 0
            continue
        current_calories += i

    return most_calories

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = parse_data(input_file)
    print(get_gesult(input_values))
