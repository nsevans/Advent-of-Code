#####
# Year 2025, Day 09, Part 02
#

import sys
sys.path += './AdventOfCode/Common/Python'
from Common.Python.utils.PuzzleContext import PuzzleContext
from Common.Python.utils import SolveWrapper as solver
from itertools import combinations, pairwise

def prepare_data(context: PuzzleContext) -> list[tuple]:
    return list(map(eval, open(context.input_file)))

def get_result(input: list[tuple[int]]) -> str:
    # get a list of all edges aka green boundry lines, sorted by largest to smallest length
    green_lines = [p for p in pairwise(input + [input[0]])]
    green_lines = sorted([(min(a,c), min(b,d), max(a,c), max(b,d)) for (a,b),(c,d) in green_lines], key=lambda x: get_area(*x), reverse=True)

    # get a list of all possible rectangles, sorted by largest to smallest area
    rect_pairs = [c for c in combinations(input, r=2)]
    rect_pairs = sorted([(min(a,c), min(b,d), max(a,c), max(b,d)) for (a,b),(c,d) in rect_pairs], key=lambda x: get_area(*x), reverse=True)

    # compare the rectangles against all lines
    for rx1, ry1, rx2, ry2 in rect_pairs:
        for lx1, ly1, lx2, ly2 in green_lines:
            # break if one is found that is within the bounds of the lines
            if lx1 < rx2 and ly1 < ry2 and lx2 > rx1 and ly2 > ry1:
                break
        else:
            return get_area(rx1,ry1,rx2,ry2)

    return None

def get_area(x1: int, y1: int, x2: int, y2: int):
    return (x2 - x1 + 1) * (y2 - y1 + 1)

if __name__ == '__main__':
    solver.run(prepare_data, get_result, sys.argv)
