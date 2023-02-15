using System.ComponentModel.DataAnnotations;

namespace ToDoBackend.Entities.DTO_Models
{
    public class Task_DTO
    {
        //EF Core will call the parametrised ctor if it is provided
        //if parametrised ctor is not provided, EF Core calls the parameterless ctor
        //public Task_DTO(int task_id, string task_name, string? task_description, DateTime task_creation_date, DateTime? task_close_date, string task_priority, string task_status, Task_type_DTO task_Type)
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

        [Required]
        [MaxLength(50)]
        public string task_name { get; set; }

        [MaxLength(200)]
        public string? task_description { get; set; }

        [Required]
        public DateTime task_creation_date { get; set; }

        public DateTime? task_close_date { get; set; }

        [Required]
        public string task_priority { get; set; }

        [Required]
        public string task_status { get; set; }

        [Required]
        public Task_type_DTO task_Type { get; set; }

        [Required]
        public User_DTO user { get; set; }

    }
}
