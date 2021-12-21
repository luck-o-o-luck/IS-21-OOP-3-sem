using System;
using System.Linq;
using BackupsExtra.Models;

namespace BackupsExtra.Algorithm
{
    public class DateLimitAlgorithm : ICleaningAlgorithm
    {
        public DateLimitAlgorithm(DateTime time)
        {
            DateTime = time;
        }

        public DateTime DateTime { get; private set; }

        public void Clean(BackupJobExtra backupJobExtra)
        {
            var selectedRestorePoints =
                backupJobExtra.GetRestorePoints().Where(point => point.Date > DateTime).ToList();

            foreach (var point in selectedRestorePoints)
            {
                backupJobExtra.AddRestorePoint(point);
            }
        }

        public void ChangeDateLimit(DateTime dateTime)
        {
            DateTime = dateTime;
            Logger.LoggingInformation("Date limit was changed");
        }
    }
}