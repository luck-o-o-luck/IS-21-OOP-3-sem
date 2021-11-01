using System.Collections.Generic;
using Backups.Models;
using Backups.Services;
using Backups.Tools;
using Ionic.Zip;

namespace Backups.Algorithms
{
    public class AlgorithmSplitStorages : IAlgorithm
    {
        public List<Storage> CreateCopy(BackupJob backupJob, int number)
        {
            if (backupJob is null)
                throw new BackupsException("Backup job is null");

            var storages = new List<Storage>();

            foreach (JobObject job in backupJob.JobObjects)
            {
                var zip = new ZipFile();
                zip.AddFile(job.FullPath);
                var storage = new Storage(job.FileName + $"_{number}.zip", zip);
                storage.AddJobObject(job);
                storages.Add(storage);
            }

            return storages;
        }
    }
}