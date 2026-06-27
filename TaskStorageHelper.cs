using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MyCybersecurityChatbot
{
    public class TaskStorageHelper
    {
        private readonly string _filePath;

        public TaskStorageHelper()
        {
            _filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "CyberTasks.json");
        }

        public void SaveTasks(List<CyberTask> tasks)
        {
            try
            {
                string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch { }
        }

        public List<CyberTask> LoadTasks()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = File.ReadAllText(_filePath);
                    return JsonConvert.DeserializeObject<List<CyberTask>>(json) ?? new List<CyberTask>();
                }
            }
            catch { }

            return new List<CyberTask>();
        }
    }
}