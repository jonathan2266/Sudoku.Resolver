using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Puzzles.Sets
{
    public class LargeSet_1
    {
        private readonly static int?[,] _unsolved = new int?[,]
        {
            {null,null,null,3,   null,null,10,9,    4,null,null,0,    null,null,null,null },
            {null,null,null,null,   null,0,11,null,    1,null,6,null,    null,3,null,null },
            {null,null,null,1,   15,7,null,null,    5,13,null,16,    10,11,null,0 },
            {null,7,null,null,   null,6,null,16,    8,null,10,2,    null,null,null,null },

            {7,null,null,15,   null,null,1,null,    null,null,null,6,    12,13,11,10 },
            {null,null,null,4,   null,16,null,null,    null,null,null,null,    null,null,2,null },
            {null,13,11,12,   7,9,null,4,    null,null,null,null,    null,null,null,5 },
            {null,null,6,10,   null,null,2,0,    16,null,null,null,    null,9,null,8 },

            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {16,2,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },

            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },

        };

        private readonly static int?[,] _solved = new int?[,]
        {
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },

            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },

            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },

            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
            {null,null,null,null,   null,null,null,null,    null,null,null,null,    null,null,null,null },
        };

        public static Cell[,] Unsolved { get { return _unsolved.ToCells(); } }
        public static Cell[,] Solved { get { return _solved.ToCells(); } }
    }
}
