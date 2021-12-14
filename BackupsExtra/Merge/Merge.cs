using System.Collections.Generic;
using System.Linq;
using Backups.Models;
using Backups.Tools;
using BackupsExtra.Models;
using BackupsExtra.Tools;

namespace BackupsExtra
{
    public class Merge : IMerge
    {
        public void AddPoints(BackupJobExtra backupJobExtra, List<RestorePoint> points)
        {
            foreach (RestorePoint point in points)
            {
                var selectedPoint = FindRestorePoint(point, backupJobExtra);

                if (selectedPoint is null)
                    throw new BackupsExtraException("Point is null");

                backupJobExtra.MergePoints(selectedPoint, point);
            }
        }

        private RestorePoint FindRestorePoint(RestorePoint restorePoint, BackupJobExtra backupJobExtra)
            => backupJobExtra.RestorePoints.SingleOrDefault(point => point == restorePoint);
    }
}