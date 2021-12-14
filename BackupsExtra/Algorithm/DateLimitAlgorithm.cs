using System.Linq;
using BackupsExtra.Models;

namespace BackupsExtra.Algorithm
{
    public class DateLimitAlgorithm : ICleaningAlgorithm
    {
        public void Clean(BackupJobExtra backupJobExtra)
        {
            var selectedRestorePoints =
                backupJobExtra.RestorePoints.Where(point => point.Date > backupJobExtra.DateLimit).ToList();

            backupJobExtra.ChangeRestorePoints(selectedRestorePoints);
        }
    }
}