using System.Collections.Generic;
using ToDoFinal.Data;
using ToDoFinal.Models;
using System.Linq;

namespace ToDoFinal.Services
{
    public class AdminTasks
    {
        private readonly ToDoModelContext _context;
        public AdminTasks(ToDoModelContext context)
        {
            _context = context;
        }

        public List<ToDoTask> GetAll()
        {
            return _context.Tasks.ToList();
        }
    }
}
