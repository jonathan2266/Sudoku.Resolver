using HtmlAgilityPack;
using Sudoku.Parser.Normalization;
using Sudoku.Parser.Utilities;

namespace Sudoku.Parser.Web.Sudoku
{
    public class RetrievePartialWebFormattedPuzzle : IRetrievePuzzle
    {
        private readonly HtmlDocument _document;
        private readonly INormalize _normalization;
        private readonly UnorderedCellUtilities.Boundary _boundary = new(16);

        private const string _baseElementId = "grilleJeu";
        private const string _cellClass = "c";
        private const string _columnAttribute = "c";
        private const string _rowAttribute = "r";
        private const string _valueAttribute = "v";
        private const string _isFixedValueCellClass = "fixe";

        private readonly ValueTask<IEnumerable<SudokuBoard>> _emptyValueResultCollection = new(Enumerable.Empty<SudokuBoard>());

        public RetrievePartialWebFormattedPuzzle(string html, INormalize normalize)
        {
            _normalization = normalize;

            _document = new HtmlDocument();
            _document.LoadHtml(html);
        }

        public ValueTask<IEnumerable<SudokuBoard>> Load()
        {
            var baseElement = _document.GetElementbyId(_baseElementId);
            if (baseElement == null)
            {
                return _emptyValueResultCollection;
            }

            var cellCollection = ReadFixedCellCollectionFromBase(baseElement);
            var unorderedCellCollection = ConvertCellCollectionToUnorderedCells(cellCollection);

            var validator = UnorderedCellUtilities.FromCollection(unorderedCellCollection, _boundary);

            if (!validator.IsValidCollection())
            {
                return _emptyValueResultCollection;
            }

            return ValueTask.FromResult<IEnumerable<SudokuBoard>>(new SudokuBoard[] {
                SudokuBoard.FromOrderedCells(validator.ToOrderedCollection())
            });
        }

        private static IEnumerable<HtmlNode> ReadFixedCellCollectionFromBase(HtmlNode node)
        {
            return node.ChildNodes.Where(x => x.HasClass(_cellClass) && x.HasClass(_isFixedValueCellClass));
        }

        private IEnumerable<UnorderedCell> ConvertCellCollectionToUnorderedCells(IEnumerable<HtmlNode> cellNodes)
        {
            List<UnorderedCell> unorderedCellCollection = new(cellNodes.Count());

            foreach (var cellNode in cellNodes)
            {
                unorderedCellCollection.Add(NodeToCell(cellNode));
            }

            return unorderedCellCollection;
        }

        private UnorderedCell NodeToCell(HtmlNode node)
        {
            int nullIndexCorrection = -1;

            var column = node.GetAttributeValue<int?>(_columnAttribute, null);
            var row = node.GetAttributeValue<int?>(_rowAttribute, null);
            var value = node.GetAttributeValue<string?>(_valueAttribute, null);

            if (!column.HasValue || !row.HasValue || string.IsNullOrEmpty(value))
            {
                return UnorderedCell.InvalidCell();
            }

            return new UnorderedCell(column.Value + nullIndexCorrection, row.Value + nullIndexCorrection, _normalization.Map(value));
        }
    }
}
