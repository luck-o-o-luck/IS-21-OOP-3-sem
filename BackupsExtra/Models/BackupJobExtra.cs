using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Algorithms;
using Backups.Models;
using Backups.Services;
using BackupsExtra.Algorithm;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    public class BackupJobExtra : BackupJob
    {
        private List<IRestorePointSelector> _selectors;
        public BackupJobExtra(
            IAlgorithm algorithm,
            IRepository repository,
            string name,
            IRestorePointRetainer retainer)
            : base(algorithm, repository, name)
        {
            _selectors = new List<IRestorePointSelector>();
            RestorePointRetainer = retainer ?? throw new BackupsExtraException("Retainer can't be null");
        }

        public IRestorePointRetainer RestorePointRetainer { get; private set; }
        public List<IRestorePointSelector> Selectors => _selectors;

        public void AddSelector(IRestorePointSelector selector)
        {
            if (selector is null)
                throw new BackupsExtraException("Selector can't be null");

            _selectors.Add(selector);
        }

        public void DeleteSelector(IRestorePointSelector selector)
        {
            if (selector is null)
                throw new BackupsExtraException("Selector can't be null");

            _selectors.Remove(selector);
        }

        public void ChangeRetainer(IRestorePointRetainer retainer)
        {
            RestorePointRetainer = retainer ?? throw new BackupsExtraException("Retainer can't be null");
        }
    }
}