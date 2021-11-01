using System.IO;
using Backups.Tools;

namespace Backups.Models
{
    public class JobObject
    {
        public JobObject(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new BackupsException("Path to file is null");

            FullPath = path;
            FileName = Path.GetFileNameWithoutExtension(path);
        }

        public string FullPath { get; }
        public string FileName { get; }
    }
}