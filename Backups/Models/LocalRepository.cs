using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Services;
using Backups.Tools;

namespace Backups.Models
{
    public class LocalRepository : IRepository
    {
        public LocalRepository(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new BackupsException("Path is null");

            FullPathRepository = path;
            DirectoryInfo = Directory.CreateDirectory(FullPathRepository);
        }

        public string FullPathRepository { get; }
        public DirectoryInfo DirectoryInfo { get; }

        public void Save(BackupJob backupJob)
        {
            if (backupJob is null)
                throw new BackupsException("Backup job is null");

            string path = Path.Combine(DirectoryInfo.FullName, backupJob.Name, backupJob.RestorePoints.Last().Name);

            var dirInfo = new DirectoryInfo(path);

            if (!dirInfo.Exists)
                dirInfo.Create();

            foreach (Storage storage in backupJob.RestorePoints.Last().Storages)
            {
                storage.ZipFile.Save(Path.Combine(path, storage.Path));
            }
        }
    }
}