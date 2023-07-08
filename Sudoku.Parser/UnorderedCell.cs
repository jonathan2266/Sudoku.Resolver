namespace Sudoku.Parser
{
    public record UnorderedCell
    {
        private static readonly UnorderedCell _invalidCell = new(-1, -1, -1);

        public UnorderedCell(int column, int row, int value)
        {
            Column = column;
            Row = row;
            Value = value;
        }

        public int Column { get; set; }
        public int Row { get; set; }
        public int Value { get; set; }

        public Cell ToCell()
        {
            return new Cell(Value);
        }

        public static UnorderedCell InvalidCell()
        {
            return _invalidCell;
        }
    }
}
