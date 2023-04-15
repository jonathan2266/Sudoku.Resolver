using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public struct Cell
    {
        public Cell()
        {
            IsFixed = false;
            Value = null;
        }

        public Cell(int value)
        {
            IsFixed = true;
            Value = value;
        }

        public bool IsFixed { get; init; }
        public int? Value { get; set; }
    }
}
