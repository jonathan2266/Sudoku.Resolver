using BenchmarkDotNet.Running;
using Sudoku.Benchmark.Benchmarks;
using Sudoku.Puzzles.Sets;
using System;

namespace Sudoku.Benchmark
{
    public class Program
    {
        private static readonly bool _startBenchmarks = true;

        static async Task Main(string[] args)
        {
            await Task.Delay(1);

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
            var summery2 = BenchmarkRunner.Run<SudokuChallangeReadingBenchmark>();
        }

        private static void RunCustomTest()
        {
            var board = new SudokuBoard(Set_1.Solved);

            var isSolved = false;

            for (int i = 0; i < 1000000; i++)
            {
                var isValid = board.IsBoardStateValid();
                isSolved = board.IsBoardResolved();
            }
        }
    }
}