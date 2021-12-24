using System.Collections.Generic;
using Backups.Models;

namespace BackupsExtra.Algorithm
{
    public interface IRestorePointSelector
    {
        IReadOnlyCollection<RestorePoint> Select(IReadOnlyCollection<RestorePoint> sourcePoints);
    }
}