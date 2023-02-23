using System.ComponentModel.DataAnnotations;

namespace ToDoBackend.Entities.View_Models
{
    public class ViewTask
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
        public ViewTaskType task_Type { get; set; }

        [Required(ErrorMessage = "Task needs to be assigned to a user")]
        public int user_id { get; set; }

        [Required]
        public string user_name { get; set; }

        [Required]
        public string user_surname { get; set; }
    }
}
