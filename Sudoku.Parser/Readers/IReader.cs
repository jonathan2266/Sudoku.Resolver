using System.Text;

namespace Sudoku.Parser.Readers
{
    public interface IReader
    {
        Task<Stream> GetStream();
        Encoding StreamEncoding { get; }
    }
}
