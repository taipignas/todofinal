using System.Collections.Generic;
using System.Linq;
using ToDoFinal.Data;
using ToDoFinal.Models;

namespace ToDoFinal.Services
{
    public class ManageTasks
    {
        private readonly ToDoModelContext _context;
        public ManageTasks(ToDoModelContext context)
        {
            _context = context;
        }

        public void AddTask(ToDoTask task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public List<ToDoTask> GetTasks(string userId)
        {
            return _context.Tasks.Where(t => t.ToDoUserId == userId).ToList();
        }

        public void UpdateTask(ToDoTask task, int taskId)
        {
            ToDoTask taskForUpdate = _context.Tasks.FirstOrDefault(t => t.Id == taskId);
            taskForUpdate.Priority = task.Priority;
            taskForUpdate.DueDate = task.DueDate;
            taskForUpdate.Status = task.Status;
            _context.SaveChanges();
        }
    }
}
