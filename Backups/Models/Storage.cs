using System.Collections.Generic;
using Backups.Tools;
using Ionic.Zip;

namespace Backups.Models
{
    public class Storage
    {
        private List<JobObject> _jobObjects;

        public Storage(string path, ZipFile file)
        {
            if (string.IsNullOrEmpty(path))
                throw new BackupsException("Path is null");

            _jobObjects = new List<JobObject>();
            Path = path;
            ZipFile = file;
        }

        public string Path { get; }
        public ZipFile ZipFile { get; }

        public void AddJobObject(JobObject jobObject)
        {
            if (jobObject is null)
                throw new BackupsException("Job object is null");

            _jobObjects.Add(jobObject);
        }
    }
}