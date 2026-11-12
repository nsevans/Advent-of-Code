import time
import json
from collections.abc import Callable

from Common.Python.utils.PuzzleContext import PuzzleContext
from Common.Python.utils.PuzzleResult import PuzzleResult

def run(prepare_data_method: Callable, get_result_method: Callable, arguments: list[str]):
    file_name: str = arguments[1]
    puzzle_context = PuzzleContext(file_name)

    start_time = time.perf_counter()

    data = prepare(prepare_data_method, puzzle_context)
    result = solve(get_result_method, data)

    run_time_ms = (time.perf_counter() - start_time) * 1000

    puzzleResult = PuzzleResult(result, run_time_ms)
    print(json.dumps(puzzleResult.to_dict()))

def prepare(prepare_data_method: Callable, puzzle_context: PuzzleContext) -> any:
    data = prepare_data_method(puzzle_context)
    return data

def solve(get_result_method: Callable, data: any) -> any:
    result = get_result_method(data)
    return result