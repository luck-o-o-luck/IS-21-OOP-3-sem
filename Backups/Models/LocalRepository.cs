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

            Path = path;
            DirectoryInfo = Directory.CreateDirectory(Path);
        }

        public string Path { get; }
        public DirectoryInfo DirectoryInfo { get; }

        public void Save(BackupJob backupJob)
        {
            if (backupJob is null)
                throw new BackupsException("Backup job is null");

            var dirInfo = new DirectoryInfo(DirectoryInfo.FullName + "/" + backupJob.Name + "/" +
                                            backupJob.RestorePoints.Last().Name);

            if (!dirInfo.Exists)
                dirInfo.Create();

            foreach (Storage storage in backupJob.RestorePoints.Last().Storages)
            {
                storage.ZipFile.Save(dirInfo + "/" + storage.Path);
            }
        }
    }
}