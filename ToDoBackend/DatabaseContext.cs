using Microsoft.EntityFrameworkCore;
using ToDoBackend.Entities;

namespace ToDoBackend
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {

        }

        //they will be mapped to database tables
        public DbSet<task> task { get; set; }
        public DbSet<task_type> task_type { get; set; }
        public DbSet<user> user { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //defining primary keys for the tables
            //another approach is to use [Key] in model class
            builder.Entity<task>().HasKey(task => task.task_id);
            builder.Entity<task_type>().HasKey(task_type => task_type.task_type_id);
            builder.Entity<user>().HasKey(user => user.user_id);
        }
    }
 
}
