#####
# Year 2022, Day 02, Part 02
#
# Outcomes:
# Lose = X (0 points)
# Tie = Y (3 points)
# Win = Z (6 points)
#
# Hands:
# Rock = A (1 point)
# Paper = B (2 points)
# Scissors = C (3 points)
#

import sys

# The point outcomes of each hand played
# The first number is the points earned for the hand played, the second number
# is the points earned for either winning, losing, or tying the game.
outcomes = {
    'A': {'X': 3 + 0, 'Y': 1 + 3, 'Z': 2 + 6},
    'B': {'X': 1 + 0, 'Y': 2 + 3, 'Z': 3 + 6},
    'C': {'X': 2 + 0, 'Y': 3 + 3, 'Z': 1 + 6}
}

def prepare_data(input_file: str) -> list[(chr, chr)]:
    with open(input_file) as file:
        input_values = file.read()
        return input_values.split('\n')

def get_result(input: list[(chr, chr)]) -> str:
    score = 0
    for i in input:
        score += outcomes[i[0]][i[-1]]
    return score

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(get_result(input_values))
