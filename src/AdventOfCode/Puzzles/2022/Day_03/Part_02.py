#####
# Year 2022, Day 03, Part 02
#

import sys

priority = [x for x in ' abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ']

def prepareData(inputFile: str) -> list[set[str]]:
    with open(inputFile) as file:
        inputValues = file.read()
        splitValues = inputValues.split('\n')
        return [[set(x) for x in splitValues[i:i+3]] for i in range(0, len(splitValues), 3)]

def getResult(input: list[set[str]]) -> str:
    # Get the intersection of each group of 3 strings (should only ever be 1 value), then sum the index
    # of the found value using the priority list where the index is it's priority value
    totalPriority = sum([priority.index(list(g[0] & g[1] & g[2])[0]) for g in input])
    return totalPriority

if __name__ == '__main__':
    inputFile = sys.argv[1]
    inputValues = prepareData(inputFile)
    print(getResult(inputValues))
