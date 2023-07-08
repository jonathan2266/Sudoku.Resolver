using BenchmarkDotNet.Attributes;
using Sudoku.Parser;
using Sudoku.Parser.File;
using Sudoku.Parser.Readers;

namespace Sudoku.Benchmark.Benchmarks
{
    [SimpleJob(warmupCount: 10, iterationCount: 10)]
    [MemoryDiagnoser]
    public class SudokuChallangeReadingBenchmark
    {
        private IRetrievePuzzle _retriever = default!;
        private IRetrievePuzzle _retriever2 = default!;
        private IRetrievePuzzle _retriever3 = default!;

        private IReader _reader = default!;

        public SudokuChallangeReadingBenchmark()
        {

        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _reader = ReaderFromString.CreateFromString(Puzzles.Puzzles.all_17_clue_sudokus);
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _retriever = new RetrieveSudokuChallengePuzzles();
            _retriever2 = new RetrieveMinimalSudokuChallengePuzzles();
            _retriever3 = new RetrieveMinimalSudokuChallengePuzzlesBytes();
        }

        [Benchmark(Baseline = true)]
        public async Task TimeToReadFileAndParseToBoard()
        {
            await _retriever.Load(_reader);
        }

        [Benchmark]
        public async Task TimeToReadFileAndParseToBoardAndValidateAllBoards()
        {
            var boards = await _retriever.Load(_reader);

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

        [Benchmark(Baseline = false)]
        public async Task TimeToReadFileAndParseToBoardFastV2()
        {
            await _retriever2.Load(_reader);
        }

        [Benchmark]
        public async Task TimeToReadFileAndParseToBoardAndValidateAllBoardsFastV2()
        {
            var boards = await _retriever2.Load(_reader);

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

        [Benchmark(Baseline = false)]
        public async Task TimeToReadFileAndParseToBoardFastV3()
        {
            await _retriever3.Load(_reader);
        }

        [Benchmark]
        public async Task TimeToReadFileAndParseToBoardAndValidateAllBoardsFastV3()
        {
            var boards = await _retriever3.Load(_reader);

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
