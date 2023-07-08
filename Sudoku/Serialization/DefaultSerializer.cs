namespace Sudoku.Serialization
{
    public class DefaultSerializer : ISerializeBoards<SudokuBoard>, IDeserializeBoards<SudokuBoard>
    {
        private readonly string _serializationPrefix = "default:";
        private readonly char _afterPrefixCharacter = ':';

        private readonly char _nullChar = '\0';

        public string Serialize(SudokuBoard board)
        {
            var totalCells = board.Size * board.Size;
            var totalSpaceRequired = CalculateTotalAllocationSpace(totalCells);

            Span<char> serializedPuzzle = stackalloc char[totalSpaceRequired];
            var afterPrefixStartpoint = AddPrefix(serializedPuzzle);
            var afterSizeStartPoint = AddPuzzleSizeAndAfterPrefix(afterPrefixStartpoint, board.Size);
            AddPuzzleData(afterSizeStartPoint, board);

            return serializedPuzzle.ToString();
        }
        public SudokuBoard Deserialize(string serializedBoard)
        {
            if (!CanDeserialize(serializedBoard))
            {
                throw new InvalidOperationException("The serialized sequence is not valid.");
            }

            var spanBoard = serializedBoard.AsSpan();

            var sizeCharacter = spanBoard.Slice(_serializationPrefix.Length);

            var puzzleSize = Convert.ToInt32(sizeCharacter[0]);
            var rawPuzzleSlice = GetStartSpanOfPuzzleData(spanBoard);

            return CreateBoardFromSequence(rawPuzzleSlice, puzzleSize);
        }

        public bool CanDeserialize(string serializedBoard)
        {
            return serializedBoard.StartsWith(_serializationPrefix);
        }

        private Span<char> AddPrefix(Span<char> destination)
        {
            _serializationPrefix.CopyTo(destination);
            return destination.Slice(_serializationPrefix.Length);
        }

        private Span<char> AddPuzzleSizeAndAfterPrefix(Span<char> destination, int totalCells)
        {
            destination[0] = Convert.ToChar(totalCells);
            destination[1] = _afterPrefixCharacter;
            return destination.Slice(2);
        }

        private void AddPuzzleData(Span<char> destination, SudokuBoard board)
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int y = 0; y < board.Size; y++)
                {
                    int offset = board.Size * y + i;
                    destination[offset] = Convert.ToChar(board[i, y].Value ?? _nullChar);
                }
            }
        }

        private int CalculateTotalAllocationSpace(int totalBoardCells)
        {
            return totalBoardCells + _serializationPrefix.Length + 2;
        }

        private ReadOnlySpan<char> GetStartSpanOfPuzzleData(ReadOnlySpan<char> serializedBoard)
        {
            int index = serializedBoard.LastIndexOf(_afterPrefixCharacter);
            return serializedBoard.Slice(index + 1);
        }

        private SudokuBoard CreateBoardFromSequence(ReadOnlySpan<char> rawBoard, int size)
        {
            Cell[,] boardCells = new Cell[size, size];

            for (int i = 0; i < boardCells.GetLength(0); i++)
            {
                for (int y = 0; y < boardCells.GetLength(1); y++)
                {
                    var charCell = rawBoard[y * size + i];
                    if (charCell != _nullChar)
                    {
                        boardCells[i, y] = new Cell(Convert.ToInt32(charCell));
                    }
                }
            }

            return new SudokuBoard(boardCells);
        }
    }
}
