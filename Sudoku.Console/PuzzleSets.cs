using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Benchmark
{
    public class PuzzleSets
    {
        private readonly static int?[,] _puzzle = new int?[,]
  {
            {4, null, 5, null, null, null, null, 7, null},
            {7,6,3,null,1,null,4,null,null },
            {null,2,8,4,null,null,5,6, null },

            {1,null,7,6,null,null,8,null,2 },
            {null,8,null,3,7,null,null,null,6 },
            {null,null,6,8,2,1,null,null,5 },

            {null,7,null,null,5,3,9,null,null },
            {null,3,null,9,6,null,null,null,null },
            {5,4,null,7,8,null,null,null,3 },
  };

        private readonly static int?[,] _puzzleSolved = new int?[,]
        {
            {4, 1, 5, 2, 9, 6, 3, 7, 8},
            {7,6,3,5,1,8,4,2,9 },
            {9,2,8,4,3,7,5,6, 1 },

            {1,5,7,6,4,9,8,3,2 },
            {2,8,4,3,7,5,1,9,6 },
            {3,9,6,8,2,1,7,4,5 },

            {6,7,2,1,5,3,9,8,4 },
            {8,3,1,9,6,4,2,5,7 },
            {5,4,9,7,8,2,6,1,3 },
        };

        public static Cell[,] GetSet1()
        {
            return CreateCellsFrom(_puzzleSolved);
        }

        private static Cell[,] CreateCellsFrom(int?[,] rawValues)
        {
            Cell[,] cells = new Cell[9, 9];
            for (int row = 0; row < rawValues.GetLength(0); row++)
            {
                for (int column = 0; column < rawValues.GetLength(1); column++)
                {
                    var value = rawValues[row, column];
                    if (value.HasValue)
                    {
                        cells[row, column] = new Cell(value.Value);
                    }
                }
            }

            return cells;
        }
    }
}
