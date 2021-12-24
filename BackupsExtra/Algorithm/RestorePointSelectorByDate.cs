using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Models;
using BackupsExtra.Models;

namespace BackupsExtra.Algorithm
{
    public class RestorePointSelectorByDate : IRestorePointSelector
    {
        public RestorePointSelectorByDate(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        public DateTime DateTime { get; }

        public IReadOnlyCollection<RestorePoint> Select(IReadOnlyCollection<RestorePoint> sourcePoints)
        {
           var selectedRestorePoints =
                sourcePoints.Where(point => point.Date > DateTime).ToList();

           return selectedRestorePoints;
        }
    }
}