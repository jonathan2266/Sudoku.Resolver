using Sudoku.Parser.Normalization;
using Sudoku.Parser.Readers;
using Sudoku.Parser.Utilities;
using Sudoku.Parser.Web.Sudoku;

namespace Sudoku.Parser.Web.Tests
{
    [TestClass]
    public class RetrievePartialWebFormattedPuzzleTests
    {
        private readonly INormalize _normalizer = new HexaDecimalNormalizerWithSingleOffset();
        private RetrievePartialWebFormattedPuzzle _retriever = default!;
        private UnorderedCellUtilities.Boundary boundary = new UnorderedCellUtilities.Boundary(16);

        private IReader _reader = ReaderFromString.CreateFromString(Puzzles.Puzzles.Sudoku_16_unsolved_nr1);
        private IReader _invalidReader = ReaderFromString.CreateFromString(string.Empty);

        private readonly Cell _cell_column_3_row_7_is_e;
        private readonly string _cellValueE = "e";
        private readonly int _column3 = 3;
        private readonly int _row7 = 7;

        public RetrievePartialWebFormattedPuzzleTests()
        {
            _cell_column_3_row_7_is_e = new(_normalizer.Map(_cellValueE));
        }

        [TestInitialize]
        public void Setup()
        {
            _retriever = new RetrievePartialWebFormattedPuzzle(_normalizer, boundary);
        }

        [TestMethod]
        public async Task Retriever_Load_RetunsOnePuzzle()
        {
            int expectedPuzzles = 1;

            var board = await _retriever.Load(_reader);

            Assert.IsNotNull(board);
            Assert.IsTrue(board.Any());
            Assert.AreEqual(board.Count(), expectedPuzzles);
        }

        [TestMethod]
        public async Task Retriever_Load_RetunsValidPuzzle()
        {
            var board = (await _retriever.Load(_reader)).First();
            var isBoardValid = board.IsBoardStateValid();

            Assert.IsTrue(isBoardValid);
        }

        [TestMethod]
        public async Task Retriever_Cell_2_5_hasValue_E()
        {
            var board = (await _retriever.Load(_reader)).First();

            bool areValuesEqual = board[_row7, _column3] == _cell_column_3_row_7_is_e;

            Assert.IsTrue(areValuesEqual);
        }

        [TestMethod]
        public async Task Retriever_InvalidPuzzle_NoneReturned()
        {
            var boards = await _retriever.Load(_invalidReader);

            Assert.IsNotNull(boards);
            Assert.IsTrue(!boards.Any());
        }
    }
}
