using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoFinal.Models;

namespace ToDoFinal.Data
{
    public class ToDoModelContext : DbContext
    {
        public DbSet<ToDoTask> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:ignokitm.database.windows.net,1433;Initial Catalog=ToDoFinal;Persist Security Info=False;User ID=taipignas;Password=ca9rcgG^4MSL;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
