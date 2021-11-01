using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Models;
using Backups.Services;
using Backups.Tools;
using Ionic.Zip;

namespace Backups.Algorithms
{
    public class AlgorithmSingleStorages : IAlgorithm
    {
        public List<Storage> CreateCopy(BackupJob backupJob, int number)
        {
            if (backupJob is null)
                throw new BackupsException("Backup job is null");

            var zip = new ZipFile();
            var storages = new List<Storage>();

            foreach (JobObject job in backupJob.JobObjects)
            {
                zip.AddFile(job.FullPath);
            }

            storages.Add(new Storage($"Archive_{number}.zip", zip));

            foreach (JobObject job in backupJob.JobObjects)
            {
                storages.Last().AddJobObject(job);
            }

            return storages;
        }
    }
}