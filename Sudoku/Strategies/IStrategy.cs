using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Strategies
{
    public interface IStrategy
    {
        void Run(SudokuBoard board);
    }
}
