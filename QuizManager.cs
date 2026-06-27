using System.Collections.Generic;

namespace MyCybersecurityChatbot
{
    public class QuizManager
    {
        private readonly List<QuizQuestion> _questions = new();
        private int _currentIndex = 0;
        private int _score = 0;

        public QuizManager()
        {
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            _questions.Clear();

            _questions.Add(new QuizQuestion("What is Phishing?",
                "A type of fish", "Fake email to steal info", "A firewall", "A password", 1,
                "Phishing is a social engineering attack."));

            _questions.Add(new QuizQuestion("What does 2FA mean?",
                "Two Fake Accounts", "Two Factor Authentication", "Too Fast Access", "Two Firewall Alert", 1,
                "2FA adds an extra layer of security."));

            _questions.Add(new QuizQuestion("Best way to store passwords?",
                "Write on paper", "Same password everywhere", "Use a password manager", "Save in browser", 2,
                "Password managers are the safest option."));

            _questions.Add(new QuizQuestion("What should you do with suspicious email?",
                "Click the link", "Reply with password", "Report as phishing", "Forward to friends", 2,
                "Never click links or reply with personal info."));

            _questions.Add(new QuizQuestion("What is Ransomware?",
                "A game", "Malware that encrypts files", "A strong password", "A firewall", 1,
                "Ransomware locks your files and demands payment."));

            _questions.Add(new QuizQuestion("What is Social Engineering?",
                "Hacking code", "Manipulating people", "Building firewalls", "Virus scanning", 1,
                "Social engineering tricks people into giving away info."));

            _questions.Add(new QuizQuestion("Best practice for public Wi-Fi?",
                "Use it freely", "Use a VPN", "Share password", "Disable firewall", 1,
                "Always use a VPN on public networks."));

            _questions.Add(new QuizQuestion("What does GDPR stand for?",
                "General Data Protection Regulation", "Global Data Privacy Rules", "Good Data Protection", "General Digital Protection", 0,
                "GDPR protects personal data privacy."));

            _questions.Add(new QuizQuestion("What should you do if you get a suspicious link?",
                "Click it", "Ignore and delete", "Reply asking for more info", "Forward to friends", 1,
                "Never click suspicious links."));

            _questions.Add(new QuizQuestion("What is a strong password example?",
                "password123", "MyName2023", "Tr0ub4dor&3", "abc", 2,
                "Use mix of characters, numbers, and symbols."));

            _questions.Add(new QuizQuestion("What is MFA?",
                "Multi Factor Authentication", "Main Firewall Access", "Multiple File Access", "Mobile Friendly App", 0,
                "Multi-factor adds extra security layers."));

            _questions.Add(new QuizQuestion("What is the 3-2-1 backup rule?",
                "3 copies, 2 media, 1 offsite", "3 passwords, 2 devices, 1 cloud", "Backup every 3 days", "3 antivirus, 2 firewalls", 0,
                "Keep 3 copies of data on 2 different media with 1 offsite."));
        }

        // Rest of the class remains the same...
        public string GetCurrentQuestion()
        {
            if (_currentIndex >= _questions.Count)
                return "🎉 Quiz Completed!";

            return _questions[_currentIndex].ToString();
        }

        public string SubmitAnswer(int selected)
        {
            if (_currentIndex >= _questions.Count)
                return "Quiz already finished.";

            var q = _questions[_currentIndex];
            bool correct = selected == q.CorrectAnswer;

            string result = correct ? "✅ Correct!\n" : $"❌ Wrong! Correct was: {q.Options[q.CorrectAnswer]}\n";

            if (correct) _score++;

            result += "\n" + q.Explanation;

            _currentIndex++;
            return result;
        }

        public bool HasMoreQuestions() => _currentIndex < _questions.Count;

        public void ResetQuiz()
        {
            _currentIndex = 0;
            _score = 0;
        }

        public string GetFinalResult() => $"🏆 Final Score: {_score}/{_questions.Count}";
    }
}