﻿namespace Task_Manager.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public ICollection<Task> Tasks { get; set; }

    }
}
