using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToDoFinal.Identity
{
    public class ToDoUserContext : IdentityDbContext<ToDoUser>
    {
        public ToDoUserContext(DbContextOptions<ToDoUserContext> options)
            : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ToDoFinal;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}
    }
}
