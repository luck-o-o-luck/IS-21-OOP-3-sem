using System.Collections.Generic;
using Backups.Models;

namespace Backups.Services
{
    public interface IAlgorithm
    {
        List<Storage> CreateCopy(BackupJob backupJob, int number);
    }
}