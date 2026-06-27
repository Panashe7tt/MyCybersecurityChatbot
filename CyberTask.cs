namespace MyCybersecurityChatbot
{
    public class CyberTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public System.DateTime? ReminderDate { get; set; }

        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
    }
}