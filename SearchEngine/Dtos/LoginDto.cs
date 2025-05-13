using System.ComponentModel.DataAnnotations;

namespace SearchEngine.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]

        public string Password { get; set; }

    }
}
