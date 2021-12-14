using System;
using BackupsExtra.Models;
using BackupsExtra.Tools;

namespace BackupsExtra.Algorithm
{
    public class HybridAlgorithm : ICleaningAlgorithm
    {
        public bool CleanWithAmountPointsLimit { get; private set; }
        public bool CleanWithDateLimit { get; private set; }

        public void Clean(BackupJobExtra backupJobExtra)
        {
            if (!CleanWithDateLimit && !CleanWithAmountPointsLimit)
                throw new BackupsExtraException("Can't do algorithm");

            if (CleanWithDateLimit)
            {
                var algorithm = new DateLimitAlgorithm();
                algorithm.Clean(backupJobExtra);
                CleanWithDateLimit = false;
            }

            if (CleanWithAmountPointsLimit)
            {
                var algorithm = new AmountPointsLimitAlgorithm();
                algorithm.Clean(backupJobExtra);
                CleanWithAmountPointsLimit = false;
            }
        }

        public void CleaningWithAmountPointsLimit()
        {
            CleanWithAmountPointsLimit = true;
        }

        public void CleaningWithDateLimit()
        {
            CleanWithDateLimit = true;
        }
    }
}