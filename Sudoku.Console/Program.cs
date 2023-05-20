using BenchmarkDotNet.Running;
using Sudoku.Benchmark.Benchmarks;
using Sudoku.Parser.File;
using Sudoku.Puzzles.Sets;
using Sudoku.Strategies;
using System;
using System.Collections.Immutable;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;

namespace Sudoku.Benchmark
{
    public class Program
    {
        private static readonly bool _startBenchmarks = true;

        static async Task Main(string[] args)
        {
            await Task.Delay(1);
            await TestResolver();
            //await Tests();

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

        private static async Task TestResolver()
        {
            var loader = new RetrieveMinimalSudokuChallengePuzzlesBytes(Encoding.UTF8.GetBytes(Puzzles.Puzzles.all_17_clue_sudokus));
            var boards = await loader.Load();

            IStrategy strategy = new BruteForceStrategy();

            foreach (var board in boards)
            {
                strategy.Run(board);
            }

            await Task.Delay(1);

        }

        private static async Task Tests()
        {
            var loader = new RetrieveMinimalSudokuChallengePuzzlesBytes(Encoding.UTF8.GetBytes(Puzzles.Puzzles.all_17_clue_sudokus));
            var boards = await loader.Load();

            for (int i = 0; i < 10; i++)
            {


                foreach (var board in boards)
                {
                    if (!board.IsBoardStateValid())
                    {
                        int not = 410;
                    }
                }
            }

            Environment.Exit(0);

            //byte[] array = Enumerable.Repeat<byte>(48, 81).ToArray();
            //Vector<byte> shiftVector = new Vector<byte>(array);

            //Span<byte> shiftedSudokuLine = stackalloc byte[81];

            //var newLineBytes = Encoding.UTF8.GetBytes(Environment.NewLine).AsSpan();

            //var contents = File.ReadAllBytes("C:\\Users\\jonat\\Desktop\\sudoku.txt").AsSpan();
            //int index=  contents.IndexOf(newLineBytes);

            //var puzzleStart = contents.Slice(index + newLineBytes.Length);

            //var puzzleLine = puzzleStart.Slice(0, 81);
            //Vector<byte> vector = new(puzzleLine);
            //var resultingVector = vector - shiftVector;
            ////Span<Vector<byte>> vector = MemoryMarshal.Cast<Vector<byte>, byte>(span);
        }

        private static void RunBenchmarks()
        {
            // var summary = BenchmarkRunner.Run<SudokuValidationBenchmark>();
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