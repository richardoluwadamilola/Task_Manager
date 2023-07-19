using Task_Manager.Models;

namespace TaskManager.EntityDTOs
{
    public class TaskDto
    {
        internal int Id;

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public PriorityLevel Priority { get; set; }
        public int UserId { get; set; }
    }
}
