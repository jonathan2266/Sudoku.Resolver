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



            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static void RunBenchmarks()
        {
            var summary = BenchmarkRunner.Run<SudokuValidationBenchmark>();
        }
    }
}