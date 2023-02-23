using Microsoft.EntityFrameworkCore;
using ToDoBackend.Entities.DTO_Models;

namespace ToDoBackend
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {

        }

        //they will be mapped to database tables
        public DbSet<TaskDTO> task { get; set; }
        public DbSet<TaskTypeDTO> task_type { get; set; }
        public DbSet<UserDTO> user { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //defining primary keys for the tables
            //another approach is to use [Key] in model class
            builder.Entity<TaskDTO>().HasKey(task => task.task_id);
            builder.Entity<TaskTypeDTO>().HasKey(task_type => task_type.task_type_id);
            builder.Entity<UserDTO>().HasKey(user => user.user_id);
        }
    }
 
}
