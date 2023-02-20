using System.ComponentModel.DataAnnotations;

namespace ToDoBackend.Entities.Create_Models
{
    public class Create_User
    {
        //[Required]
        //public int user_id { get; set; }

        [Required]
        [MaxLength(30)]
        public string user_name { get; set; }

        [Required]
        [MaxLength(50)]
        public string user_surname { get; set; }

        [Required]
        [MaxLength(50)]
        public string user_email { get; set; }

        [Required]
        [MaxLength(500)]
        public string user_password { get; set; }
    }
}

