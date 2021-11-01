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

            foreach (JobObject jobObject in backupJob.JobObjects)
            {
                var zip = new ZipFile();
                zip.AddFile(jobObject.FullPath);
                var storage = new Storage(jobObject.FileName + $"_{number}.zip", zip);
                storage.AddJobObject(jobObject);
                storages.Add(storage);
            }

            return storages;
        }
    }
}