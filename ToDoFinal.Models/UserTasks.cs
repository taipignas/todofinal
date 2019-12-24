using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoFinal.Models
{
    public class UserTasks
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public List<ToDoTask> Tasks { get; set; }
    }
}
