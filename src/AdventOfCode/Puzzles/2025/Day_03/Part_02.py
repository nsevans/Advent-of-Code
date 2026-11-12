#####
# Year 2025, Day 03, Part 02
#

import sys
sys.path += './AdventOfCode/Common/Python'
from Common.Python.utils.PuzzleContext import PuzzleContext
from Common.Python.utils import SolveWrapper as solver

def prepare_data(context: PuzzleContext) -> list[list[str]]:
    with open(context.input_file) as file:
        return file.read().split("\n")

def get_result(input: list[list[str]]) -> str:
    total = 0
    for battery_bank in input:
        largest_value = ''
        for i in range(12 - 1, -1, -1):
            # get the index of the largest digit from a sliding variable width window of values in the bank
            # the window width is determined by how many digits left there are to find (i) and the remaining
            # digits in the battery_bank
            # ex: if the battery_bank has 100 digits remaining and there are 10 (i = 9) digits left the find,
            # this will find the largest digit in the first 91 digits of the battery_bank, delete all up to the
            # one it found, then repeat for the next digit
            index_of_best = battery_bank.index(max(battery_bank[:len(battery_bank) - i]))

            largest_value += battery_bank[index_of_best]
            # remove the start of the bank up to and including the selected largest value
            battery_bank = battery_bank[index_of_best + 1:]
        total += int(largest_value)
    return total

if __name__ == '__main__':
    solver.run(prepare_data, get_result, sys.argv)
