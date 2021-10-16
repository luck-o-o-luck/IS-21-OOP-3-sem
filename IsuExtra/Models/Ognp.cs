using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class OGNP
    {
        private List<StreamOGNP> _streams;

        public OGNP(string name, char faculty)
        {
            if (string.IsNullOrEmpty(name))
                throw new IsuExtraException("String is null or empty");

            NameOGNP = name;
            Faculty = faculty;
            _streams = new List<StreamOGNP>();
        }

        public char Faculty { get; }
        public string NameOGNP { get; }
        public IReadOnlyList<StreamOGNP> Streams => _streams;

        public void AddStreamOfOGNP(StreamOGNP stream)
        {
            if (stream is null)
                throw new IsuExtraException("Stream is null");

            _streams.Add(stream);
        }
    }
}