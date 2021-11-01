using Backups.Models;

namespace Backups.Services
{
    public interface IRepository
    {
        public void Save(BackupJob backupJob);
    }
}