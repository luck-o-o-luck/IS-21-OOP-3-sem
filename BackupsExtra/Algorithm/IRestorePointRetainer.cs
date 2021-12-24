using System.Collections.Generic;
using Backups.Models;

namespace BackupsExtra.Algorithm
{
    public interface IRestorePointRetainer
    {
        void Retain(IReadOnlyCollection<RestorePoint> points, BackupJob backupJob);
    }
}