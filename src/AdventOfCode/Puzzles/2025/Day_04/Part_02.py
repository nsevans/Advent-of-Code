#####
# Year 2025, Day 04, Part 02
#

import sys

def prepare_data(input_file: str) -> list[list[str]]:
    with open(input_file) as file:
        input_values = file.read().split('\n')
        return [[i for i in line] for line in input_values]

def get_result(input: list[list[str]]) -> str:
    directions = [[0,-1],[1,-1],[1,0],[1,1],[0,1],[-1,1],[-1,0],[-1,-1]]
    adjacent_limit = 4
    row = len(input)
    col = len(input[0])

    total_removed = 0

    # keep track of where to start each iteration, will update to the first
    # index that has a value on each iteration
    start_pos = [0,0]

    # keep track of each iteration
    while True:
        removed_count = 0
        next_start_pos = None

        for y in range(start_pos[0], row):
            for x in range(start_pos[1], col):
                if input[y][x] != '@':
                    continue

                adjacents_found = 0
                for dir in directions:
                    ny = y + dir[0]
                    nx = x + dir[1]

                    if ny < 0 or nx < 0 or ny >= len(input) or nx >= len(input[y]):
                        continue

                    if input[ny][nx] == '@':
                        adjacents_found += 1

                        # break out early if number of adjacent rolls has met the limit
                        if adjacents_found >= adjacent_limit:
                            break

                if adjacents_found < adjacent_limit:
                    removed_count += 1
                    input[y][x] = '.'

                # when finding a value that can't be removed, if its the first of this iteration
                # keep track of its index so the next iteration can start at this point
                elif next_start_pos == None:
                    next_start_pos = [y,x]

            # reset starting x position to 0 so the y scans after the first scan start at the left most position
            start_pos[1] = 0

        if removed_count == 0:
            break

        total_removed += removed_count
        start_pos = next_start_pos
    return total_removed

if __name__ == '__main__':
    input_file = sys.argv[1]
    input_values = prepare_data(input_file)
    print(get_result(input_values))
