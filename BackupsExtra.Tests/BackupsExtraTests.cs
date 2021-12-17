using System;
using Backups.Algorithms;
using Backups.Models;
using BackupsExtra.Algorithm;
using BackupsExtra.Models;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTests
    {
        [Test]
        [Ignore("Ignore")]
        public void CreateBackupJob_CheckCountRestorePointsAndStorages()
        {
            var backup = new Backup();
            const string path = @"C:\kysect";

            var repository = new LocalRepository(path);
            var algorithm = new AlgorithmSingleStorages();
            var hybridAlgorithm = new HybridAlgorithm();
            hybridAlgorithm.AddAlgorithm(new AmountPointsLimitAlgorithm());
            var backupJob = new BackupJobExtra(algorithm, repository, "FirstBackup", hybridAlgorithm, new DateTime(2021, 12, 14), 1);

            var jobObject1 = new JobObject(@"C:\kysect\SoBad.txt");
            var jobObject2 = new JobObject(@"C:\kysect\PacifyHer.txt");
            backupJob.AddJobObject(jobObject1);
            backupJob.AddJobObject(jobObject2);
            backup.AddBackupJob(backupJob);
            backupJob.CreateBackup();
            backupJob.CreateBackup();

            backupJob.CleaningAlgorithm.Clean(backupJob);
            Assert.AreEqual(backupJob.RestorePoints.Count, 1);
        }
    }
}