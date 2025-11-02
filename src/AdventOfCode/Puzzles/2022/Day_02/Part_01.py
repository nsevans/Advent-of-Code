#####
# Year 2022, Day 02, Part 01
#
# Outcomes:
# Lose = 0 points
# Tie = 3 points
# Win = 6 points
#
# Hands:
# Rock = A & X (1 point)
# Paper = B & Y (2 points)
# Scissors = C & Z (3 points)
#

import sys

# Possible outcomes for each player hand
# The hand it woudl win against, the hand it would lose against, and the number of points for playing that hand
outcomes = {
    'X': {'win': 'C', 'lose': 'B', 'points': 1},
    'Y': {'win': 'A', 'lose': 'C', 'points': 2},
    'Z': {'win': 'B', 'lose': 'A', 'points': 3}}

def prepareData(inputFile: str) -> list[(chr, chr)]:
    with open(inputFile) as file:
        inputValues = file.read()
        return inputValues.split('\n')

def getResult(input: list[(chr, chr)]) -> str:
    score = 0
    for i in input:
        opp = i[0]
        outcome = outcomes[i[-1]]

        score += outcome['points']
        # Win
        if (opp == outcome['win']):
            score += 6
        # Lose
        elif (opp == outcome['lose']):
            score += 0
        # Tie
        else:
            score += 3
    return score

if __name__ == '__main__':
    inputFile = sys.argv[1]
    inputValues = prepareData(inputFile)
    print(getResult(inputValues))
