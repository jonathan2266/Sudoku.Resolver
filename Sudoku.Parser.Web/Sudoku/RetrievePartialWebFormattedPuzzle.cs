using HtmlAgilityPack;
using Sudoku.Parser.Normalization;
using Sudoku.Parser.Readers;
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
        private const string _rowAttribute = "l";
        private const string _valueAttribute = "v";
        private const string _isFixedValueCellClass = "fixe";

        public RetrievePartialWebFormattedPuzzle(INormalize normalize, UnorderedCellUtilities.Boundary boundary)
        {
            _normalization = normalize;
            _boundary = boundary;
            _document = new HtmlDocument();
        }

        public async Task<IEnumerable<SudokuBoard>> Load(IReader reader)
        {
            var stream = await reader.GetStream();
            _document.Load(stream, reader.StreamEncoding);

            var baseElement = _document.GetElementbyId(_baseElementId);
            if (baseElement == null)
            {
                return Enumerable.Empty<SudokuBoard>();
            }
            
            var cellCollection = ReadFixedCellCollectionFromBase(baseElement);
            var unorderedCellCollection = ConvertCellCollectionToUnorderedCells(cellCollection);

            var validator = UnorderedCellUtilities.FromCollection(unorderedCellCollection, _boundary);

            if (!validator.IsValidCollection())
            {
                return Enumerable.Empty<SudokuBoard>();
            }

            return new SudokuBoard[] {
                SudokuBoard.FromOrderedCells(validator.ToOrderedCollection())
            };
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
