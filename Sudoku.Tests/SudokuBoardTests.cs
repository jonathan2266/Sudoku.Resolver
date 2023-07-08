using Sudoku.Puzzles.Sets;

namespace Sudoku.Tests
{
    [TestClass]
    public class SudokuBoardTests
    {
        private SudokuBoard? _board = null;


        [TestInitialize]
        public void Setup()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SudokuBoard_NonSquareBoard_InvalidArgumentException()
        {
            _board = new SudokuBoard(InvalidBoards.NonSquare);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SudokuBoard_UnsupportedLength_InvalidArgumentException()
        {
            _board = new SudokuBoard(InvalidBoards.InvalidSize);
        }

        [TestMethod]
        public void SudokuBoard_ValidBoard_StateValid()
        {
            _board = new SudokuBoard(Set_1.Unsolved);

            var isValid = _board.IsBoardStateValid();

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void SudokuBoard_ValidateBoardWithDuplicateRow_InvalidBoard()
        {
            _board = new SudokuBoard(InvalidBoards.DuplicateNumberInRow);

            var isValid = _board.IsBoardStateValid();

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void SudokuBoard_ValidateBoardWithDuplicateRow_HasOneInvalidRow()
        {
            _board = new SudokuBoard(InvalidBoards.DuplicateNumberInRow);
            int ExpectedInvalidRows = 1;
            int actualInvalidRows = 0;
            int rows = _board.Size;

            for (int i = 0; i < rows; i++)
            {
                if (!_board.IsRowValid(i))
                {
                    ++actualInvalidRows;
                }
            }

            Assert.AreEqual(ExpectedInvalidRows, actualInvalidRows);
        }

        [TestMethod]
        public void SudokuBoard_ValidateBoardWithDuplicateColumn_InvalidBoard()
        {
            _board = new SudokuBoard(InvalidBoards.DuplicateColumnInRow);

            var isValid = _board.IsBoardStateValid();

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void SudokuBoard_ValidateBoardWithDuplicateColumn_HasOneInvalidColumn()
        {
            _board = new SudokuBoard(InvalidBoards.DuplicateColumnInRow);
            int ExpectedInvalidColumns = 1;
            int actualInvalidColumns = 0;
            int columns = _board.Size;

            for (int i = 0; i < columns; i++)
            {
                if (!_board.IsColumnValid(i))
                {
                    ++actualInvalidColumns;
                }
            }

            Assert.AreEqual(ExpectedInvalidColumns, actualInvalidColumns);
        }

        [TestMethod]
        public void SudokuBoard_ValidateBoardWithDuplicateInternalSquare_InvalidBoard()
        {
            _board = new SudokuBoard(InvalidBoards.DuplicateNumberInSquare);

            var isValid = _board.IsBoardStateValid();

            Assert.IsFalse(isValid);
        }



        [TestMethod]
        public void SudokuBoard_ValidateBoardWithDuplicateInternalSquare_HasOneInvalidSquare()
        {
            _board = new SudokuBoard(InvalidBoards.DuplicateNumberInSquare);
            var size = _board.Size;
            var internalBoardSize = _board.InternalSquareSize;
            int expectedInvalidSquares = 1;
            int actualInvalidSquares = 0;

            for (int row = 0; row < size; row += internalBoardSize)
            {
                for (int column = 0; column < size; column += internalBoardSize)
                {
                    if (!_board.IsSquareValid(row, column))
                    {
                        ++actualInvalidSquares;
                    }
                }
            }

            Assert.AreEqual(expectedInvalidSquares, actualInvalidSquares);
        }

        [TestMethod]
        public void SudokuBoard_ValidateBoardWithSolved_9_SizePuzzle_BoardValid()
        {
            _board = new SudokuBoard(Set_1.Solved);

            var isValid = _board.IsBoardStateValid();

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void SudokuBoard_ValidateBoardWithSolved_9_SizePuzzle_BoardIsSolved()
        {
            _board = new SudokuBoard(Set_1.Solved);

            var isValid = _board.IsBoardResolved();

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void SudokuBoard_ValidateBoardWithUnSolved_9_SizePuzzle_BoardIsNotSolved()
        {
            _board = new SudokuBoard(Set_1.Unsolved);

            var isValid = _board.IsBoardResolved();

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SudokuBoard_IsRowValid_OutOfRange()
        {
            _board = new SudokuBoard(Set_1.Solved);

            int outOfBounds = _board.Size + 1;

            _ = _board.IsRowValid(outOfBounds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SudokuBoard_IsColumnValid_OutOfRange()
        {
            _board = new SudokuBoard(Set_1.Solved);

            int outOfBounds = _board.Size + 1;

            _ = _board.IsColumnValid(outOfBounds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SudokuBoard_IsInternalSquareValid_OutOfRange()
        {
            _board = new SudokuBoard(Set_1.Solved);

            int outOfBounds = _board.Size + 1;

            _ = _board.IsSquareValid(outOfBounds, outOfBounds);
        }

        [TestMethod]
        public void SudokuBoard_Size_MatchesPuzzleSize()
        {
            _board = new SudokuBoard(Set_1.Solved);

            int boardSizeFromProperty = _board.Size;
            int rawPuzzleBoardSize = Set_1.Solved.GetLength(0);

            Assert.AreEqual(boardSizeFromProperty, rawPuzzleBoardSize);
        }

        [TestMethod]
        public void SudokuBoard_ResetPuzzle_ResetsToFixedCellState()
        {
            var unsolvedSet = Set_1.Unsolved;
            var cellsWithNewValues = unsolvedSet.FillAllEmptyCellsWithRandomValues();
            _board = new SudokuBoard(cellsWithNewValues);
            bool hasNonMatchingValues = false;


            _board.ResetState();
            for (int row = 0; row < _board.Size; row++)
            {
                for (int column = 0; column < _board.Size; column++)
                {
                    if (_board[row, column] != unsolvedSet[row, column])
                    {
                        hasNonMatchingValues = true;
                    }
                }
            }

            Assert.IsFalse(hasNonMatchingValues);
        }
    }
}
