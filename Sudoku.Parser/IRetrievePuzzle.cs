using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Parser.Readers;

namespace Sudoku.Parser
{
    public interface IRetrievePuzzle
    {
        Task<IEnumerable<SudokuBoard>> Load(IReader reader);
    }
}
