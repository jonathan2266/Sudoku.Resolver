using System.Text;

namespace Sudoku.Parser.Readers
{
    public class ReaderFromString : IReader
    {
        private readonly string _text;
        private Encoding _encoding = Encoding.UTF8;

        public ReaderFromString(string text)
        {
            _text = text;
        }

        public Task<Stream> GetStream()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(_text);
            writer.Flush();
            stream.Position = 0;
            return Task.FromResult<Stream>(stream);
        }

        public Encoding StreamEncoding { get { return _encoding; } }

        public static IReader CreateFromString(string text)
        {
            return new ReaderFromString(text);
        }
    }
}
