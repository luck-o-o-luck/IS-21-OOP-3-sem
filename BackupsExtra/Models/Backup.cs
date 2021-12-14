using System.Collections.Generic;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    public class Backup
    {
        private List<BackupJobExtra> _backupJobs;

        public Backup()
        {
            _backupJobs = new List<BackupJobExtra>();
        }

        public string Name { get; } = "Backup";
        public IReadOnlyList<BackupJobExtra> BackupJobExtras => _backupJobs;

        public void DeleteBackupJob(BackupJobExtra backupJob)
        {
            if (backupJob is null)
                throw new BackupsExtraException("Backup job is null");

            _backupJobs.Remove(backupJob);
        }

        public void AddBackupJob(BackupJobExtra backupJob)
        {
            if (backupJob is null)
                throw new BackupsExtraException("Backup job is null");

            _backupJobs.Add(backupJob);
        }
    }
}