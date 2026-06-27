using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MyCybersecurityChatbot
{
    public class Database
    {
        private readonly string _chatFile;
        private readonly string _taskFile;

        public Database()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _chatFile = Path.Combine(path, "chat.json");
            _taskFile = Path.Combine(path, "tasks.json");
        }

        public void SaveChat(string sender, string message)
        {
            var data = LoadChatHistory();
            data.Add(new ChatRecord
            {
                Sender = sender,
                Message = message,
                Time = DateTime.Now.ToString("HH:mm")
            });

            File.WriteAllText(_chatFile, JsonConvert.SerializeObject(data, Formatting.Indented));
        }

        public List<ChatRecord> LoadChatHistory()
        {
            if (!File.Exists(_chatFile))
                return new List<ChatRecord>();

            return JsonConvert.DeserializeObject<List<ChatRecord>>(File.ReadAllText(_chatFile)) ?? new List<ChatRecord>();
        }

        public void SaveTask(string title, string description)
        {
            var data = LoadTasks();
            data.Add(new TaskRecord
            {
                Title = title,
                Description = description,
                Created = DateTime.Now.ToString("yyyy-MM-dd")
            });

            File.WriteAllText(_taskFile, JsonConvert.SerializeObject(data, Formatting.Indented));
        }

        public List<TaskRecord> LoadTasks()
        {
            if (!File.Exists(_taskFile))
                return new List<TaskRecord>();

            return JsonConvert.DeserializeObject<List<TaskRecord>>(File.ReadAllText(_taskFile)) ?? new List<TaskRecord>();
        }
    }

    // Record classes
    public class ChatRecord
    {
        public string Sender { get; set; } = "";
        public string Message { get; set; } = "";
        public string Time { get; set; } = "";
    }

    public class TaskRecord
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Created { get; set; } = "";
    }
}