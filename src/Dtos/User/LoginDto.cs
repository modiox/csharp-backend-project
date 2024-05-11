using System.ComponentModel.DataAnnotations;

namespace api.Dtos.User
{
    public class LoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}