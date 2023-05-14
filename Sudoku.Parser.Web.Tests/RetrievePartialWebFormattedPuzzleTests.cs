using Sudoku.Parser.Normalization;
using Sudoku.Parser.Web.Sudoku;

namespace Sudoku.Parser.Web.Tests
{
    [TestClass]
    public class RetrievePartialWebFormattedPuzzleTests
    {
        private readonly INormalize _normalizer = new HexaDecimalNormalizerWithSingleOffset();
        private RetrievePartialWebFormattedPuzzle _retriever = null;

        private readonly Cell _cell_column_4_row_1_is_e;
        private readonly string _cellValueE = "e";
        private readonly int _column4 = 4;
        private readonly int _row1 = 1;

        public RetrievePartialWebFormattedPuzzleTests()
        {
            _cell_column_4_row_1_is_e = new(_normalizer.Map(_cellValueE));
        }

        [TestInitialize]
        public void Setup()
        {
            _retriever = new RetrievePartialWebFormattedPuzzle(Puzzles.Puzzles.Sudoku_16_unsolved_nr1, _normalizer);
        }

        public void SetupInvalidPuzzle()
        {
            _retriever = new RetrievePartialWebFormattedPuzzle(string.Empty, _normalizer);
        }

        [TestMethod]
        public async Task Retriever_Load_RetunsOnePuzzle()
        {
            int expectedPuzzles = 1;

            var board = await _retriever.Load();

            Assert.IsNotNull(board);
            Assert.IsTrue(board.Any());
            Assert.AreEqual(board.Count(), expectedPuzzles);
        }

        [TestMethod]
        public async Task Retriever_Load_RetunsValidPuzzle()
        {
            var board = (await _retriever.Load()).First();
            var isBoardValid = board.IsBoardStateValid();

            Assert.IsTrue(isBoardValid);
        }

        [TestMethod]
        public async Task Retriever_Cell_2_5_hasValue_E()
        {
            var board = (await _retriever.Load()).First();

            bool areValuesEqual = board[_row1, _column4] == _cell_column_4_row_1_is_e;

            Assert.IsTrue(areValuesEqual);
        }

        [TestMethod]
        public async Task Retriever_InvalidPuzzle_NoneReturned()
        {
            SetupInvalidPuzzle();
            var boards = await _retriever.Load();

            Assert.IsNotNull(boards);
            Assert.IsTrue(!boards.Any());
        }
    }
}
