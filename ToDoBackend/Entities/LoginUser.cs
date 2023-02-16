using System.ComponentModel.DataAnnotations;

namespace ToDoBackend.Entities
{
    public class LoginUser
    {


        [Required]
        [EmailAddress(ErrorMessage = "Please provide email address in correct format")]
        [MaxLength(50)]
        public string user_email { get; set; }

        [Required]
        [MaxLength(500)]
        public string user_password { get; set; }
    }
}
