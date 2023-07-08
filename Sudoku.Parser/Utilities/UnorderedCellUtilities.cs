using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Parser.Utilities
{
    public class UnorderedCellUtilities
    {
        private readonly IEnumerable<UnorderedCell> _cells;
        private readonly Boundary _boundary;

        public UnorderedCellUtilities(IEnumerable<UnorderedCell> cells, Boundary boundary)
        {
            _cells = cells ?? throw new ArgumentNullException(nameof(cells));
            _boundary = boundary;
        }

        public bool IsValidCollection()
        {
            if (_cells.Any(x => x == UnorderedCell.InvalidCell())) return false;

            if (HasUnorderedCollectionHaveDuplicateRows(_cells) || HasUnorderedCollectionHaveDuplicateColumns(_cells))
            {
                return false;
            }

            return true;
        }

        public Cell[,] ToOrderedCollection()
        {
            if (!IsValidCollection())
            {
                throw new InvalidOperationException($"The {nameof(UnorderedCell)} structure is not valid.");
            }

            var boundary = DetermineBoundaries();
            return ProjectStructureToFixedArray(boundary);
        }

        public static UnorderedCellUtilities FromCollection(IEnumerable<UnorderedCell> cells, Boundary boundary)
        {
            return new UnorderedCellUtilities(cells, boundary);
        }

        private Cell[,] ProjectStructureToFixedArray(Boundary boundary)
        {
            Cell[,] projectedStructure = new Cell[boundary.RowLength, boundary.ColumnLength];

            foreach (var item in _cells)
            {
                projectedStructure[item.Row, item.Column] = new Cell(item.Value);
            }

            return projectedStructure;
        }

        private Boundary DetermineBoundaries()
        {
            return _boundary;
        }

        private static bool HasUnorderedCollectionHaveDuplicateRows(IEnumerable<UnorderedCell> cells)
        {
            bool hasDuplicates = false;

            foreach (var rowCollection in cells.GroupBy(x => x.Row))
            {
                var allColumns = rowCollection.Select(x => x.Column);
                var distinctColumnCount = allColumns.Distinct().Count();

                if (allColumns.Count() != distinctColumnCount)
                {
                    hasDuplicates = true;
                    break;
                }
            }

            return hasDuplicates;
        }

        private static bool HasUnorderedCollectionHaveDuplicateColumns(IEnumerable<UnorderedCell> cells)
        {
            bool hasDuplicates = false;

            foreach (var rowCollection in cells.GroupBy(x => x.Column))
            {
                var allColumns = rowCollection.Select(x => x.Row);
                var distinctColumnCount = allColumns.Distinct().Count();

                if (allColumns.Count() != distinctColumnCount)
                {
                    hasDuplicates = true;
                    break;
                }
            }

            return hasDuplicates;
        }

        public readonly struct Boundary
        {
            public Boundary(int length)
            {
                RowLength = length;
                ColumnLength = length;
            }

            public int RowLength { get; init; }
            public int ColumnLength { get; init; }
        }
    }
}
