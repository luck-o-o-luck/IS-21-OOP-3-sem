using System;
using System.Collections.Generic;
using Backups.Tools;

namespace Backups.Models
{
    public class RestorePoint
    {
        private List<Storage> _storages;

        public RestorePoint(DateTime date, string name, List<Storage> storages, int id)
        {
            if (string.IsNullOrEmpty(name))
                throw new BackupsException("Name is null");
            if (storages.Count == 0)
                throw new BackupsException("You don't have files for saving");

            Date = date;
            _storages = storages;
            Name = name + $"_{id}";
        }

        public string Name { get; }
        public DateTime Date { get; }
        public IReadOnlyList<Storage> Storages => _storages;

        public void AddStorage(Storage storage)
        {
            if (storage is null)
                throw new BackupsException("Storage is null");

            _storages.Add(storage);
        }
    }
}