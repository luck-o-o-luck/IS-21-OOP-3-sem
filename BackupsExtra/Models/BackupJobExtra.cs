using System;
using System.Collections.Generic;
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
            ICleaningAlgorithm cleaningAlgorithm)
            : base(algorithm, repository, name)
        {
            CleaningAlgorithm = cleaningAlgorithm ?? throw new BackupsExtraException("Cleaning algorithm is null");
        }

        public ICleaningAlgorithm CleaningAlgorithm { get; private set; }

        public void ChangeCleaningAlgorithm(ICleaningAlgorithm newCleaningAlgorithm)
        {
            CleaningAlgorithm = newCleaningAlgorithm ?? throw new BackupsExtraException("Cleaning algorithm is null");
            Logger.LoggingInformation("Algorithm was changed");
        }
    }
}