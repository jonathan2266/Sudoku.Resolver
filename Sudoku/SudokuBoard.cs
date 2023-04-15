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

        private readonly int _rowDimension = 0;
        private readonly int _columnDimension = 1;


        private readonly Cell[,] _board;
        private readonly HashSet<int> _duplicateNumberDetector;

        public SudokuBoard(Cell[,] cells)
        {
            _board = new Cell[_boardSize, _boardSize];
            _board = cells;
            _internalBoardSquareUniqueNumbers = _internalBoardSquareSize * _internalBoardSquareSize;

            _duplicateNumberDetector = new HashSet<int>(_internalBoardSquareUniqueNumbers);
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

        private Span2D<Cell> GetSudokuSquare(int row, int column)
        {
            Span2D<Cell> boardSpan = _board;
            return boardSpan.Slice(row, column, _internalBoardSquareSize, _internalBoardSquareSize);
        }

        private Span<Cell> GetBoardRow(int row)
        {
            Span2D<Cell> boardSpan = _board;
            return boardSpan.GetRowSpan(row);
        }

        private RefEnumerable<Cell> GetBoardColumn(int column)
        {
            Span2D<Cell> boardSpan = _board;
            return boardSpan.GetColumn(column);
        }

        private bool IsBoardRowValid(Span<Cell> boardRow)
        {
            _duplicateNumberDetector.Clear();

            int totalValuesAdded = 0;
            for (int i = 0; i < boardRow.Length; i++)
            {
                ref var cell = ref boardRow[i];
                if (cell.Value.HasValue)
                {
                    totalValuesAdded++;
                    _duplicateNumberDetector.Add(cell.Value.Value);
                }
            }

            return _duplicateNumberDetector.Count == totalValuesAdded;
        }

        private bool IsBoardColumnValid(RefEnumerable<Cell> boardColumn)
        {
            _duplicateNumberDetector.Clear();

            int totalValuesAdded = 0;
            for (int i = 0; i < boardColumn.Length; i++)
            {
                ref var cell = ref boardColumn[i];
                if (cell.Value.HasValue)
                {
                    totalValuesAdded++;
                    _duplicateNumberDetector.Add(cell.Value.Value);
                }
            }

            return _duplicateNumberDetector.Count == totalValuesAdded;
        }

        private bool IsSudokuSquareValid(Span2D<Cell> square)
        {
            _duplicateNumberDetector.Clear();

            int totalValuesAdded = 0;
            for (int row = 0; row < square.Width; row++)
            {
                for (int column = 0; column < square.Height; column++)
                {
                    ref var cell = ref square[row, column];
                    if (cell.Value.HasValue)
                    {
                        totalValuesAdded++;
                        _duplicateNumberDetector.Add(cell.Value.Value);
                    }
                }
            }

            return _duplicateNumberDetector.Count == totalValuesAdded;
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
    }
}