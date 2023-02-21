using System.ComponentModel.DataAnnotations;

namespace ToDoBackend.Entities.View_Models
{
    public class View_user
    {
        [Required]
        public int user_id { get; set; }

        [Required]
        [MaxLength(30)]
        public string user_name { get; set; }

        [Required]
        [MaxLength(50)]
        public string user_surname { get; set; }

        [Required]
        [MaxLength(50)]
        public string user_email { get; set; }
    }
}

