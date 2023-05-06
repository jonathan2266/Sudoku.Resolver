using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Puzzles.Sets
{
    public static class InvalidBoards
    {
        private readonly static int?[,] _nonSquare = new int?[,]
        {
            {4, null, 5, null, null, null, null, 7, null},
            {7,6,3,null,1,null,4,null,null },
            {null,2,8,4,null,null,5,6, null },

            {1,null,7,6,null,null,8,null,2 },
            {null,8,null,3,7,null,null,null,6 },
            {null,null,6,8,2,1,null,null,5 },

            {null,7,null,null,5,3,9,null,null },
            {null,3,null,9,6,null,null,null,null }
        };

        private readonly static int?[,] _InvalidSize = new int?[,] {

            {4, null, 5, null, null, null, null, 7},
            {7,6,3,null,1,null,4,null},
            {null,2,8,4,null,null,5,6},

            {1,null,7,6,null,null,8,null },
            {null,8,null,3,7,null,null,null },
            {null,null,6,8,2,1,null,null},

            {null,7,null,null,5,3,9,null },
            {null,3,null,9,6,null,null,null}
        };

        private readonly static int?[,] _duplicateNumberInRow = new int?[,]
        {
            {4, null, 5, null, null, null, null, 7, null},
            {7,6,3,null,1,null,4,null,null },
            {null,2,8,4,null,null,5,6, null },

            {1,null,7,6,null,null,8,1,2 }, //Duplicate 1 in row
            {null,8,null,3,7,null,null,null,6 },
            {null,null,6,8,2,1,null,null,5 },

            {null,7,null,null,5,3,9,null,null },
            {null,3,null,9,6,null,null,null,null },
            {5,4,null,7,8,null,null,null,3 },
        };

        private readonly static int?[,] _duplicateNumberInColumn = new int?[,]
        {
            {4, null, 5, null, null, null, null, 7, null},
            {7,6,3,null,1,null,4,null,null },
            {null,2,8,4,null,null,5,6, null },

            {1,null,7,6,null,null,8,null,2 },
            {null,8,null,3,7,null,null,null,6 },
            {null,null,6,8,2,1,null,null,5 },

            {null,7,null,null,5,3,9,null,null },
            {null,3,null,9,6,null,null,null,null },
            {5,4,null,7,8,null,null,7,3 },
        };

        private readonly static int?[,] _duplicateNumberInSquare = new int?[,]
        {
            {4, null, 5, null, null, null, null, 7, null},
            {7,6,3,null,1,null,4,null,null },
            {null,2,8,4,null,null,5,6, null },

            {1,null,7,6,null,null,8,null,2 },
            {null,8,null,3,7,null,null,null,6 },
            {null,null,6,8,2,1,null,null,5 },

            {null,7,null,null,5,3,9,null,null },
            {null,3,null,9,6,7,null,null,null },
            {5,4,null,7,8,null,null,null,3 },
        };

        public static Cell[,] NonSquare { get { return _nonSquare.ToCells(); } }
        public static Cell[,] InvalidSize { get { return _InvalidSize.ToCells(); } }
        public static Cell[,] DuplicateNumberInRow { get { return _duplicateNumberInRow.ToCells(); } }
        public static Cell[,] DuplicateColumnInRow { get { return _duplicateNumberInColumn.ToCells(); } }
        public static Cell[,] DuplicateNumberInSquare { get { return _duplicateNumberInSquare.ToCells(); } }

    }
}
