using BenchmarkDotNet.Attributes;
using Sudoku.Parser;
using Sudoku.Parser.File;

namespace Sudoku.Benchmark.Benchmarks
{
    [SimpleJob(warmupCount: 10, iterationCount: 10)]
    [MemoryDiagnoser]
    public class SudokuChallangeReadingBenchmark
    {
        private IRetrievePuzzle _reader = null;

        public SudokuChallangeReadingBenchmark()
        {

        }

        [GlobalSetup]
        public void GlobalSetup()
        {
     
        }

        [IterationSetup]
        public void IterationSetup()
        {
            TextReader reader = new StringReader(Puzzles.Puzzles.all_17_clue_sudokus);
            _reader = new RetrieveSudokuChallengePuzzles(reader);
        }

        [Benchmark(Baseline = true)]
        public async Task TimeToReadFileAndParseToBoard()
        {
            await _reader.Load();
        }

        [Benchmark]
        public async Task TimeToReadFileAndParseToBoardAndValidateAllBoards()
        {
            var boards = await _reader.Load();

            bool anyInvalid = false;

            foreach (var board in boards)
            {
                if (!board.IsBoardStateValid())
                {
                    anyInvalid = true;
                }
            }

            if (anyInvalid)
            {
                Console.WriteLine("Invalid board during test");
            }
        }
    }
}
