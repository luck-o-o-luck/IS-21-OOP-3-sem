using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Services;
using Backups.Tools;

namespace Backups.Models
{
    public class BackupJob
    {
       private int _id = 1;
       private List<JobObject> _jobObjects;
       private List<RestorePoint> _restorePoints;

       public BackupJob(IAlgorithm algorithm, IRepository repository, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new BackupsException("The name isn't entered");

            _jobObjects = new List<JobObject>();
            _restorePoints = new List<RestorePoint>();
            Algorithm = algorithm ?? throw new BackupsException("The algorithm isn't entered");
            Repository = repository ?? throw new BackupsException("The repository isn't entered");
            Name = name;
        }

       public string Name { get; }
       public IAlgorithm Algorithm { get; }
       public IRepository Repository { get; }
       public IReadOnlyList<JobObject> JobObjects => _jobObjects;
       public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;

       public void AddJobObject(JobObject jobObject)
        {
            if (jobObject is null)
                throw new BackupsException("Job doesn't exists");

            _jobObjects.Add(jobObject);
        }

       public void DeleteJobObject(JobObject jobObject)
        {
            if (jobObject is null)
                throw new BackupsException("Job object doesn't exists");
            if (_jobObjects.SingleOrDefault(job => job.FullPath.Equals(jobObject.FullPath)) is null)
                throw new BackupsException("Job object doesn't exists");

            _jobObjects.Remove(jobObject);
        }

       public void CreateBackup()
       {
           List<Storage> storages = Algorithm.CreateCopy(this, _id);
           var restorePoint = new RestorePoint(DateTime.Now, "RestorePoint", storages, _id);

           _id++;
           _restorePoints.Add(restorePoint);
           Repository.Save(this);
       }
    }
}