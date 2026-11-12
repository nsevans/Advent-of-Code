class PuzzleResult:
    def __init__(self, result: any, time: any):
        self.result = result
        self.time = time

    def to_dict(self):
        return {'result': str(self.result), 'time': str(self.time)}