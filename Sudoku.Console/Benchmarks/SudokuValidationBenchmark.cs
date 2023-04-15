using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Benchmark.Benchmarks
{
    [SimpleJob(warmupCount: 10, iterationCount: 10)]
    //[EtwProfiler(performExtraBenchmarksRun: false)] 
    [MemoryDiagnoser]
    public class SudokuValidationBenchmark
    {
        private SudokuBoard _board;

        public SudokuValidationBenchmark()
        {

        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _board = new SudokuBoard(PuzzleSets.GetSolvedSet_1());
        }

        [Benchmark(Baseline = true)]
        public bool IsBoardStateValid()
        {
            return _board.IsBoardStateValid();
        }

        [Benchmark]
        public bool IsBoardResolved()
        {
            return _board.IsBoardResolved();
        }

    }
}
