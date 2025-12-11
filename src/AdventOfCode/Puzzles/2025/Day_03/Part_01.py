#####
# Year 2025, Day 03, Part 01
#

import sys
sys.path += './AdventOfCode/Common/Python'
import Common.Python.utils.solve_wrapper as solver

def prepare_data(input_file: str) -> list[list[int]]:
    with open(input_file) as file:
        input_values = file.read()
        return [[int(b) for b in line] for line in input_values.split('\n')]

def get_result(input: list[list[int]]) -> str:
    total = 0
    for bank in input:
        battery_1, battery_1_index, battery_2 = 0, 0, 0

        # loop over the battery_bank (to the second last index) to find the largest digit
        for index in range(0, len(bank)-1):
            if bank[index] > battery_1:
                battery_1 = bank[index]
                battery_1_index = index

        # start where first the loop finished, then loop to find the next digit that is the largest
        for index in range(battery_1_index + 1, len(bank)):
            if bank[index] > battery_2:
                battery_2 = bank[index]

        # first digit is a tens digit so need to multiply by 10
        total += (battery_1 * 10) + battery_2
    return total



if __name__ == '__main__':
    solver.run(sys.argv[1], prepare_data, get_result)
