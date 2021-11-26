using Microsoft.EntityFrameworkCore;
using WebApplicationMINIMAL.Model;

namespace WebApplicationMINIMAL.Data
{
 

    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options)
            : base(options) { }

        public DbSet<Todo> Todos => Set<Todo>();
    }
}
