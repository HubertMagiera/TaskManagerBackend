using System.ComponentModel.DataAnnotations;

namespace ToDoBackend.Entities.View_Models
{
    public class View_task
    {
        //EF Core will call the parametrised ctor if it is provided
        //if parametrised ctor is not provided, EF Core calls the parameterless ctor
        //public Task(int task_id, string task_name, string? task_description, DateTime task_creation_date, DateTime? task_close_date, string task_priority, string task_status, Task_type task_Type)
        //{
        //    this.task_id = task_id;
        //    this.task_name = task_name;
        //    this.task_description = task_description;
        //    this.task_creation_date = task_creation_date;
        //    this.task_close_date = task_close_date;
        //    this.task_priority = task_priority;
        //    this.task_status = task_status;
        //    this.task_Type = task_Type;
        //}

        [Required]
        public int task_id { get; set; }

        [Required(ErrorMessage = "Task name is required")]
        [MaxLength(50)]
        public string task_name { get; set; }

        [MaxLength(200)]
        public string? task_description { get; set; }

        [Required]
        public DateTime task_creation_date { get; set; }

        public DateTime? task_close_date { get; set; }

        [Required(ErrorMessage = "Task priority is required")]
        public string task_priority { get; set; }

        [Required(ErrorMessage = "Task status is required")]
        public string task_status { get; set; }

        [Required(ErrorMessage = "Task type is required")]
        public View_task_type task_Type { get; set; }

        [Required(ErrorMessage = "Task needs to be assigned to a user")]
        public int user_id { get; set; }

        [Required]
        public string user_name { get; set; }

        [Required]
        public string user_surname { get; set; }
    }
}
