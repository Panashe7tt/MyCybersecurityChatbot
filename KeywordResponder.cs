using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCybersecurityChatbot
{
    public class KeywordResponder
    {
        private readonly Random _random = new Random();

        /// <summary>
        /// Dictionary of keywords and responses (NLP Simulation - Part 3 Requirement)
        /// Supports varied user phrasing
        /// </summary>
        private readonly Dictionary<string[], List<string>> _topicResponses = new Dictionary<string[], List<string>>
        {
            // PASSWORD
            {
                new[] { "password", "passphrase", "update password", "change password" },
                new List<string>
                {
                    "🔐 Strong Password Tips:\n" +
                    "• Minimum 12-16 characters\n" +
                    "• Mix letters, numbers, symbols\n" +
                    "• Use password manager + 2FA"
                }
            },

            // PHISHING
            {
                new[] { "phishing", "phish", "fake email", "suspicious" },
                new List<string>
                {
                    "🎣 Phishing Awareness:\n" +
                    "• Always check sender address\n" +
                    "• Hover over links before clicking\n" +
                    "• Never share personal info"
                }
            },

            // TASK / REMINDER
            {
                new[] { "add task", "remind me", "set reminder", "new task" },
                new List<string>
                {
                    "✅ Task Assistant activated!\nGo to Tasks tab or type 'add task [title]'"
                }
            },

            // QUIZ
            {
                new[] { "quiz", "start quiz", "test", "questions" },
                new List<string> { "🧠 Type 'quiz' to start the Cybersecurity Quiz!" }
            },

            // LOG
            {
                new[] { "log", "activity", "what have you done" },
                new List<string> { "📋 Type 'log' to see recent actions." }
            },

            // GENERAL
            {
                new[] { "cyber", "safety", "vpn", "backup", "2fa", "mfa" },
                new List<string>
                {
                    "🛡️ Key Cybersecurity Tips:\n" +
                    "• Use strong unique passwords\n" +
                    "• Enable 2FA/MFA\n" +
                    "• Keep software updated\n" +
                    "• Backup data (3-2-1 rule)"
                }
            }
        };

        /// <summary>
        /// Returns response based on user input keywords
        /// </summary>
        public string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            string lower = input.ToLower();

            foreach (var entry in _topicResponses)
            {
                if (entry.Key.Any(keyword => lower.Contains(keyword)))
                {
                    return entry.Value[_random.Next(entry.Value.Count)];
                }
            }

            return null;
        }
    }
}