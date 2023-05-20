namespace Sudoku.Strategies
{
    public class BruteForceStrategy : IStrategy
    {
        private SudokuBoard _board;

        public void Run(SudokuBoard board)
        {
            _board = board;

            if (!board.IsBoardStateValid())
            {
                return;
            }

            bool IsInUnresolvedState = true;
            bool skipForwardJump = true;

            int currentRow = 0;
            int currentColumn = 0;

            while (IsInUnresolvedState)
            {
                skipForwardJump = false;

                ref var cell = ref board[currentRow, currentColumn];
                if (!cell.IsFixed)
                {
                    if (CanAssignNewValueToCell(ref cell))
                    {
                        AssignNewValueToCell(ref cell);
                        if (!IsCurrentMoveValid(currentRow, currentColumn))
                        {
                            skipForwardJump = true;
                        }
                        else if (currentRow == _board.Size - 1 && currentColumn == board.Size - 1)
                        {
                            IsInUnresolvedState = false;
                        }

                    }
                    else
                    {
                        cell.Value = null;
                        skipForwardJump = true;
                        while (true)
                        {
                            SetToPreviousPosition(ref currentRow, ref currentColumn);
                            ref var othercell = ref board[currentRow, currentColumn];
                            if (!othercell.IsFixed)
                            {
                                break;
                            }

                        }
                    }
                }

                if (!skipForwardJump)
                {
                    SetToNextPosition(ref currentRow, ref currentColumn);
                }
            }

            if (!_board.IsBoardResolved())
            {
                throw new InvalidOperationException("Unable to solve the puzzle.");
            }
        }

        private bool IsCurrentMoveValid(int row, int column)
        {
            return (_board.IsRowValid(row) && _board.IsColumnValid(column) && _board.IsSquareValid(row, column));
        }

        private void SetToNextPosition(ref int row, ref int column)
        {
            if (column < _board.Size - 1)
            {
                ++column;
            }
            else if (row < _board.Size - 1)
            {
                ++row;
                column = 0;
            }
            else
            {
                column = 0;
                row = 0;
            }
        }

        private void SetToPreviousPosition(ref int row, ref int column)
        {
            if (column == 0)
            {
                --row;
                column = _board.Size - 1;
            }
            else if (row >= 0)
            {
                --column;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private static bool CanAssignNewValueToCell(ref Cell cell)
        {
            return (!cell.Value.HasValue || cell.Value < 9);
        }

        private static void AssignNewValueToCell(ref Cell cell)
        {
            cell.Value ??= 0;
            ++cell.Value;
        }
    }
}
