using Backups.Algorithms;
using Backups.Models;
using Backups.Services;
using Backups.Tools;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTests
    {
        [Test]
        [Ignore("Ignore a test")]
        public void CreateBackupJob_CheckCountRestorePointsAndStorages()
        {
            const string path = @"C:\kysect";

            var repository = new LocalRepository(path);
            var algorithm = new AlgorithmSplitStorages();
            var backupJob = new BackupJob(algorithm, repository, "FirstBackup");

            var jobObject1 = new JobObject(@"C:\kysect\SoBad.txt");
            var jobObject2 = new JobObject(@"C:\kysect\PacifyHer.txt");
            backupJob.AddJobObject(jobObject1);
            backupJob.AddJobObject(jobObject2);

            backupJob.CreateBackup();
            
            backupJob.DeleteJobObject(jobObject1);
            
            backupJob.CreateBackup();
            
            Assert.AreEqual(backupJob.RestorePoints.Count, 2);
            Assert.AreEqual(backupJob.RestorePoints[0].Storages.Count +
                            backupJob.RestorePoints[1].Storages.Count, 3);
        }
    }
}