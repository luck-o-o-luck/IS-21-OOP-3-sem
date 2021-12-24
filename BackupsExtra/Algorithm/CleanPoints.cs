using System.Collections.Generic;
using System.Linq;
using Backups.Models;
using BackupsExtra.Models;

namespace BackupsExtra.Algorithm
{
    public class CleanPoints
    {
        public void Clean(BackupJobExtra backupJobExtra)
        {
            List<RestorePoint> restorePoints = new List<RestorePoint>();
            foreach (IRestorePointSelector selector in backupJobExtra.Selectors)
            {
                IReadOnlyCollection<RestorePoint> selectedPoints = selector.Select(backupJobExtra.RestorePoints);
                restorePoints = selectedPoints.Concat(selectedPoints.Where(point => !restorePoints.Contains(point))).ToList();
            }

            backupJobExtra.RestorePointRetainer.Retain(restorePoints, backupJobExtra);
        }
    }
}