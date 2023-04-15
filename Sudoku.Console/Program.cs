using BenchmarkDotNet.Running;
using Sudoku.Benchmark.Benchmarks;
using System;

namespace Sudoku.Benchmark
{
    public class Program
    {
        private static readonly bool _startBenchmarks = true;

        static void Main(string[] args)
        {
            if (_startBenchmarks)
            {
                RunBenchmarks();
            }
            else
            {
                RunCustomTest();
            }



            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static void RunBenchmarks()
        {
            var summary = BenchmarkRunner.Run<SudokuValidationBenchmark>();
        }

        private static void RunCustomTest()
        {
            var board = new SudokuBoard(PuzzleSets.GetSolvedSet_1());

            var isSolved = false;

            for (int i = 0; i < 1000000; i++)
            {
                var isValid = board.IsBoardStateValid();
                isSolved = board.IsBoardResolved();
            }
        }
    }
}