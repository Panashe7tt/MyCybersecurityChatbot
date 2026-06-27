namespace MyCybersecurityChatbot
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public string[] Options { get; set; }
        public int CorrectAnswer { get; set; }  // 0=A, 1=B, 2=C, 3=D
        public string Explanation { get; set; }

        public QuizQuestion(string question, string a, string b, string c, string d, int correct, string explanation)
        {
            Question = question;
            Options = new[] { a, b, c, d };
            CorrectAnswer = correct;
            Explanation = explanation;
        }

        public override string ToString()
        {
            return $"{Question}\n\n" +
                   $"A. {Options[0]}\n" +
                   $"B. {Options[1]}\n" +
                   $"C. {Options[2]}\n" +
                   $"D. {Options[3]}";
        }
    }
}