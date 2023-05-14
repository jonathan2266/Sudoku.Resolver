namespace Sudoku.Parser.Normalization
{
    public class HexaDecimalNormalizerWithSingleOffset : INormalize
    {
        private static readonly Dictionary<string, int> _letterToNumberLookupTable = new()
        {
            {"1",1 },
            {"2", 2 },
            {"3", 3 },
            {"4", 4 },
            {"5", 5 },
            {"6", 6 },
            {"7",7 },
            {"9", 8 },
            {"a", 9 },
            {"b", 10 },
            {"c", 11 },
            {"d", 12 },
            {"e", 13 },
            {"f", 14 },
            {"g", 15 },
        };

        public int Map(string value)
        {
            return _letterToNumberLookupTable[value];
        }
    }
}
