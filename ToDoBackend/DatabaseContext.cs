using Microsoft.EntityFrameworkCore;

namespace ToDoBackend
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {

        }

        //add dbsets for each entity
    }
}
