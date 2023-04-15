using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Enumerables;
using System;

namespace Sudoku
{
    public class SudokuBoard
    {
        private readonly int _boardSize = 9;
        private readonly int _internalBoardSquareSize = 3;
        private readonly int _internalBoardSquareUniqueNumbers;
        private readonly int _internalBoardSquareStartNumber = 1;

        private readonly int _rowDimension = 0;
        private readonly int _columnDimension = 1;


        private readonly Cell[,] _board;

        private readonly Dictionary<int, bool> _duplicateNumberLookup;

        public SudokuBoard(Cell[,] cells)
        {
            _board = new Cell[_boardSize, _boardSize];
            _board = cells;
            _internalBoardSquareUniqueNumbers = _internalBoardSquareSize * _internalBoardSquareSize;


            _duplicateNumberLookup = new Dictionary<int, bool>(_internalBoardSquareUniqueNumbers);
            for (int i = _internalBoardSquareStartNumber; i < _internalBoardSquareUniqueNumbers + _internalBoardSquareStartNumber; i++)
            {
                _duplicateNumberLookup.Add(i, false);
            }
        }

        public ref Cell this[int row, int column]
        {
            get
            {
                return ref _board[row, column];
            }
        }

        public bool IsRowValid(int row)
        {
            return IsBoardRowValid(GetBoardRow(row));
        }

        public bool IsColumnValid(int column)
        {
            return IsBoardColumnValid(GetBoardColumn(column));
        }

        public bool IsSquareValid(int row, int column)
        {
            return IsSudokuSquareValid(GetSudokuSquare(row / _internalBoardSquareSize, column / _internalBoardSquareSize));
        }

        public bool IsBoardStateValid()
        {
            if (ValidateIfAllRowsAndColumnsAreValid() && ValidateAllBoardSquares())
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public bool IsBoardResolved()
        {
            if (IsBoardStateValid() && DoesEachCellHaveAValue())
            {
                return true;
            }

            return false;
        }

        public void ResetState()
        {
            for (int row = 0; row < _board.GetLength(_rowDimension); row++)
            {
                for (int column = 0; column < _board.GetLength(_columnDimension); column++)
                {
                    ref var cell = ref this[row, column];
                    if (!cell.IsFixed) cell.Value = null;
                }
            }
        }

        private bool ValidateIfAllRowsAndColumnsAreValid()
        {
            for (int row = 0; row < _board.GetLength(_rowDimension); row++)
            {
                var isRowValid = IsBoardRowValid(GetBoardRow(row));
                if (!isRowValid)
                {
                    return false;
                }
            }

            for (int column = 0; column < _board.GetLength(_columnDimension); column++)
            {
                var isColumnValid = IsBoardColumnValid(GetBoardColumn(column));
                if (!isColumnValid)
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidateAllBoardSquares()
        {
            var rowAndColumnSections = _boardSize / _internalBoardSquareSize;
            for (int row = 0; row < rowAndColumnSections; row++)
            {
                for (int column = 0; column < rowAndColumnSections; column++)
                {
                    if (!IsSudokuSquareValid(GetSudokuSquare(row * _internalBoardSquareSize, column * _internalBoardSquareSize)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private ReadOnlySpan2D<Cell> GetSudokuSquare(int row, int column)
        {
            ReadOnlySpan2D<Cell> boardSpan = _board;
            return boardSpan.Slice(row, column, _internalBoardSquareSize, _internalBoardSquareSize);
        }

        private ReadOnlySpan<Cell> GetBoardRow(int row)
        {
            ReadOnlySpan2D<Cell> boardSpan = _board;
            return boardSpan.GetRowSpan(row);
        }

        private ReadOnlyRefEnumerable<Cell> GetBoardColumn(int column)
        {
            ReadOnlySpan2D<Cell> boardSpan = _board;
            return boardSpan.GetColumn(column);
        }

        private bool IsBoardRowValid(ReadOnlySpan<Cell> boardRow)
        {
            ResetLookup(_duplicateNumberLookup);

            for (int i = 0; i < boardRow.Length; i++)
            {
                var cell = boardRow[i];
                if (IsCellValueDuplicateInContinuationDirection(ref cell, _duplicateNumberLookup))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsBoardColumnValid(ReadOnlyRefEnumerable<Cell> boardColumn)
        {
            ResetLookup(_duplicateNumberLookup);

            for (int i = 0; i < boardColumn.Length; i++)
            {
                var cell = boardColumn[i];
                if (IsCellValueDuplicateInContinuationDirection(ref cell, _duplicateNumberLookup))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsSudokuSquareValid(ReadOnlySpan2D<Cell> square)
        {
            ResetLookup(_duplicateNumberLookup);

            for (int row = 0; row < square.Width; row++)
            {
                for (int column = 0; column < square.Height; column++)
                {
                    var cell = square[row, column];

                    if (IsCellValueDuplicateInContinuationDirection(ref cell, _duplicateNumberLookup))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void ResetLookup(Dictionary<int, bool> lookup)
        {
            foreach (var key in lookup.Keys)
            {
                lookup[key] = false;
            }
        }

        private bool DoesEachCellHaveAValue()
        {
            for (int row = 0; row < _board.GetLength(_rowDimension); row++)
            {
                for (int column = 0; column < _board.GetLength(_columnDimension); column++)
                {
                    ref var cell = ref this[row, column];
                    if (!cell.Value.HasValue)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool IsCellValueDuplicateInContinuationDirection(ref Cell cell, Dictionary<int, bool> lookupContinuation)
        {
            if (!cell.Value.HasValue)
            {
                return false;
            }

            if (lookupContinuation[cell.Value.Value])
            {
                return true;
            }
            else
            {
                lookupContinuation[cell.Value.Value] = true;
            }

            return false;
        }
    }

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