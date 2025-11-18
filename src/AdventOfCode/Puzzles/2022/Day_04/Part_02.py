#####
# Year 2022, Day 04, Part 02
#

import sys
import re

def prepareData(inputFile: str) -> list[list[int]]:
    with open(inputFile) as file:
        inputValues = file.read()
        return [list(int(i) for i in re.split(r'[,-]', l)) for l in inputValues.split('\n')]

def getResult(input: list[list[int]]) -> int:
    redundencies = 0
    for p in input:
        if p[0] <= p[2] and p[1] >= p[2]:
            redundencies += 1
        elif p[2] <= p[0] and p[3] >= p[0]:
            redundencies += 1
    return redundencies

def intersects(a1: int, a2: int, b1: int, b2: int) -> bool:
    if a1 <= b1 and a2 >= b1:
        return True
    if b1 <= a1 and b2 >= a1:
        return True

if __name__ == '__main__':
    inputFile = sys.argv[1]
    inputValues = prepareData(inputFile)
    print(getResult(inputValues))
