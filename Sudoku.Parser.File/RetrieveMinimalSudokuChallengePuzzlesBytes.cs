using Sudoku.Parser.Readers;
using Sudoku.Parser.Utilities;
using System.Text;

namespace Sudoku.Parser.File
{
    public class RetrieveMinimalSudokuChallengePuzzlesBytes : IRetrievePuzzle
    {
        private static readonly byte[] _newLineBytes = Encoding.UTF8.GetBytes(Environment.NewLine);
        private static readonly UnorderedCellUtilities.Boundary _boundary = new(9);
        private static readonly int _expectedPuzzleLength = _boundary.ColumnLength * _boundary.RowLength;
        private static readonly int _utf8NumberOfset = 48;

        public RetrieveMinimalSudokuChallengePuzzlesBytes()
        {

        }

        public async Task<IEnumerable<SudokuBoard>> Load(IReader reader)
        {
            var contents = await ReadBytesFromReader(reader);
            var (TotalPuzzle, FirstPuzzleIndex) = ReadExpectedPuzzleAmount(contents);

            return DeconstructRawPuzzlesInToPuzzles(contents.AsSpan().Slice(FirstPuzzleIndex), TotalPuzzle);
        }

        private static (int TotalPuzzle, int FirstPuzzleIndex) ReadExpectedPuzzleAmount(byte[] sodokuContents)
        {
            Span<byte> fileContents = sodokuContents.AsSpan();
            int firstNewLineOcurance = fileContents.IndexOf(_newLineBytes);
            var amountOfPuzzles = Encoding.UTF8.GetString(fileContents[..firstNewLineOcurance]);

            if (!int.TryParse(amountOfPuzzles, out int parsedAmount))
            {
                return (0, 0);
            }

            return (parsedAmount, firstNewLineOcurance + _newLineBytes.Length);
        }

        private static SudokuBoard[] DeconstructRawPuzzlesInToPuzzles(Span<byte> rawPuzzles, int totalPuzzles)
        {
            SudokuBoard[] boards = new SudokuBoard[totalPuzzles];
            int totalExpectedLengthPerLine = CalculateExpectedLineLength();

            for (int i = 0; i < totalPuzzles; i++)
            {
                int slicePosition = i * totalExpectedLengthPerLine;

                Cell[,] cells = new Cell[_boundary.RowLength, _boundary.ColumnLength];
                var puzzleCells = ContructPuzzleLineIntoCells(rawPuzzles.Slice(slicePosition, _expectedPuzzleLength), ref cells);
                boards[i] = ConstructBoardFromCells(ref puzzleCells);
            }

            return boards;
        }

        private static int CalculateExpectedLineLength()
        {
            return _expectedPuzzleLength + _newLineBytes.Length;
        }

        private static ref Cell[,] ContructPuzzleLineIntoCells(Span<byte> puzzleLine, ref Cell[,] cells)
        {
            int rowPosition = 0;
            int columnPosition = 0;

            for (int i = 0; i < puzzleLine.Length; i++)
            {
                if (i > 0 && i % _boundary.RowLength == 0)
                {
                    rowPosition++;
                    columnPosition = 0;
                }

                var puzzleValue = puzzleLine[i] - _utf8NumberOfset;
                if (puzzleValue > 0)
                {
                    cells[rowPosition, columnPosition] = new Cell(puzzleValue);
                }

                ++columnPosition;
            }

            return ref cells;
        }

        private static SudokuBoard ConstructBoardFromCells(ref Cell[,] cells)
        {
            return new SudokuBoard(ref cells);
        }

        private static async Task<byte[]> ReadBytesFromReader(IReader reader)
        {
            var stream = await reader.GetStream();

            byte[] readbytes = new byte[stream.Length];
            await stream.ReadAsync(readbytes, 0, (int)stream.Length);
            return readbytes;
        }
    }
}
