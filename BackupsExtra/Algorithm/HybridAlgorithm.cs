using System;
using System.Collections.Generic;
using BackupsExtra.Models;
using BackupsExtra.Tools;
using NUnit.Framework;

namespace BackupsExtra.Algorithm
{
    public class HybridAlgorithm : ICleaningAlgorithm
    {
        private List<ICleaningAlgorithm> _algoritms = new List<ICleaningAlgorithm>();

        public void Clean(BackupJobExtra backupJobExtra)
        {
            foreach (ICleaningAlgorithm algorithm in _algoritms)
            {
                algorithm.Clean(backupJobExtra);
            }
        }

        public void AddAlgorithm(ICleaningAlgorithm algorithm)
        {
            if (algorithm is null)
                throw new BackupsExtraException("Algorithm is null");
            if (_algoritms.Count == 2)
                throw new BackupsExtraException("You can't add algorithm more than twice");
            if (_algoritms.Count != 0 && _algoritms[0].GetType() == algorithm.GetType())
                throw new BackupsExtraException("Types are equal");

            _algoritms.Add(algorithm);
        }
    }
}