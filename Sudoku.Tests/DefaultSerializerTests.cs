using Sudoku.Puzzles.Sets;
using Sudoku.Serialization;
using System.Text;

namespace Sudoku.Tests
{
    [TestClass]
    public class DefaultSerializerTests
    {
        private SudokuBoard _board = default!;
        private DefaultSerializer _serializer = new();

        private string set_1_solution_base64 = "ZGVmYXVsdDoJOgQHCQECAwYIBQEGAgUICQcDBAUDCAcEBgIBCQIFBAYDCAEJBwkBAwQHAgUGCAYIBwkFAQMEAgMEBQgBBwkCBgcCBgMJBAgFAQgJAQIGBQQHAw==";

        [TestInitialize]
        public void Setup()
        {
            _board = new SudokuBoard(Set_1.Solved);
            _serializer = new();
        }

        [TestMethod]
        public void Serializer_returns_correct_string()
        {
            var serializedBoard = _serializer.Serialize(_board);
            _serializer.Deserialize(serializedBoard);
            Assert.AreEqual(Encoding.Default.GetString(Convert.FromBase64String(set_1_solution_base64)), serializedBoard);
        }

    }
}
