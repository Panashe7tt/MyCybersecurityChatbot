using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyCybersecurityChatbot
{
    public partial class MainWindow : Window
    {
        private readonly ChatBot _bot;
        private TextBlock? _typingIndicator;

        public MainWindow()
        {
            InitializeComponent();
            _bot = new ChatBot();

            ShowChat(null, null);
            AddWelcomeMessage();

            // Play greeting audio on startup (Part 2 requirement)
            Task.Run(() => Audio.PlayGreeting());
        }

        // ================= CHAT BUBBLES =================
        private void AddChatBubble(string message, bool isUser)
        {
            StackPanel container = new StackPanel
            {
                Margin = new Thickness(5),
                HorizontalAlignment = isUser ? HorizontalAlignment.Right : HorizontalAlignment.Left
            };

            Border bubble = new Border
            {
                CornerRadius = new CornerRadius(12),
                Padding = new Thickness(12),
                MaxWidth = 520,
                Background = isUser ? Brushes.DodgerBlue : new SolidColorBrush(Color.FromRgb(45, 45, 70))
            };

            TextBlock text = new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap
            };

            bubble.Child = text;
            container.Children.Add(bubble);
            ChatPanel.Children.Add(container);
        }

        private void AddWelcomeMessage()
        {
            AddChatBubble("Hello! I'm Nash Bot. What’s your name?", false);
        }

        // ================= SEND MESSAGE (Core Interaction) =================
        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserInput.Text)) return;

            string msg = UserInput.Text.Trim();
            AddChatBubble(msg, true);
            UserInput.Clear();

            ShowTypingIndicator();
            await Task.Delay(600);
            HideTypingIndicator();

            string response = _bot.ProcessInput(msg);
            AddChatBubble(response, false);
        }

        // ================= TYPING INDICATOR =================
        private void ShowTypingIndicator()
        {
            _typingIndicator = new TextBlock
            {
                Text = "Nash is typing...",
                Foreground = Brushes.Gray,
                Margin = new Thickness(5)
            };
            ChatPanel.Children.Add(_typingIndicator);
        }

        private void HideTypingIndicator()
        {
            if (_typingIndicator != null)
                ChatPanel.Children.Remove(_typingIndicator);
        }

        // ================= TASK ASSISTANT (Part 3) =================
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskTitleInput.Text))
            {
                MessageBox.Show("Please enter a task title.");
                return;
            }

            string title = TaskTitleInput.Text.Trim();
            string desc = TaskDescInput.Text.Trim();

            // Reminder detection (NLP simulation)
            DateTime? reminder = null;
            string lower = desc.ToLower();
            if (lower.Contains("tomorrow")) reminder = DateTime.Now.AddDays(1);
            else if (lower.Contains("3 days")) reminder = DateTime.Now.AddDays(3);
            else if (lower.Contains("week")) reminder = DateTime.Now.AddDays(7);

            var taskManager = new TaskManager();
            taskManager.AddTask(title, desc, reminder);

            _bot.ProcessInput("add task " + title); // For activity log

            MessageBox.Show("Task added successfully!" +
                (reminder.HasValue ? $"\nReminder set: {reminder.Value:yyyy-MM-dd}" : ""));

            TaskTitleInput.Clear();
            TaskDescInput.Clear();

            _bot.RefreshTaskList(TasksListBox);
        }

        // ================= QUIZ =================
        private void QuizAnswer_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Tag == null || !int.TryParse(btn.Tag.ToString(), out int index)) return;

            string letter = ((char)('a' + index)).ToString();
            string result = _bot.ProcessInput(letter);
            QuizQuestionText.Text = result;
        }

        // ================= NAVIGATION =================
        private void ShowChat(object sender, RoutedEventArgs e) { HideAll(); ChatView.Visibility = Visibility.Visible; }
        private void ShowTasks(object sender, RoutedEventArgs e)
        {
            HideAll();
            TaskView.Visibility = Visibility.Visible;
            _bot.RefreshTaskList(TasksListBox);
        }
        private void ShowQuiz(object sender, RoutedEventArgs e)
        {
            HideAll();
            QuizView.Visibility = Visibility.Visible;
            QuizQuestionText.Text = _bot.ProcessInput("start quiz");
        }
        private void ShowLog(object sender, RoutedEventArgs e)
        {
            HideAll();
            LogTextBox.Visibility = Visibility.Visible;
            LogTextBox.Text = _bot.ProcessInput("log");
        }
        /// <summary>
        //
        

        private void HideAll()
        {
            ChatView.Visibility = Visibility.Collapsed;
            TaskView.Visibility = Visibility.Collapsed;
            QuizView.Visibility = Visibility.Collapsed;
            LogTextBox.Visibility = Visibility.Collapsed;
        }
    }
}