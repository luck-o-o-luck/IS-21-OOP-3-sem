using System.Collections.Generic;
using System.Linq;
using Backups.Algorithms;
using Backups.Models;
using BackupsExtra.Models;

namespace BackupsExtra.Algorithm
{
    public class MergePoints : IRestorePointRetainer
    {
        public void Retain(IReadOnlyCollection<RestorePoint> points, BackupJob backupJob)
        {
            var algorithm = new AlgorithmSingleStorages();

            if (object.ReferenceEquals(backupJob.GetAlgorithm().GetType(), algorithm.GetType()))
            {
                foreach (RestorePoint point in points)
                {
                    backupJob.DeleteRestorePoint(point);
                }
            }

            var commonPoints = points.Where(point => backupJob.RestorePoints.Contains(point)).ToList();

            foreach (RestorePoint point in commonPoints)
            {
                RestorePoint commonPoint = backupJob.RestorePoints.Single(x => commonPoints.Contains(x));

                if (point.Storages.Count == 0)
                {
                    foreach (Storage storage in commonPoint.Storages)
                    {
                        point.AddStorage(storage);
                    }

                    backupJob.DeleteRestorePoint(commonPoint);
                    backupJob.AddRestorePoint(point);
                }

                foreach (Storage storage in commonPoint.Storages)
                {
                    if (!point.Storages.Contains(storage))
                        point.AddStorage(storage);

                    backupJob.DeleteRestorePoint(commonPoint);
                    backupJob.AddRestorePoint(point);
                }
            }
        }
    }
}