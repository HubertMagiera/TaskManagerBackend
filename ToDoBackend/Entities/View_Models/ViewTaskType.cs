using System.ComponentModel.DataAnnotations;

namespace ToDoBackend.Entities.View_Models
{
    public class ViewTaskType
    {
        [Required]
        public int task_type_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string task_type_name { get; set; }

        [MaxLength(500)]
        public string? task_type_description { get; set; }

    }
}
