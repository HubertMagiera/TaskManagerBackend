using System.ComponentModel.DataAnnotations;

namespace ToDoBackend.Entities.DTO_Models
{
    public class Task_type_DTO
    {
        //EF Core will call the parametrised ctor if it is provided
        //if parametrised ctor is not provided, EF Core calls the parameterless ctor
        //public Task_type_DTO(int task_type_id, string task_type_name)
        //{
        //    this.task_type_id = task_type_id;
        //    this.task_type_name = task_type_name;
        //}

        //public Task_type_DTO(int task_type_id, string task_type_name, string task_type_description)
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
