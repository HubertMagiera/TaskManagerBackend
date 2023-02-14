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
        public DbSet<Task_DTO> task { get; set; }
        public DbSet<Task_type_DTO> task_type { get; set; }
        public DbSet<User_DTO> user { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //defining primary keys for the tables
            //another approach is to use [Key] in model class
            builder.Entity<Task_DTO>().HasKey(task => task.task_id);
            builder.Entity<Task_type_DTO>().HasKey(task_type => task_type.task_type_id);
            builder.Entity<User_DTO>().HasKey(user => user.user_id);
        }
    }
 
}
