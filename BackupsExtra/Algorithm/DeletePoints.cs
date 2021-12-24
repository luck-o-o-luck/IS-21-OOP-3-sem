using System.Collections.Generic;
using System.Linq;
using Backups.Models;

namespace BackupsExtra.Algorithm
{
    public class DeletePoints : IRestorePointRetainer
    {
        public void Retain(IReadOnlyCollection<RestorePoint> points, BackupJob backupJob)
        {
            var deletedPoint = backupJob.RestorePoints.Where(point => !points.Contains(point)).ToList();

            foreach (RestorePoint point in deletedPoint)
            {
                backupJob.DeleteRestorePoint(point);
            }
        }
    }
}