#####
# Year 2022, Day 05, Part 01
#

import sys

def prepare_data(input_file: str) -> tuple[list[list[str]], list[list[int]]]:
    with open(input_file) as file:
        input_values = file.read().split("\n\n")

        stacks = [[' ' if line[i] == ' ' else line[i+1] for i in range(0, len(line), 4)] for line in input_values[0].split("\n")][:-1]
        # rotate list so each column is its own list and remove empty positions
        stacks = [list(filter(lambda x: not str.isspace(x), reversed(row))) for row in zip(*stacks)]

        moves = [[int(i) for i in line.split(' ') if i.isdecimal()] for line in input_values[1].split("\n")]

        return (stacks, moves)

def get_result(input: tuple[list[list[str]], list[list[int]]]) -> str:
    stacks = input[0]
    moves = input[1]

    for move in moves:
        amount = move[0]
        from_index = move[1] - 1
        to_index = move[2] - 1

        # get stack of boxes and reverse order to mimic taking one at a time (top box would be at bottom of stack of moved boxes)
        boxes_to_take = list(reversed(stacks[from_index][-amount:]))
        stacks[from_index] = stacks[from_index][:-amount]
        stacks[to_index].extend(boxes_to_take)

    return ''.join([stack[-1] for stack in stacks])

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(get_result(input_values))
