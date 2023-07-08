using BenchmarkDotNet.Attributes;
using Sudoku.Parser.File;
using Sudoku.Parser.Readers;
using Sudoku.Serialization;

namespace Sudoku.Benchmark.Benchmarks
{
    [SimpleJob(warmupCount: 10, iterationCount: 10)]
    [MemoryDiagnoser]
    public class SodokuSerializationBenchmark
    {
        private SudokuBoard[] _boards = default!;
        private ISerializeBoards _boardSerializer = default!;

        public SodokuSerializationBenchmark()
        {

        }

        [GlobalSetup]
        public async Task GlobalSetup()
        {
            //_board = new SudokuBoard(Set_1.Solved);

            var reader = ReaderFromString.CreateFromString(Puzzles.Puzzles.all_17_clue_sudokus);
            var retriever = new RetrieveMinimalSudokuChallengePuzzlesBytes();

            _boards = (await retriever.Load(reader)).ToArray();

            _boardSerializer = new DefaultSerializer();
        }

        [Benchmark(Baseline = true)]
        public void SerializeAllBoards()
        {
            foreach (var item in _boards)
            {
                var response = _boardSerializer.Serialize(item);
            }
        }
    }
}
