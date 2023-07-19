namespace Task_Manager.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public PriorityLevel Priority { get; set; }

        // Foreign key property
        public int UserId { get; set; }

        // Navigation property
        public User User { get; set; }
    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }
}
