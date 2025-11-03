#####
# Year 2022, Day 03, Part 01
#

import sys

priority = [x for x in ' abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ']

def prepareData(inputFile: str) -> list[(set[str], set[str])]:
    with open(inputFile) as file:
        inputValues = file.read()
        return [(set(x[:int(len(x)/2)]), set(x[int(len(x)/2):])) for x in inputValues.split("\n")]

def getResult(input: list[(set[str], set[str])]) -> str:
    # Get the intersection of each group of strings (there should only ever be 1), then sum the index
    # of the value in the priority list
    totalPriority = sum([priority.index(list(l[0] & l[1])[0]) for l in input])
    return totalPriority

if __name__ == '__main__':
    inputFile = sys.argv[1]
    inputValues = prepareData(inputFile)
    print(getResult(inputValues))
