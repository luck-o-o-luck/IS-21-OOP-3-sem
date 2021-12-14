using System.Linq;
using BackupsExtra.Models;
using BackupsExtra.Tools;

namespace BackupsExtra.Algorithm
{
    public class AmountPointsLimitAlgorithm : ICleaningAlgorithm
    {
        public void Clean(BackupJobExtra backupJobExtra)
        {
            if (backupJobExtra.RestorePoints.Count == backupJobExtra.AmountPointsLimit)
                throw new BackupsExtraException("You can't delete all files :(");

            int count = backupJobExtra.RestorePoints.Count - backupJobExtra.AmountPointsLimit;
            var selectedRestorePoints = backupJobExtra.RestorePoints.ToList();

            for (int i = 0; i < count; i++)
                selectedRestorePoints.Remove(backupJobExtra.RestorePoints[i]);

            backupJobExtra.ChangeRestorePoints(selectedRestorePoints);
        }
    }
}