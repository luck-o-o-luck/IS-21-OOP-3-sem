using System.Collections.Generic;
using Backups.Models;
using BackupsExtra.Models;

namespace BackupsExtra
{
    public interface IMerge
    {
        void AddPoints(BackupJobExtra backupJobExtra, List<RestorePoint> points);
    }
}