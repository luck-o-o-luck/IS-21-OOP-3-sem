using BackupsExtra.Models;

namespace BackupsExtra.Algorithm
{
    public interface ICleaningAlgorithm
    {
        void Clean(BackupJobExtra backupJobExtra);
    }
}