using System.Linq;
using BackupsExtra.Models;
using BackupsExtra.Tools;

namespace BackupsExtra.Algorithm
{
    public class AmountPointsLimitAlgorithm : ICleaningAlgorithm
    {
        public AmountPointsLimitAlgorithm(int count)
        {
            Count = count;
        }

        public int Count { get; private set; }

        public void Clean(BackupJobExtra backupJobExtra)
        {
            if (backupJobExtra.GetRestorePoints().Count == Count)
                throw new BackupsExtraException("You can't delete all files :(");

            int count = backupJobExtra.GetRestorePoints().Count - Count;
            var points = backupJobExtra.GetRestorePoints().Take(count).ToList();

            var delete = new MergePoints();
            delete.Merge(points, backupJobExtra);
        }

        public void ChangeAmountPointsLimit(int amount)
        {
            if (amount < 0)
                throw new BackupsExtraException("Amount can't be less than zero");

            Count = amount;
            Logger.LoggingInformation("Amount was changed");
        }
    }
}