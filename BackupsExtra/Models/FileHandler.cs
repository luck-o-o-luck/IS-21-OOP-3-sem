using System.IO;
using BackupsExtra.Tools;
using Newtonsoft.Json;

namespace BackupsExtra.Models
{
    public class FileHandler
    {
        private JsonSerializerSettings _serializerSettings;
        public FileHandler(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new BackupsExtraException("Path is null");

            PathForFile = Path.Combine(path, "Data.txt");
            _serializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
            };
        }

        public string PathForFile { get; private set; }

        public void SaveDataStore(Backup backup)
        {
            string jsonString = JsonConvert.SerializeObject(backup, _serializerSettings);
            File.WriteAllText(PathForFile, jsonString);
        }

        public Backup LoadDataStore()
        {
            return JsonConvert.DeserializeObject<Backup>(File.ReadAllText(PathForFile), _serializerSettings);
        }

        public void ChangePathForFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new BackupsExtraException("New path is null");

            PathForFile = path;
        }
    }
}