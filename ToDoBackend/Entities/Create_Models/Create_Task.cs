using System.ComponentModel.DataAnnotations;
using ToDoBackend.Entities.DTO_Models;
using ToDoBackend.Entities.View_Models;

namespace ToDoBackend.Entities.Create_Models
{
    public class Create_Task
    {
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
        public int task_type_id { get; set; }

    }
}
