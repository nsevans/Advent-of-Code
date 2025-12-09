#####
# Year 2025, Day 08, Part 01
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
    max_connections = 1000
    # create list of each possible combination of junction box sorted by the distance
    junction_box_pairs = sorted(itertools.combinations(input, 2), key=lambda c: math.dist(*c))
    for index, (junction_box1, junction_box2) in enumerate(junction_box_pairs):
        for coord in input:
            if junction_box1 in input[coord]:
                coord1 = coord
            if junction_box2 in input[coord]:
                coord2 = coord

        if coord1 != coord2:
             # union the sets and delete one of them
            input[coord1] |= input[coord2]
            del input[coord2]

        if index + 1 == max_connections:
            sorted_circuits = sorted(len(input[i]) for i in input)
            # get the product of the sizes of the largest 3 circuts
            return math.prod(sorted_circuits[-3:])

    return None

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(get_result(input_values))
