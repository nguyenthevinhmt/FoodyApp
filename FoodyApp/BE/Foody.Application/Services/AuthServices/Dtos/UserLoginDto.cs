using System.ComponentModel.DataAnnotations;

namespace Foody.Application.Services.AuthServices.Dtos
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email sai định dạng")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
