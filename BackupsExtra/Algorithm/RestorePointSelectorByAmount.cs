using System.Collections.Generic;
using System.Linq;
using Backups.Models;
using BackupsExtra.Tools;

namespace BackupsExtra.Algorithm
{
    public class RestorePointSelectorByAmount : IRestorePointSelector
    {
        public RestorePointSelectorByAmount(int count)
        {
            if (count < 0)
                throw new BackupsExtraException("Count can be less than zero");

            Count = count;
        }

        public int Count { get; }

        public IReadOnlyCollection<RestorePoint> Select(IReadOnlyCollection<RestorePoint> sourcePoints)
        {
            sourcePoints = sourcePoints.ToList();
            int count = sourcePoints.Count - Count;
            var selectedPoints = sourcePoints.Take(count).ToList();

            return selectedPoints;
        }
    }
}