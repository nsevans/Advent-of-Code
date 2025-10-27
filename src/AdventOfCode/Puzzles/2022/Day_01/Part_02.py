# Part 02

import sys

def parseData(inputFile: str) -> list[int]:
    with open(inputFile) as file:
        inputValues = file.read()
        # Convert to list of ints and replace all empty lines with a 0
        return list(map(int, inputValues.replace('\n\n', '\n0\n').split('\n')))

def getResult(input: list[int]) -> str:
    mostCalories = [0, 0, 0]
    currentCalories = 0
    for i in input:
        currentCalories += i
        if i == 0:
            # Find the first index in mostCalories that is less than currentCalories
            index = next((i for i, m in enumerate(mostCalories) if m < currentCalories), None)
            if index != None:
                mostCalories[index] = currentCalories
                # Sort so that the smallest values are at the beginning of the list
                mostCalories.sort()
            currentCalories = 0

    return sum(mostCalories)

if __name__ == '__main__':
    inputFile = sys.argv[1]
    inputValues = parseData(inputFile)
    print(getResult(inputValues))
