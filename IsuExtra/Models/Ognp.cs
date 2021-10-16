using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Ognp
    {
        private List<StreamOgnp> _streams;

        public Ognp(string name, char faculty)
        {
            if (string.IsNullOrEmpty(name))
                throw new IsuExtraException("String is null or empty");

            NameOGNP = name;
            Faculty = faculty;
            _streams = new List<StreamOgnp>();
        }

        public char Faculty { get; }
        public string NameOGNP { get; }
        public IReadOnlyList<StreamOgnp> Streams => _streams;

        public void AddStreamOfOGNP(StreamOgnp stream)
        {
            if (stream is null)
                throw new IsuExtraException("Stream is null");

            _streams.Add(stream);
        }
    }
}