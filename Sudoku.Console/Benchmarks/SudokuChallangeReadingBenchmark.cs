using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using Sudoku.Parser;
using Sudoku.Parser.File;
using System.Text;

namespace Sudoku.Benchmark.Benchmarks
{
    [SimpleJob(warmupCount: 10, iterationCount: 10)]
    [MemoryDiagnoser]
    public class SudokuChallangeReadingBenchmark
    {
        private IRetrievePuzzle _reader = null;
        private IRetrievePuzzle _reader2 = null;
        private IRetrievePuzzle _reader3 = null;

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

            _reader2 = new RetrieveMinimalSudokuChallengePuzzles(new StringReader(Puzzles.Puzzles.all_17_clue_sudokus));
            _reader3 = new RetrieveMinimalSudokuChallengePuzzlesBytes(Encoding.UTF8.GetBytes(Puzzles.Puzzles.all_17_clue_sudokus));
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

        [Benchmark(Baseline = false)]
        public async Task TimeToReadFileAndParseToBoardFastV2()
        {
            await _reader2.Load();
        }

        [Benchmark]
        public async Task TimeToReadFileAndParseToBoardAndValidateAllBoardsFastV2()
        {
            var boards = await _reader2.Load();

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
            await _reader3.Load();
        }

        [Benchmark]
        public async Task TimeToReadFileAndParseToBoardAndValidateAllBoardsFastV3()
        {
            var boards = await _reader3.Load();

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
