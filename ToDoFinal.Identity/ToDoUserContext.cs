using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToDoFinal.Identity
{
    public class ToDoUserContext : IdentityDbContext<ToDoUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:ignokitm.database.windows.net,1433;Initial Catalog=ToDoFinal;Persist Security Info=False;User ID=taipignas;Password=ca9rcgG^4MSL;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
