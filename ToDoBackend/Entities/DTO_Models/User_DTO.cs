using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ToDoBackend.Entities.DTO_Models
{
    public class User_DTO
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
        //verification if email address has correct format. In case it is invalid, returns a provided error message
        [EmailAddress(ErrorMessage = "Please provide email address in correct format")]
        [MaxLength(50)]
        public string user_email { get; set; }

        [Required]
        [MaxLength(500)]
        public string user_password { get; set; }

        public string user_refresh_token { get; set; }//property used to store refresh token for a user
    }
}
