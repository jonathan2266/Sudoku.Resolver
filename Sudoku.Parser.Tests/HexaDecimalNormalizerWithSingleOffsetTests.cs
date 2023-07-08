using Sudoku.Parser.Normalization;

namespace Sudoku.Parser.Tests
{
    [TestClass]
    public class HexaDecimalNormalizerWithSingleOffsetTests
    {
        private readonly INormalize _normalizer = new HexaDecimalNormalizerWithSingleOffset();

        private const string _hexedecimalA = "A";
        private const int _hexedecimalANumber = 10;

        [TestMethod]
        public void HexaDecimalNormalizer_MapA_Returns10()
        {
            var mappedValue = _normalizer.Map(_hexedecimalA);

            Assert.AreEqual(mappedValue, _hexedecimalANumber);
        }


    }
}