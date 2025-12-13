#####
# Year 2025, Day 12, Part 01
#

import sys
sys.path += './AdventOfCode/Common/Python'
import Common.Python.utils.solve_wrapper as solver

def prepare_data(input_file: str) -> tuple[tuple[int], list[tuple[tuple[int]], int]]:
    with open(input_file) as file:
        input_values = file.read().split('\n')

        # assuming all presents are the same sized, look at the width and height of
        # the first one, then skip all the rest
        present_width = len(input_values[1])
        present_height = 0
        for i in range(1, len(input_values)):
            if input_values[i] == '':
                break
            present_height += 1

        # get the index of the last empty string, which is the index before the tree
        # input starts
        tree_start_index = len(input_values) - 1 - input_values[::-1].index('') + 1
        trees = []
        for j in range(tree_start_index, len(input_values)):
            tree_data = input_values[j].split(':')
            size = tuple(int(s) for s in tree_data[0].split('x'))
            amount = sum([int(a) for a in tree_data[1].split()])
            trees.append((size, amount))

        return ((present_width, present_height), trees)

def get_result(input: tuple[tuple[int], list[tuple[tuple[int]], int]]) -> str:
    count = 0
    present_size = input[0]
    # the example input is misleading by making it seem that rotating and fitting the presents together is
    # required to solve the real puzzle input. But, the real puzzle input can be solved by simply counting
    # the number of present sized spaces that are under the tree. If the number of presents needed to be put
    # under the tree are less than or equal to the number of available present sized spaces, then we can count
    # it as a valid configuration.
    for tree in input[1]:
        tree_size = tree[0]
        present_count = tree[1]
        # calculate the max number of presents that can be placed under the tree
        available_spots = (tree_size[0] // present_size[0]) * (tree_size[1] // present_size[1])
        if present_count <= available_spots:
            count += 1

    return count

if __name__ == '__main__':
    solver.run(sys.argv[1], prepare_data, get_result)
