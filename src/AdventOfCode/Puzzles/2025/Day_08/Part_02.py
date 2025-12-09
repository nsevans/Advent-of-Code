#####
# Year 2025, Day 08, Part 02
#

import sys
import math
import itertools

def prepare_data(input_file: str) -> dict[tuple, set[tuple]]:
    with open(input_file) as file:
        input_values = file.read().split()
        # create a dictionary with each coord as a key and each value being a set, initialized with the same coord
        return {coord: {coord} for coord in map(eval, input_values)}

def get_result(input: dict[tuple, set[tuple]]) -> str:
    # create list of each possible combination of junction box sorted by the distance
    junction_box_pairs = sorted(itertools.combinations(input, 2), key=lambda c: math.dist(*c))
    for (junction_box1, junction_box2) in junction_box_pairs:
        for key in input:
            if junction_box1 in input[key]:
                key1 = key
            if junction_box2 in input[key]:
                key2 = key

        if key1 != key2:
            # union the sets and delete one of them
            input[key1] |= input[key2]
            del input[key2]

        if len(input) == 1:
            return junction_box1[0] * junction_box2[0]

    return None


def get_distance(a: list[int], b: list[int]):
    return abs((a[0] - b[0]**2) + ((a[1] - b[1])**2) + ((a[2] - b[2])**2))

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(get_result(input_values))
