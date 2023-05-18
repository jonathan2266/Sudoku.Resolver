using Sudoku.Parser.Utilities;

namespace Sudoku.Parser.File
{
    public class RetrieveMinimalSudokuChallengePuzzles : IRetrievePuzzle
    {
        private readonly TextReader _reader;
        private readonly UnorderedCellUtilities.Boundary _boundary = new(9); //puzzles are in 9 size

        public RetrieveMinimalSudokuChallengePuzzles(TextReader reader)
        {
            _reader = reader;
        }

        public Task<IEnumerable<SudokuBoard>> Load()
        {
            var deconstructionResult = DeconstuctFile(_reader);
            return Task<IEnumerable<SudokuBoard>>.FromResult(DeconstructRawPuzzlesInToPuzzles(deconstructionResult.RawPuzzles).AsEnumerable());
        }

        private SudokuBoard[] DeconstructRawPuzzlesInToPuzzles(string[] rawPuzzles)
        {
            SudokuBoard[] boards = new SudokuBoard[rawPuzzles.Length];

            for (int i = 0; i < rawPuzzles.Length; i++)
            {
                var puzzleCells = ContructPuzzleLineIntoCells(rawPuzzles[i]);
                boards[i] = ConstructBoardFromCells(ref puzzleCells);
            }

            return boards;
        }

        private static SudokuBoard ConstructBoardFromCells(ref Cell[,] cells)
        {
            return new SudokuBoard(cells);
        }

        private Cell[,] ContructPuzzleLineIntoCells(string puzzleLine)
        {
            int rowPosition = 0;
            int columnPosition = 0;

            Cell[,] cells = new Cell[_boundary.RowLength, _boundary.ColumnLength];

            for (int i = 0; i < puzzleLine.Length; i++)
            {
                if (i > 0 && i % _boundary.RowLength == 0)
                {
                    rowPosition++;
                    columnPosition = 0;
                }

                var puzzleValue = (int)char.GetNumericValue(puzzleLine[i]);
                if (puzzleValue > 0)
                {
                    cells[rowPosition, columnPosition] = new Cell(puzzleValue);
                }

                ++columnPosition;
            }

            return cells;
        }

        private static (int TotalPuzzles, string[] RawPuzzles) DeconstuctFile(TextReader reader)
        {
            var puzzleAmount = reader.ReadLine();
            if (!int.TryParse(puzzleAmount, out int parsedAmount))
            {
                return (0, Array.Empty<string>());
            }

            var rawPuzzles = reader.ReadToEnd().Split("\r\n");

            return (parsedAmount, rawPuzzles);
        }
    }
}
