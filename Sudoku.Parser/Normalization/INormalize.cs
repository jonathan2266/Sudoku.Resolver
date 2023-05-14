using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Parser.Normalization
{
    public interface INormalize
    {
        int Map(string value);
    }
}
