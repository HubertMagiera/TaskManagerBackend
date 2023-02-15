using System.ComponentModel.DataAnnotations;
using ToDoBackend.Entities.DTO_Models;

namespace ToDoBackend.Entities.Update_Models
{
    public class Update_Task
    {
        [Required]
        public int task_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string task_name { get; set; }

        [MaxLength(200)]
        public string? task_description { get; set; }

        [Required]
        public string task_priority { get; set; }

        [Required]
        public string task_status { get; set; }

        [Required]
        public int task_type_id { get; set; }
    }
}
