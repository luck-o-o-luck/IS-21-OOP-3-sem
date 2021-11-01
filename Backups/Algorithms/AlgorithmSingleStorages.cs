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

            foreach (JobObject jobObject in backupJob.JobObjects)
            {
                zip.AddFile(jobObject.FullPath);
            }

            var storage = new Storage($"Archive_{number}.zip", zip);

            foreach (JobObject jobObject in backupJob.JobObjects)
            {
                storage.AddJobObject(jobObject);
            }

            storages.Add(storage);

            return storages;
        }
    }
}