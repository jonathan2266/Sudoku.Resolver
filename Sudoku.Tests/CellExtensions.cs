using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Tests
{
    public static class CellExtensions
    {
        private static readonly Random _ramdom = new(15);
        private static readonly int _cellMaxValue = 9;

        public static Cell[,] FillAllEmptyCellsWithRandomValues(this Cell[,] cells)
        {
            for (var row = 0; row < cells.GetLength(0); row++)
            {
                for (var column = 0; column < cells.GetLength(1); column++)
                {
                    if (!cells[row, column].IsFixed && cells[row, column].Value == null)
                    {
                        cells[row, column].Value = _ramdom.Next(_cellMaxValue);
                    }
                }
            }

            return cells;
        }
    }
}
