using System;
using System.Linq;
using Backups.Algorithms;
using Backups.Models;
using Backups.Services;
using BackupsExtra.Algorithm;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    public class BackupJobExtra : BackupJob
    {
        public BackupJobExtra(
            IAlgorithm algorithm,
            IRepository repository,
            string name,
            ICleaningAlgorithm cleaningAlgorithm,
            DateTime date,
            int count)
            : base(algorithm, repository, name)
        {
            if (count < 0)
                throw new BackupsExtraException("Count can't be null");

            DateLimit = date;
            AmountPointsLimit = count;
            CleaningAlgorithm = cleaningAlgorithm ?? throw new BackupsExtraException("Cleaning algorithm is null");
        }

        public ICleaningAlgorithm CleaningAlgorithm { get; private set; }
        public DateTime DateLimit { get; private set; }
        public int AmountPointsLimit { get; private set; }

        public void ChangeCleaningAlgorithm(ICleaningAlgorithm newCleaningAlgorithm)
        {
            CleaningAlgorithm = newCleaningAlgorithm ?? throw new BackupsExtraException("Cleaning algorithm is null");
            Logger.LoggingInformation("Algorithm was changed");
        }

        public void ChangeDateLimit(DateTime dateTime)
        {
            DateLimit = dateTime;
            Logger.LoggingInformation("Date limit was changed");
        }

        public void ChangeAmountPointsLimit(int amount)
        {
            if (amount < 0)
                throw new BackupsExtraException("Amount can't be less than zero");

            AmountPointsLimit = amount;
            Logger.LoggingInformation("Amount was changed");
        }

        public void MergePoints(RestorePoint removesPoint, RestorePoint mergesPoint)
        {
            var algorithm = new AlgorithmSingleStorages();
            if (object.ReferenceEquals(Algorithm.GetType(), algorithm.GetType()))
            {
                DeleteRestorePoint(removesPoint);
            }

            var selectedStorages = removesPoint.Storages.Where(storage => !mergesPoint.Storages.Contains(storage));

            foreach (Storage storage in selectedStorages)
            {
                mergesPoint.AddStorage(storage);
            }

            Logger.LoggingInformation("Algorithm was merged");
        }
    }
}