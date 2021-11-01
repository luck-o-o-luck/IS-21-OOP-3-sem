using System;
using System.IO;
using Backups.Algorithms;
using Backups.Models;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            const string path = @"C:\kysect";

            var repository = new LocalRepository(path);
            var algorithm = new AlgorithmSingleStorages();
            var backupJob = new BackupJob(algorithm, repository, "FirstBackup");

            var jobObject1 = new JobObject(@"C:\kysect\SoBad.txt");
            var jobObject2 = new JobObject(@"C:\kysect\PacifyHer.txt");
            backupJob.AddJobObject(jobObject1);
            backupJob.AddJobObject(jobObject2);

            backupJob.CreateBackup();
        }
    }
}
