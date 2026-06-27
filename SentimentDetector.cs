using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCybersecurityChatbot
{
    public enum Sentiment
    {
        Neutral,
        Happy,
        Sad,
        Worried,
        Frustrated,
        Curious,
        Angry,
        Confused,
        Excited,
        Fearful
    }

    public class SentimentDetector
    {
        private readonly Dictionary<Sentiment, Dictionary<string, int>> _keywords;

        public SentimentDetector()
        {
            _keywords = new Dictionary<Sentiment, Dictionary<string, int>>
            {
                { Sentiment.Happy, new Dictionary<string, int> { { "great", 2 }, { "awesome", 3 }, { "good", 1 }, { "happy", 2 } } },
                { Sentiment.Sad, new Dictionary<string, int> { { "sad", 2 }, { "bad", 2 }, { "unhappy", 2 } } },
                { Sentiment.Worried, new Dictionary<string, int> { { "worried", 2 }, { "scared", 2 }, { "anxious", 2 } } },
                { Sentiment.Frustrated, new Dictionary<string, int> { { "frustrated", 3 }, { "annoyed", 2 } } },
                { Sentiment.Curious, new Dictionary<string, int> { { "how", 2 }, { "why", 2 }, { "what", 1 } } },
                { Sentiment.Angry, new Dictionary<string, int> { { "angry", 2 }, { "hate", 3 } } },
                { Sentiment.Confused, new Dictionary<string, int> { { "confused", 2 }, { "dont understand", 3 } } },
                { Sentiment.Excited, new Dictionary<string, int> { { "excited", 2 } } },
                { Sentiment.Fearful, new Dictionary<string, int> { { "afraid", 2 }, { "scared", 2 } } }
            };
        }

        public Sentiment Detect(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return Sentiment.Neutral;

            string text = message.ToLower();
            var scores = new Dictionary<Sentiment, int>();

            foreach (var sentiment in _keywords)
            {
                int score = 0;

                foreach (var keyword in sentiment.Value)
                {
                    if (text.Contains(keyword.Key))
                        score += keyword.Value;
                }

                scores[sentiment.Key] = score;
            }

            var best = scores.OrderByDescending(s => s.Value).First();
            return best.Value == 0 ? Sentiment.Neutral : best.Key;
        }

        public string GetResponse(Sentiment sentiment)
        {
            return sentiment switch
            {
                Sentiment.Happy => "That's great to hear! 😊",
                Sentiment.Sad => "I'm sorry you're feeling that way 💙",
                Sentiment.Worried => "Let's work through this together.",
                Sentiment.Frustrated => "Take a breath, you're doing fine.",
                Sentiment.Curious => "Good question!",
                Sentiment.Angry => "I understand your frustration.",
                Sentiment.Confused => "Let me clarify that for you.",
                Sentiment.Excited => "That’s awesome! 🎉",
                Sentiment.Fearful => "You're safe, let's handle it together.",
                _ => "Thanks for sharing."
            };
        }
    }
}