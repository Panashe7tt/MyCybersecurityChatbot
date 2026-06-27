using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace MyCybersecurityChatbot
{
    public class ChatBot
    {
        private readonly KeywordResponder _keywords;
        private readonly SentimentDetector _sentiment;
        private readonly TaskManager _tasks;
        private readonly QuizManager _quiz;
        private readonly ActivityLog _log;
        private readonly Database _db;

        private bool _awaitingName = true;
        private string _userName = "User";

        public ChatBot()
        {
            _keywords = new KeywordResponder();
            _sentiment = new SentimentDetector();
            _tasks = new TaskManager();
            _quiz = new QuizManager();
            _log = new ActivityLog();
            _db = new Database();
        }

        /// <summary>
        /// Main input processor - Handles NLP, commands, and routes to features (Part 3 Core)
        /// </summary>
        public string ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please enter a message.";

            string text = input.ToLower().Trim();

            // Sentiment response
            string sentimentResponse = _sentiment.GetResponse(_sentiment.Detect(input));

            // Activity Log Command
            if (text.Contains("log") || text.Contains("what have you done") || text.Contains("history"))
                return _log.GetLastActions();

            // Quiz Commands
            if (text.Contains("quiz") || text.Contains("start quiz") || text.Contains("test me"))
            {
                _quiz.ResetQuiz();
                _log.Add("Quiz started");
                return "🧠 Quiz Started!\n\n" + _quiz.GetCurrentQuestion();
            }

            // Quiz Answer (a/b/c/d)
            if (text.Length == 1 && text[0] >= 'a' && text[0] <= 'd')
            {
                int answer = text[0] - 'a';
                string result = _quiz.SubmitAnswer(answer);
                _log.Add($"Quiz answered: {text.ToUpper()}");

                if (_quiz.HasMoreQuestions())
                    return result + "\n\n" + _quiz.GetCurrentQuestion();

                _log.Add("Quiz completed");
                return result + "\n\n" + _quiz.GetFinalResult();
            }

            // Task / Reminder Command
            if (text.Contains("add task") || text.Contains("remind me") || text.Contains("new task"))
            {
                _log.Add("Task added via chat");
                return "Task added! Go to Tasks tab to manage reminders.";
            }

            // User Name
            if (_awaitingName)
            {
                _awaitingName = false;
                _userName = input;
                _log.Add("User name set");
                return $"Nice to meet you, {_userName} 👋";
            }

            // Keyword-based NLP Response
            string keywordResponse = _keywords.GetResponse(input);
            string response = keywordResponse ?? "Try: quiz, add task, log, password, phishing...";

            string finalResponse = sentimentResponse + "\n\n" + response;

            // Save chat history
            _db.SaveChat("User", input);
            _db.SaveChat("Bot", finalResponse);

            return finalResponse;
        }

        // Public methods for UI
        public void RefreshTaskList(ListBox listBox) => _tasks.RefreshTasksList(listBox);
        public void ToggleTask(int id) => _tasks.ToggleTask(id);

        public List<ChatRecord> GetChatHistory() => _db.LoadChatHistory();

        public string GetStats()
        {
            var chats = _db.LoadChatHistory();
            return $"📊 Stats\nMessages: {chats.Count}";
        }
    }
}