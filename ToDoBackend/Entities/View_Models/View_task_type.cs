using System.ComponentModel.DataAnnotations;

namespace ToDoBackend.Entities.View_Models
{
    public class View_task_type
    {
        //public Task_type(int task_type_id, string task_type_name)
        //{
        //    this.task_type_id = task_type_id;
        //    this.task_type_name = task_type_name;
        //}

        //public Task_type(int task_type_id, string task_type_name, string task_type_description)
        //{
        //    this.task_type_id = task_type_id;
        //    this.task_type_name = task_type_name;
        //    this.task_type_description = task_type_description;
        //}
        [Required]
        public int task_type_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string task_type_name { get; set; }

        [MaxLength(500)]
        public string? task_type_description { get; set; }
    }
}
