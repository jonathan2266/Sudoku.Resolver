namespace Sudoku.Strategies
{
    public class MixedStrategy : IStrategy
    {
        private SudokuBoard _board = default!;

        public void Run(SudokuBoard board)
        {
            ArgumentNullException.ThrowIfNull(board, nameof(board));

            _board = board;


        }

        private void GenerateRemainingDigitsLookup()
        {
            Span<Remaining> remainings = stackalloc Remaining[_board.Size * _board.Size];
        }

        private static void FindAllNakedSingles()
        {

        }
    }

    public struct Remaining
    {

        public int Row { get; set; }
        public int Column { get; set; }
    }
}
