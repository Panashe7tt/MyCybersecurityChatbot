using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace MyCybersecurityChatbot
{
    public class TaskManager
    {
        private readonly List<CyberTask> _tasks = new();
        private readonly TaskStorageHelper _storage = new();
        private int _nextId = 1;

        public TaskManager()
        {
            var loaded = _storage.LoadTasks();
            _tasks.AddRange(loaded);

            if (_tasks.Count > 0)
                _nextId = _tasks.Max(t => t.Id) + 1;
        }

        public void AddTask(string title, string description, System.DateTime? reminder)
        {
            _tasks.Add(new CyberTask
            {
                Id = _nextId++,
                Title = title,
                Description = description,
                ReminderDate = reminder,
                IsCompleted = false
            });

            _storage.SaveTasks(_tasks);
        }

        public void ToggleTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                _storage.SaveTasks(_tasks);
            }
        }

        public void DeleteTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsDeleted = true;
                _storage.SaveTasks(_tasks);
            }
        }

        public void RefreshTasksList(ListBox listBox)
        {
            listBox.Items.Clear();

            foreach (var t in _tasks.Where(t => !t.IsDeleted))
            {
                string status = t.IsCompleted ? "✅ Done" : "⏳ Pending";
                string reminderInfo = t.ReminderDate.HasValue
                    ? $" | Reminder: {t.ReminderDate.Value:yyyy-MM-dd}"
                    : "";

                listBox.Items.Add($"[{t.Id}] {t.Title} - {status}{reminderInfo}");
            }
        }

        public List<CyberTask> GetAllTasks() => _tasks.Where(t => !t.IsDeleted).ToList();
    }
}