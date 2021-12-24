using System;
using System.IO;
using Backups.Models;

namespace BackupsExtra.Models
{
    public class Restore
    {
        public void RestorePoint(RestorePoint point, string path)
        {
            string name = Directory.CreateDirectory($"RestoredPoint {DateTime.Now:h:mm:ss}").FullName;

            var dirInfo = new DirectoryInfo(path);

            if (!dirInfo.Exists)
                dirInfo.Create();

            foreach (Storage storage in point.Storages)
            {
                Directory.CreateDirectory(name);
                foreach (Storage pointStorage in point.Storages)
                {
                    storage.ZipFile.Save(Path.Combine(path, storage.Path));
                }

                Logger.LoggingInformation($"{name} was restored");
            }
        }
    }
}