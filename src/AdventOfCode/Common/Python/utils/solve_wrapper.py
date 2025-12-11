import time
import json
from collections.abc import Callable

class PuzzleResult:
    def __init__(self, result: any, time: any):
        self.result = result
        self.time = time

    def to_dict(self):
        return {'result': str(self.result), 'time': str(self.time)}

def run(file_name: str, prepare_data_method: Callable, get_result_method: Callable):
    start_time = time.perf_counter()
    data = prepare(prepare_data_method, file_name)
    result = solve(get_result_method, data)
    run_time_ms = (time.perf_counter() - start_time) * 1000

    puzzleResult = PuzzleResult(result, run_time_ms)
    print(json.dumps(puzzleResult.to_dict()))

def prepare(prepare_data_method: Callable, file_name: str) -> any:
    data = prepare_data_method(file_name)
    return data

def solve(get_result_method: Callable, data: any) -> any:
    result = get_result_method(data)
    return result