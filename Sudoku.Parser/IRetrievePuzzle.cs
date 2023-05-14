using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Parser
{
    public interface IRetrievePuzzle
    {
        ValueTask<IEnumerable<SudokuBoard>> Load();
    }
}
