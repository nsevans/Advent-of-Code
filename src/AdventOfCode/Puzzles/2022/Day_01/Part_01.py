# Part 01

import sys

def parseData(inputFile: str) -> list[int]:
    with open(inputFile) as file:
        inputValues = file.read()
        # Convert to list of ints and replace all empty lines with a 0
        return list(map(int, inputValues.replace('\n\n', '\n0\n').split('\n')))

def getResult(input: list[int]) -> str:
    mostCalories = 0
    currentCalories = 0
    for i in input:
        if i == 0:
            mostCalories = currentCalories if currentCalories > mostCalories else mostCalories
            currentCalories = 0
            continue
        currentCalories += i

    return mostCalories

if __name__ == '__main__':
    inputFile = sys.argv[1]
    inputValues = parseData(inputFile)
    print(getResult(inputValues))
