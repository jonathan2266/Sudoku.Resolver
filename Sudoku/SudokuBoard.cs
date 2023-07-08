using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Enumerables;

namespace Sudoku
{
    public class SudokuBoard
    {
        private readonly int _boardSize;
        private readonly int _internalBoardSquareSize;

        private readonly int _rowDimension = 0;
        private readonly int _columnDimension = 1;


        private readonly Cell[,] _board;
        private static readonly int[] _supportedBoardSizes = new int[2] { 9, 16 };

        public SudokuBoard(Cell[,] cells) : this(ref cells)
        {

        }

        public SudokuBoard(ref Cell[,] cells)
        {
            IsInitialBoardConfigurationValid(ref cells);

            _board = cells;
            _boardSize = cells.GetLength(_rowDimension);
            _internalBoardSquareSize = Convert.ToInt32(Math.Sqrt(_boardSize));
            ValidateBoardSizeAndInternalSquareSize();
        }

        public ref Cell this[int row, int column]
        {
            get
            {
                return ref _board[row, column];
            }
        }

        public int Size { get { return _boardSize; } }
        public int InternalSquareSize { get { return _internalBoardSquareSize; } }


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
            return IsSudokuSquareValid(GetSudokuSquare(row, column));
        }

        public bool IsBoardStateValid()
        {
            return ValidateIfAllRowsAndColumnsAreValid() && ValidateAllBoardSquares();
        }

        public bool IsBoardResolved()
        {
            return IsBoardStateValid() && DoesEachCellHaveAValue();
        }

        public static SudokuBoard FromOrderedCells(Cell[,] cells)
        {
            return new SudokuBoard(ref cells);
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

                var isColumnValid = IsBoardColumnValid(GetBoardColumn(row)); //Board is validated to be square.
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
            int leftNormalizedRow = row / _internalBoardSquareSize * _internalBoardSquareSize;
            int leftNormalizedColumn = column / InternalSquareSize * InternalSquareSize;

            return boardSpan.Slice(leftNormalizedRow, leftNormalizedColumn, _internalBoardSquareSize, _internalBoardSquareSize);
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

        private static bool IsBoardRowValid(Span<Cell> boardRow)
        {
            int merged = 0;

            for (int i = 0; i < boardRow.Length; i++)
            {
                ref var cell = ref boardRow[i];
                if (cell.Value.HasValue)
                {
                    int x = 1;

                    x <<= cell.Value.Value;

                    if ((x & merged) > 0)
                    {
                        return false;
                    }

                    merged |= x;
                }
            }

            return true;
        }

        private static bool IsBoardColumnValid(RefEnumerable<Cell> boardColumn)
        {
            int merged = 0;

            for (int i = 0; i < boardColumn.Length; i++)
            {
                ref var cell = ref boardColumn[i];
                if (cell.Value.HasValue)
                {
                    int x = 1;

                    x <<= cell.Value.Value;

                    if ((x & merged) > 0)
                    {
                        return false;
                    }

                    merged |= x;
                }
            }

            return true;
        }

        private bool IsSudokuSquareValid(Span2D<Cell> square)
        {
            int merged = 0;


            for (int row = 0; row < square.Width; row++)
            {
                for (int column = 0; column < square.Height; column++)
                {
                    ref var cell = ref square[row, column];
                    if (cell.Value.HasValue)
                    {
                        int x = 1;

                        x <<= cell.Value.Value;

                        if ((x & merged) > 0)
                        {
                            return false;
                        }

                        merged |= x;
                    }
                }
            }

            return true;
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

        private void IsInitialBoardConfigurationValid(ref Cell[,] cells)
        {
            if (cells.GetLength(_rowDimension) != cells.GetLength(_columnDimension))
            {
                throw new ArgumentException("The board needs to be square", nameof(cells));
            }

            if (!_supportedBoardSizes.Contains(cells.GetLength(_rowDimension)))
            {
                throw new ArgumentException($"The board has an invalid size. Use one of the following supported sizes. {string.Join(',', _supportedBoardSizes)}", nameof(cells));
            }
        }

        private void ValidateBoardSizeAndInternalSquareSize()
        {
            if (_internalBoardSquareSize * _internalBoardSquareSize != _boardSize)
            {
                throw new InvalidOperationException($"The boards internal square {_internalBoardSquareSize} and board size {_boardSize} are not configured correctly.");
            }
        }
    }
}