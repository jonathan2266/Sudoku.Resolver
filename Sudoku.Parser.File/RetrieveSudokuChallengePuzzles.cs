﻿using Sudoku.Parser.Readers;
using Sudoku.Parser.Utilities;

namespace Sudoku.Parser.File
{
    public class RetrieveSudokuChallengePuzzles : IRetrievePuzzle
    {
        private readonly UnorderedCellUtilities.Boundary _boundary = new(9); //puzzles are in 9 size

        private readonly int _totalLineLength;

        public RetrieveSudokuChallengePuzzles()
        {
            _totalLineLength = _boundary.ColumnLength * _boundary.RowLength;
        }

        public async Task<IEnumerable<SudokuBoard>> Load(IReader reader)
        {
            using var streamReader = new StreamReader(await reader.GetStream());

            int totalPuzzles = await ReadAmountOfPuzzlesInFile(streamReader);

            var boards = await ExtractBoardsFromReader(streamReader);

            if (!ReturnedExpectedBoards(totalPuzzles, boards))
            {
                return Enumerable.Empty<SudokuBoard>();
            }

            return boards;
        }

        private async Task<int> ReadAmountOfPuzzlesInFile(TextReader reader)
        {
            var firstLine = await reader.ReadLineAsync();
            return int.Parse(firstLine);
        }

        private async Task<IEnumerable<SudokuBoard>> ExtractBoardsFromReader(TextReader reader)
        {
            bool continueReading = true;

            List<SudokuBoard> boards = new();

            while (continueReading)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                var collection = ToCellCollection(line);
                var board = MapCollectionToBoard(collection);
                if (board != null)
                {
                    boards.Add(board);
                }

            }

            return boards;
        }

        private IReadOnlyCollection<UnorderedCell> ToCellCollection(string line)
        {
            if (!IsLineExpectedSize(line))
            {
                return Array.Empty<UnorderedCell>();
            }

            List<UnorderedCell> collection = new List<UnorderedCell>();

            int column = 0;
            int row = 0;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != '0')
                {
                    collection.Add(ToCell(row, column, line[i]));
                }

                if (_boundary.RowLength == column + 1)
                {
                    column = 0;
                    row += 1;
                }
                else
                {
                    ++column;
                }
            }

            return collection;
        }

        private static UnorderedCell ToCell(int row, int column, char element)
        {
            return new UnorderedCell(column, row, (int)char.GetNumericValue(element));
        }

        private bool IsLineExpectedSize(string line)
        {
            return line.Length == _totalLineLength;
        }

        private static bool ReturnedExpectedBoards(int expectedCount, IEnumerable<SudokuBoard> recievedBoards)
        {
            return (recievedBoards.Count() == expectedCount);
        }

        private SudokuBoard? MapCollectionToBoard(IEnumerable<UnorderedCell> cells)
        {
            var utility = UnorderedCellUtilities.FromCollection(cells, _boundary);

            if (!utility.IsValidCollection())
            {
                return null;
            }

            return SudokuBoard.FromOrderedCells(utility.ToOrderedCollection());

        }
    }
}
