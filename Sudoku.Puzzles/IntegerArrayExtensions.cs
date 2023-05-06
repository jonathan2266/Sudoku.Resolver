using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Puzzles
{
    public static class IntegerArrayExtensions
    {
        private static readonly int _firstArrayIndex = 0;
        private static readonly int _secondArrayIndex = 1;

        public static Cell[,] ToCells(this int?[,] rawValues)
        {
            if (rawValues == null)
            {
                throw new ArgumentNullException(nameof(rawValues), "argument cannot be null");
            }

            Cell[,] cells = new Cell[rawValues.GetLength(_firstArrayIndex), rawValues.GetLength(_secondArrayIndex)];
            for (int row = 0; row < rawValues.GetLength(_firstArrayIndex); row++)
            {
                for (int column = 0; column < rawValues.GetLength(_secondArrayIndex); column++)
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
