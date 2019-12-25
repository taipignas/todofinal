using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoFinal.Models;

namespace ToDoFinal.Data
{
    public class ToDoModelContext : DbContext
    {
        public ToDoModelContext(DbContextOptions<ToDoModelContext> options)
            : base(options)
        {

        }
        public DbSet<ToDoTask> Tasks { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ToDoFinal;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}
    }
}
