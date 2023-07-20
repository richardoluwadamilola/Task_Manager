using System.ComponentModel.DataAnnotations;

namespace Task_Manager.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }

        public ICollection<Task> Tasks { get; set; }

    }
}
