using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ToDoBackend.Entities
{
    public class User_DTO
    {
        //EF Core will call the parametrised ctor if it is provided
        //if parametrised ctor is not provided, EF Core calls the parameterless ctor
        public User_DTO(int user_id, string user_name, string user_surname, string user_email, string user_password)
        {
            this.user_id = user_id;
            this.user_name = user_name;
            this.user_surname = user_surname;
            this.user_email = user_email;
            this.user_password = user_password;
        }

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
        [EmailAddress(ErrorMessage ="Please provide email address in correct format")]
        [MaxLength(50)]
        public string user_email { get; set; }

        [Required]
        [MaxLength(500)]
        public string user_password { get; set; }
    }
}
