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
            {"8",8 },
            {"9", 9 },
            {"a", 10 },
            {"b", 11 },
            {"c", 12 },
            {"d", 13 },
            {"e", 14 },
            {"f", 15 },
            {"g", 16 },
        };

        public int Map(string value)
        {
            value = value.ToLowerInvariant();
            return _letterToNumberLookupTable[value];
        }
    }
}
