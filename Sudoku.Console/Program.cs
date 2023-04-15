using BenchmarkDotNet.Running;
using System;

namespace Sudoku.Benchmark
{
    public class Program
    {


        static void Main(string[] args)
        {

            var board = new SudokuBoard(PuzzleSets.GetSet1());
            bool isResolved = false;

            for (int i = 0; i < 1000; i++)
            {
                isResolved = board.IsBoardResolved();
            }

            //try
            //{
            //    var summary = BenchmarkRunner.Run<SudokuValidationBenchmark>();
            //    Console.WriteLine(summary);
            //}
            //catch (Exception e)
            //{

            //    Console.WriteLine(e.ToString());
            //}

           // Console.ReadKey();

            //Cell[,] cells = CreateCellsFrom(_puzzleSolved);

            //var board = new SudokuBoard(cells);


            //board.IsBoardStateValid();
            //board.IsBoardResolved();

            //Console.ReadKey();
        }


    }
}