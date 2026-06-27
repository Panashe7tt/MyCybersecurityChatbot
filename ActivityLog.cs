using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCybersecurityChatbot
{
    public class ActivityLog
    {
        private readonly List<string> _logs = new();

        /// <summary>
        /// Adds an action to the activity log (Part 3 requirement)
        /// </summary>
        public void Add(string action)
        {
            _logs.Add($"[{DateTime.Now:HH:mm}] {action}");
        }

        /// <summary>
        /// Returns the last 10 actions (as required in the guide)
        /// </summary>
        public string GetLastActions()
        {
            var recent = _logs.TakeLast(10).ToList();
            if (recent.Count == 0) return "No activities yet.";

            return "📋 Activity Log (Last 10 actions):\n\n" +
                   string.Join("\n", recent.Select((log, i) => $"{i + 1}. {log}"));
        }
    }
}